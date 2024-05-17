using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using System.Web;
using Treenity_AI_Scraper.Extensions.API;
using Treenity_AI_Scraper.Services.Cipher;
using Treenity_AI_Scraper.Extensions;
using Treenity_AI_Scraper.Models.Database;
using Treenity_AI_Scraper.Models.Runtime;
using Treenity_AI_Scraper.Models.Treenity;

namespace Treenity_AI_Scraper.Services
{
    internal class WorkService(ProgramDbContext db,ILogger<WorkService> logger, TreenityCryptoProvider provider, AnswerService answerService,TreenityCookieService cookieService,IServiceProvider serviceProvider)
    {
        public async Task Run(TicketStore ticket, CancellationToken cancellationToken = default)
        {
            db.Attach(ticket);
            ticket.entityStore = await db.Entities.FindAsync(ticket.entityStore.Id) ?? throw new KeyNotFoundException("Entity has been deleted");
            Entity entity = new(ticket.entityStore!, null, provider, null);
            ticket.startTime = DateTime.Now;
            await db.SaveChangesAsync();
            CookieCollection cookieCollection = [];
            if (entity.EntityStore.CookieExpired != null && entity.EntityStore.CookieExpired > DateTime.Now && entity.EntityStore.cookie != null) cookieCollection = cookieService.GetSavedCookies(entity.EntityStore);
            else
            {
                var cookies = await cookieService.GetCookies(entity.EntityStore);
                if (cookies.Count == 0)
                {
                    await WriteHelper.WriteFileAsync("err.txt", $"{ticket.entityStore.username} {ticket.entityStore.password} {ticket.channel} 密码错误 {DateTime.Now}");
                    db.Tickets.Remove(ticket);
                    await db.SaveChangesAsync();
                    return;
                }
                await db.SaveChangesAsync();
                foreach (var c in cookies) cookieCollection.Add(new Cookie(c.Name, c.Value, c.Path, c.Domain));
            }
            if (cookieCollection.Count == 0)
            {
                await WriteHelper.WriteFileAsync("err.txt", $"{ticket.entityStore.username} {ticket.entityStore.password} {ticket.channel} 密码错误 {DateTime.Now}");
                db.Tickets.Remove(ticket);
                await db.SaveChangesAsync();
                return;
            }
            CASLOGC pInfo = JsonSerializer.Deserialize<CASLOGC>(HttpUtility.UrlDecode(cookieCollection.First(c => c.Name == "CASLOGC").Value))!;
            entity.useruuid = pInfo.uuid;
            HttpClientHandler handler = new()
            {
                CookieContainer = new(),
                AutomaticDecompression = DecompressionMethods.All
            };
            handler.CookieContainer.Add(cookieCollection);
            using HttpClient client = new(handler);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new("text/plain"));
            client.DefaultRequestHeaders.Accept.Add(new("*/*"));
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");
            client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36 Edg/120.0.0.0");
            client.DefaultRequestHeaders.Connection.Add("keep-alive");
            client.DefaultRequestHeaders.Host = @"zhihuishu-ai-run-api.zhihuishu.com";
            client.DefaultRequestHeaders.Referrer = new(@"https://ai.zhihuishu.com/");
            entity.client = client;

            logger.LogInformation(ticket.entityStore.username + ":" + await entity.QueryLoginUserIdentity());

            var courseList = (await entity.StudentCourseCourseList())!.rt;

            logger.LogInformation(ticket.entityStore.username + ":" + string.Join(',', courseList.Select(p => p.courseName + $"[{p.courseId}]")));

            var courseIdList = db.Channels.First(a => a.Id.ToString() == ticket.channel).coursdIds;

            var testId = courseList.FirstOrDefault(b => courseIdList.Contains(Convert.ToInt64(b.courseId)));

            if (testId == null)
            {
                await WriteHelper.WriteFileAsync("err.txt", $"{ticket.entityStore.username} {ticket.entityStore.password} {ticket.channel} 未到开始时间 {DateTime.Now}");
                db.Tickets.Remove(ticket);
                await db.SaveChangesAsync();
                return;
            }

            var poly = (await entity.GetCourseBaseInfo(testId.courseId))!.rt;

            var kp = (await entity.KnowledgePlaned(courseList.First(b => courseIdList.Contains(Convert.ToInt64(b.courseId))).courseId, poly.classId.ToString()))!.rt;

            foreach (var exam in kp.Where(node => node.floorType == 1 && (node.bestGrades == null || node.bestGrades < 100) && node.openTestExam!.Value))
            {
                Loc_RetestExam:
                var dbs = (await entity.GetTestDetailByStage(exam.courseId, exam.id, poly.classId))!.rt;
                var pid = (await entity.GetPaperID(exam.courseId, exam.id, exam.examPaperType == "SCHEDULE" ? 2 : 3, poly.classId, dbs.paperId, true))!.rt;
                entity.client!.DefaultRequestHeaders.Host = "studentexamtest.zhihuishu.com";
                await entity.OpenExam(exam.courseId, pid.paperId, exam.id);
                logger.LogInformation(ticket.entityStore.username + ":" + await entity.GetExamTestUserInfo(exam.id.ToString(), pid.paperId.ToString()));
                var ques = (await entity.GetExamSheetInfo(exam.id.ToString(), pid.paperId.ToString()))!.data;
                entity.client!.DefaultRequestHeaders.Referrer = new("https://studentexamcomh5.zhihuishu.com/");
                foreach (var psv in ques.partSheetVos[0].questionSheetVos)
                {
                    var qinfo = (await entity.GetExamQuestionInfo(exam.id.ToString(), pid.paperId, psv.questionId!.Value))!.data;
                    var ans = answerService.GetAnswer(Convert.ToInt64(qinfo.id));
                    if (ans.Count > 0)
                    {
                        await entity.SaveAnswer(exam.courseId.ToString(), exam.id.ToString(), pid.paperId, Convert.ToInt64(qinfo.id), string.Join("#@#", ans));
                        logger.LogInformation(ticket.entityStore.username + ":" + qinfo.id + ":" + string.Join(',', ans));
                    }
                    else
                    {
                        await entity.SaveAnswer(exam.courseId.ToString(), exam.id.ToString(), pid.paperId, Convert.ToInt64(qinfo.id), qinfo.optionVos[0].id.ToString());
                        logger.LogInformation(ticket.entityStore.username + ":" + qinfo.id + ":NotFound");
                    }
                }
                await Task.Delay(1000);
                do
                {
                    ques = (await entity.GetExamSheetInfo(exam.id.ToString(), pid.paperId.ToString()))!.data;
                } while (ques.answerCount != ques.questionCount);

                logger.LogInformation(ticket.entityStore.username + ":" + await entity.Submit(exam.id.ToString(), exam.courseId.ToString(), pid.paperId.ToString()));

                ques = (await entity.GetExamSheetInfo(exam.id.ToString(), pid.paperId.ToString()))!.data;

                foreach (var uq in ques.partSheetVos[0].questionSheetVos.Where(q => q.correct != 1))
                {
                    var qinfo = (await entity.GetExamQuestionInfo(exam.id.ToString(), pid.paperId, Convert.ToInt64(uq.questionId)))!.data;
                    await answerService.SetAnswer(Convert.ToInt64(uq.questionId), qinfo.optionVos.Where(o => o.isCorrect == 1)
                                                                                                                .Select(o => o.id)
                                                                                                                .Cast<long>()
                                                                                                                .ToList());
                }
                client.DefaultRequestHeaders.Host = @"zhihuishu-ai-run-api.zhihuishu.com";
                client.DefaultRequestHeaders.Referrer = new(@"https://ai.zhihuishu.com/");
                if (ques.partSheetVos[0].questionSheetVos.Where(q => q.correct != 1).Count() != 0) goto Loc_RetestExam;
            }

            kp = (await entity.KnowledgePlaned(courseList.First(b => courseIdList.Contains(Convert.ToInt64(b.courseId))).courseId, poly.classId.ToString()))!.rt;

            foreach (var point in kp.Where(node => node.floorType == 0)
                                    .SelectMany(rt => rt.children)
                                    .Flat(child => child.children)
                                    .Where(node => node.freeExam == 0 && (node.mastery == null || node.mastery < 80)))
            {
                Loc_RetestPoint:
                logger.LogInformation(ticket.entityStore.username + ":" + point.title);
                await Task.Delay(2000);
                var prInfo = (await entity.GetPracticeInfo(poly.courseId, poly.classId, point.id.ToString()))!.rt;
                entity.client!.DefaultRequestHeaders.Host = "studentexamtest.zhihuishu.com";
                await entity.OpenExam(point.courseId, prInfo.paperId!.Value, prInfo.examTestId!.Value);
                logger.LogInformation(ticket.entityStore.username + ":" + await entity.GetExamTestUserInfo(prInfo.examTestId!.Value.ToString(), prInfo.paperId!.Value.ToString()));
                var ques = (await entity.GetExamSheetInfo(prInfo.examTestId!.Value.ToString(), prInfo.paperId!.Value.ToString()))!.data;
                entity.client!.DefaultRequestHeaders.Referrer = new("https://studentexamcomh5.zhihuishu.com/");
                foreach (var psv in ques.partSheetVos[0].questionSheetVos)
                {
                    var qinfo = (await entity.GetExamQuestionInfo(prInfo.examTestId!.Value.ToString(), prInfo.paperId!.Value, psv.questionId!.Value))!.data;
                    var ans = answerService.GetAnswer(Convert.ToInt64(qinfo.id));
                    if (ans.Count > 0)
                    {
                        await entity.SaveAnswer(point.courseId.ToString(), prInfo.examTestId!.Value.ToString(), prInfo.paperId!.Value, Convert.ToInt64(qinfo.id), string.Join("#@#", ans));
                        logger.LogInformation(ticket.entityStore.username + ":" + qinfo.id + ":" + string.Join(',', ans));
                    }
                    else
                    {
                        await entity.SaveAnswer(point.courseId.ToString(), prInfo.examTestId!.Value.ToString(), prInfo.paperId!.Value, Convert.ToInt64(qinfo.id), qinfo.optionVos[0].id.ToString());
                        logger.LogInformation(ticket.entityStore.username + ":" + qinfo.id + ":NotFound");
                    }
                }
                await Task.Delay(1000);
                do
                {
                    ques = (await entity.GetExamSheetInfo(prInfo.examTestId!.Value.ToString(), prInfo.paperId!.Value.ToString()))!.data;
                } while (ques.answerCount != ques.questionCount);
                logger.LogInformation(ticket.entityStore.username + ":" + await entity.Submit(prInfo.examTestId!.Value.ToString(), point.courseId.ToString(), prInfo.paperId!.Value.ToString(), point.id.ToString()));
                client.DefaultRequestHeaders.Host = @"zhihuishu-ai-run-api.zhihuishu.com";
                client.DefaultRequestHeaders.Referrer = new(@"https://ai.zhihuishu.com/");
                var dl = (await entity.FindStudentPaperQuestionDetailList(point.courseId.ToString(), point.id.ToString(), prInfo.paperId!.Value.ToString(), poly.classId.ToString(), prInfo.examTestId!.Value.ToString()))!.rt;
                foreach (var q in dl.allQuestionList)
                {
                    List<long> ansList = [.. q.questionOptionList.Where(o => o.isCorrect == 1).Select(o => o.id!.Value).Order()];
                    List<long> queryList = [.. answerService.GetAnswer(q.id!.Value).Order()];
                    if (!ansList.SequenceEqual(queryList)) await answerService.SetAnswer(q.id!.Value, ansList);
                }
                if (dl.wrongQuestionList.Count > 0) goto Loc_RetestPoint;
            }
            ticket.finished = true;
            ticket.finishTime = DateTime.Now;
            await db.SaveChangesAsync();
        }
    }
}
