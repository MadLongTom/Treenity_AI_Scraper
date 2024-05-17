using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using Treenity_AI_Scraper.Models.Runtime;
using Treenity_AI_Scraper.Models.Treenity;

namespace Treenity_AI_Scraper.Extensions.API
{
    internal static class StuApi
    {
        static long GetTimeStamp()
        {
            return Convert.ToInt64((DateTime.Now - DateTime.UnixEpoch).TotalMilliseconds);
        }
        static string EncryptQuery(string url, Entity entity, Dictionary<string, object>? payload = null)
        {
            if (payload == null)
            {
                payload = new Dictionary<string, object>
                {
                    { "uuid" , "XNQxQboM" },
                    { "date" , GetTimeStamp() }
                };
            }
            else
            {
                payload.Add("uuid", "XNQxQboM");
                payload.Add("date", GetTimeStamp());
            }

            string secret = JsonSerializer.Serialize(payload);
            var post = new Dictionary<string, string?>
            {
                ["secretStr"] = entity.TreenityCryptoProvider.yxyz(secret, "6"),
                ["date"] = GetTimeStamp().ToString()
            };
            return QueryHelpers.AddQueryString(url, post);
        }
        static FormUrlEncodedContent EncryptBody(this Entity entity, Dictionary<string, object>? dict = null, bool withIdentity = true)
        {
            if (withIdentity)
            {
                if (dict == null)
                {
                    dict = new Dictionary<string, object>
                {
                    { "uuid" , entity.useruuid },
                    { "date" , GetTimeStamp() }
                };
                }
                else
                {
                    dict.Add("uuid", entity.useruuid);
                    dict.Add("date", GetTimeStamp());
                }
            }
            else
            {
                dict ??= [];
            }

            string secret = JsonSerializer.Serialize(dict);
            var post = new Dictionary<string, string>
            {
                ["secretStr"] = entity.TreenityCryptoProvider.yxyz(secret, "6"),
                ["date"] = GetTimeStamp().ToString()
            };
            return new FormUrlEncodedContent(post);
        }
        public static async Task<HttpResponseMessage> getLoginUserInfo(this Entity entity)
        {
            return await entity.client!.GetAsync(EncryptQuery("https://hike-ai-course.zhihuishu.com/login/getLoginUserInfo", entity));
        }
        public static async Task<HttpResponseMessage> statisticsCourseDataByCId(this Entity entity)
        {
            return await entity.client!.GetAsync(EncryptQuery("https://hike-ai-course.zhihuishu.com/aiCourse/stuApi/course/statisticsCourseDataByCId", entity));
        }
        public static async Task<HttpResponseMessage> knowledegePointPercent(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/knowlegdePoint/knowledegePointPercent", ctx);
        }
        public static async Task<HttpResponseMessage> courseBrainMapNodeList(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://hike-ai-course.zhihuishu.com/aiCourse/stuApi/course/courseBrainMapNodeList", ctx);
        }
        public static async Task<HttpResponseMessage> getCourseBrainMapList(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/knowledgePoint/getCourseBrainMapList", ctx);
        }
        public static async Task<HttpResponseMessage> findStudentPaperQuestionList(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://hike-ai-course.zhihuishu.com/aiCourse/stuApi/studentPaper/findStudentPaperQuestionList", ctx);
        }
        public static async Task<HttpResponseMessage> findStudentPaperQuestionDetailByID(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://hike-ai-course.zhihuishu.com/aiCourse/stuApi/studentPaper/findStudentPaperQuestionDetailByID", ctx);
        }
        public static async Task<HttpResponseMessage> saveStudentPageAnswer(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://hike-ai-course.zhihuishu.com/aiCourse/stuApi/studentPaper/saveStudentPageAnswer", ctx);
        }
        public static async Task<HttpResponseMessage> getStudentPaperScore(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://hike-ai-course.zhihuishu.com/aiCourse/stuApi/studentPaper/getStudentPaperScore", ctx);
        }
        public static async Task<HttpResponseMessage> saveQuestionDurationRecord(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://hike-ai-course.zhihuishu.com/aiCourse/stuApi/studentEvaluation/saveQuestionDurationRecord", ctx);
        }
        public static async Task<HttpResponseMessage> saveQuestionVideoRecord(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://hike-ai-course.zhihuishu.com/aiCourse/stuApi/studentEvaluation/saveQuestionVideoRecord", ctx);
        }
        public static async Task<HttpResponseMessage> getStudentLearnResource(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentCourse/studentKnowledgeRes", ctx);
        }
        public static async Task<HttpResponseMessage> findCourseDetail(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://hike-ai-course.zhihuishu.com/aiCourse/stuApi/course/findCourseDetail", ctx);
        }
        public static async Task<HttpResponseMessage> getPracticeInfo(this Entity entity, string courseId, long classId, string pointId)
        {
            var payload = new Dictionary<string, object>
            {
                {"courseId",courseId},
                {"classId",classId},
                {"pointId",pointId},
            };
            var ctx = EncryptBody(entity,payload,false);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentPaper/startCreatePagerByKenoledgeId", ctx);
        }

        public static async Task<PracticeInfo?> GetPracticeInfo(this Entity entity, string courseId, long classId, string pointId)
        {
            var payload = new Dictionary<string, object>
            {
                {"courseId",courseId},
                {"classId",classId},
                {"pointId",pointId},
            };
            var ctx = EncryptBody(entity, payload, false);
            return await entity.client!.TreenityPostAsJsonAsync<PracticeInfo>("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentPaper/startCreatePagerByKenoledgeId", ctx);
        }




        public static async Task<HttpResponseMessage> getStudyResourceDetail(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/resource/stuViewFile", ctx);
        }
        public static async Task<HttpResponseMessage> checkPaperQuestionDo(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://hike-ai-course.zhihuishu.com/aiCourse/stuApi/studentPaper/checkPaperQuestionDo", ctx);
        }
        public static async Task<HttpResponseMessage> saveStudyRecord(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/resource/saveStuStudyRecord", ctx);
        }
        public static async Task<HttpResponseMessage> pointPracticeStatistics(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://hike-ai-course.zhihuishu.com/aiCourse/stuApi/studentPaper/startStudentPaper", ctx);
        }
        public static async Task<HttpResponseMessage> getAccessKey(this Entity entity, long courseId, long examId, long classId)
        {
            var add = new Dictionary<string, object>
            {
                {"courseId",courseId},
                {"examId",examId},
                {"classId",classId}
            };
            var ctx = EncryptBody(entity, add);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/resource/getAccessKey", ctx);
        }

        public static async Task<AccessKey?> GetAccessKey(this Entity entity, long courseId, long examId, long classId)
        {
            var add = new Dictionary<string, object>
            {
                {"courseId",courseId},
                {"examId",examId},
                {"classId",classId}
            };
            var ctx = EncryptBody(entity, add);
            return await entity.client!.TreenityPostAsJsonAsync<AccessKey>("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/resource/getAccessKey", ctx);
        }





        public static async Task<HttpResponseMessage> updateFileBaseInfo(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://hike-ai-course.zhihuishu.com/aiCourse/stuApi/resource/saveFileBaseData", ctx);
        }
        public static async Task<HttpResponseMessage> getTestDetailByStage(this Entity entity, long courseId, long examId, long classId)
        {
            //{"courseId":4000001588,"examId":500956,"classId":4537,"uuid":"XKJvqxNM","date":1710309522000}
            var payload = new Dictionary<string, object>
            {
                {"courseId",courseId},
                {"examId",examId},
                {"classId",classId},
            };
            var ctx = EncryptBody(entity, payload);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentPaper/getGeneralExamDetail", ctx);
        }

        public static async Task<GeneralExamDetail?> GetTestDetailByStage(this Entity entity, long courseId, long examId, long classId)
        {
            //{"courseId":4000001588,"examId":500956,"classId":4537,"uuid":"XKJvqxNM","date":1710309522000}
            var payload = new Dictionary<string, object>
            {
                {"courseId",courseId},
                {"examId",examId},
                {"classId",classId},
            };
            var ctx = EncryptBody(entity, payload);
            return await entity.client!.TreenityPostAsJsonAsync<GeneralExamDetail>("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentPaper/getGeneralExamDetail", ctx);
        }

        public static async Task<HttpResponseMessage> getTestDetailByFinal(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentPaper/getCourseExamDetail", ctx);
        }
        public static async Task<HttpResponseMessage> saveStudentPaperExpireTime(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://hike-ai-course.zhihuishu.com/aiCourse/stuApi/studentPaper/saveStudentPaperExpireTime", ctx);
        }
        public static async Task<HttpResponseMessage> getPaperID(this Entity entity, long courseId, long examId, long paperType, long classId, long? paperId, bool branch) //report=false
        {
            var payload = new Dictionary<string, object>
            {
                {"courseId",courseId},
                {"examId",examId},
                {"paperType",paperType},
                {"classId",classId},
                {"paperId",paperId},
                {"branch",branch}
            };
            var ctx = EncryptBody(entity, payload);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentPaper/startPhaseTestOrCourseTest", ctx);
        }
        public static async Task<PaperID?> GetPaperID(this Entity entity, long courseId, long examId, long paperType, long classId, long? paperId, bool branch) //report=false
        {
            var payload = new Dictionary<string, object>
            {
                {"courseId",courseId},
                {"examId",examId},
                {"paperType",paperType},
                {"classId",classId},
                {"paperId",paperId},
                {"branch",branch}
            };
            var ctx = EncryptBody(entity, payload);
            return await entity.client!.TreenityPostAsJsonAsync<PaperID>("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentPaper/startPhaseTestOrCourseTest", ctx);
        }




        public static async Task<HttpResponseMessage> getTeachPlan(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentCourse/acquireTeachingPlanDetail", ctx);
        }
        public static async Task<HttpResponseMessage> knowledgeDetails(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/knowledgePoint/findKnowledgeDetail", ctx);
        }
        public static async Task<HttpResponseMessage> recommendRiskKnowledgeList(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentPaper/recommendRiskKnowledgeList", ctx);
        }
        public static async Task<HttpResponseMessage> getKnowledgeVideoTopicSegment(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentCourse/getKnowledgeVideoTopicSegment", ctx);
        }
        public static async Task<HttpResponseMessage> knowledgeRelationChart(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/knowlegdePoint/knowledgeRelationChart", ctx);
        }
        public static async Task<HttpResponseMessage> findStudentPaperQuestionDetailList(this Entity entity,string courseId,string pointId,string paperId,string classId,string examId)
        {
            var payload = new Dictionary<string, object>
            {
                {"courseId",courseId},
                {"pointId",pointId},
                {"paperId",paperId},
                {"classId",classId},
                {"examId",examId},
                {"paperType","1"}
            };
            var ctx = EncryptBody(entity,payload);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentPaper/findStudentPaperQuestionDetailList", ctx);
        }
        public static async Task<DetailList?> FindStudentPaperQuestionDetailList(this Entity entity, string courseId, string pointId, string paperId, string classId, string examId)
        {
            var payload = new Dictionary<string, object>
            {
                {"courseId",courseId},
                {"pointId",pointId},
                {"paperId",paperId},
                {"classId",classId},
                {"examId",examId},
                {"paperType","1"}
            };
            var ctx = EncryptBody(entity, payload);
            return await entity.client!.TreenityPostAsJsonAsync<DetailList>("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentPaper/findStudentPaperQuestionDetailList", ctx);
        }



        public static async Task<HttpResponseMessage> knowledgeLearningAidPackage(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/knowledgeSearch/knowledgeLearningAidPackage", ctx);
        }
        public static async Task<HttpResponseMessage> knowledgeExampleQuestionList(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/knowledgeSearch/knowledgeExampleQuestionList", ctx);
        }
        public static async Task<HttpResponseMessage> getNearKnowledge(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/knowledgePoint/getNearKnowledge", ctx);
        }
        public static async Task<HttpResponseMessage> knowledgeMapNew(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/knowlegdePoint/listCourseBrainMapNodeV2", ctx);
        }
        public static async Task<HttpResponseMessage> knowledgePlanedMap(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/knowledgePoint/knowledgePlaned/map", ctx);
        }
        public static async Task<HttpResponseMessage> getVideoByIdNew(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://newbase.zhihuishu.com/video/initVideoNew", ctx);
        }
        public static async Task<HttpResponseMessage> nodeQuestionSearch(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/questionMap/nodeQuestionSearch", ctx);
        }
        public static async Task<HttpResponseMessage> getAiCategoryById(this Entity entity)
        {
            return await entity.client!.GetAsync(EncryptQuery("https://newcourse-api.zhihuishu.com/newcourse/gateway/t/v1/aiCategory/getAiCategoryById", entity));
        }
        public static async Task<HttpResponseMessage> courseBrainMapImportStatus(this Entity entity)
        {
            return await entity.client!.GetAsync(EncryptQuery("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/knowlegdePoint/courseBrainMapImportStatus", entity));
        }
        public static async Task<HttpResponseMessage> findApplyCourseOrThemes(this Entity entity)
        {
            return await entity.client!.GetAsync(EncryptQuery("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/knowlegdePoint/findApplyCourseOrThemes", entity));
        }
        public static async Task<HttpResponseMessage> listCourseBrainMapNodeV2(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/knowlegdePoint/listCourseBrainMapNodeV2", ctx);
        }
        public static async Task<HttpResponseMessage> queryAiCategoryStatistics(this Entity entity)
        {
            return await entity.client!.GetAsync(EncryptQuery("https://newcourse-api.zhihuishu.com/newcourse/gateway/t/v1/aiCategory/queryAiCategoryStatistics", entity));
        }
        public static async Task<HttpResponseMessage> listQuestionMapGraph(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/questionMap/listQuestionMapGraph", ctx);
        }
        public static async Task<HttpResponseMessage> getQuestionNode(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/questionMap/getQuestionNode", ctx);
        }
        public static async Task<HttpResponseMessage> getPointerNumberStatic(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/questionMap/getPointerNumberStatic", ctx);
        }
        public static async Task<HttpResponseMessage> findStudentAcademicPerformance(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentPaper/findStudentAcademicPerformance", ctx);
        }
        public static async Task<HttpResponseMessage> listAiResourceChapterPage(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://newcourse-api.zhihuishu.com/newcourse/gateway/t/v1/aiResource/listAiResourceChapterPage", ctx);
        }
        public static async Task<HttpResponseMessage> listBatch(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-platform-cloud-ebook.zhihuishu.com/knowledge-map/ebook/list-batch", ctx);
        }
        public static async Task<HttpResponseMessage> getChapterMd(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-platform-cloud-ebook.zhihuishu.com/knowledge-map/ebook/chapter-md", ctx);
        }
        public static async Task<HttpResponseMessage> getPPTDetail(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/resourceslab/v2/get-ppt-detail", ctx);
        }
        public static async Task<HttpResponseMessage> getOfficeViewUrl(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://newbase.zhihuishu.com/office/getOfficeViewUrl", ctx);
        }
        public static async Task<HttpResponseMessage> isSyncNodeRelSucs(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/graph-sync/isSyncNodeRelSucs", ctx);
        }
        public static async Task<HttpResponseMessage> getCourseImg(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/map/get-course-img", ctx);
        }
        public static async Task<HttpResponseMessage> getRelations(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maprel/list-map-rel", ctx);
        }
        public static async Task<HttpResponseMessage> getSyncRecord(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/graph-sync/getSyncRecord", ctx);
        }
        public static async Task<HttpResponseMessage> getAbilitySystemNode(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/ability/getAbilitySystemNode", ctx);
        }
        public static async Task<HttpResponseMessage> getQuestionList(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/questionSystem/getQuestionList", ctx);
        }
        public static async Task<HttpResponseMessage> getGraduationList(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/talent/getGraduationList", ctx);
        }
        public static async Task<HttpResponseMessage> getTreeMapTree(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/v2/get-tree-map-tree", ctx);
        }
        public static async Task<HttpResponseMessage> getMapDetail(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/map/v2/get-map-detail", ctx);
        }
        public static async Task<HttpResponseMessage> getCourseInfo(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/course/getCourseInfo", ctx);
        }
        public static async Task<HttpResponseMessage> getFrameInfo(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/course/getFrameInfo", ctx);
        }
        public static async Task<HttpResponseMessage> listNodeSimpleTree(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/knowlegdePoint/listNodeSimpleTree", ctx);
        }
        public static async Task<HttpResponseMessage> getTreeMapDataV2(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/v2/get-tree-map-tree", ctx);
        }
        public static async Task<HttpResponseMessage> getGraphMapTree(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/v2/get-graph-map-tree", ctx);
        }
        public static async Task<HttpResponseMessage> getChildNodeList(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/get-child-node-list", ctx);
        }
        public static async Task<HttpResponseMessage> getNodePathMap(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/knowledge/node-path-map", ctx);
        }
        public static async Task<HttpResponseMessage> getRelationList(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maprel/list-map-rel-group", ctx);
        }
        public static async Task<HttpResponseMessage> getCourseTheme(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/get-theme-node-list", ctx);
        }
        public static async Task<HttpResponseMessage> getTreeKnowledgeCategory(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/list-map-tree-knowledge-category", ctx);
        }
        public static async Task<HttpResponseMessage> getKnowledgeBasisInfo(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/maptree/getNodeDetail", ctx);
        }
        public static async Task<HttpResponseMessage> getNodeChildTree(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/maptree/get-node-child-tree", ctx);
        }
        public static async Task<HttpResponseMessage> getNodeRelations(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/maptree/get-node-to-node-list", ctx);
        }
        public static async Task<HttpResponseMessage> getCourseNodeResources(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/resources/list-ai-node-resources", ctx);
        }
        public static async Task<HttpResponseMessage> previousNextNode(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/knowledge/previous-next-node", ctx);
        }
        public static async Task<HttpResponseMessage> findByStudentMapMasterRows(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/run/findByStudentMapMasterRows", ctx);
        }
        public static async Task<HttpResponseMessage> firstTreeGraphNode(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/knowledge/first-tree-graph-node", ctx);
        }
        public static async Task<HttpResponseMessage> studentCourseCourseList(this Entity entity)
        {
            return await entity.client!.GetAsync(EncryptQuery("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentCourse/courseList", entity));
        }
        public static async Task<CourseList?> StudentCourseCourseList(this Entity entity)
        {
            return await entity.client!.TreenityGetFromJsonAsync<CourseList>(EncryptQuery("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentCourse/courseList", entity));
        }
        public static async Task<HttpResponseMessage> findByStudentKnowledgeMasterList(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/polymerization/findByStudentKnowledgeMasterList", ctx);
        }
        public static async Task<HttpResponseMessage> findCourseMapBasicInfo(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentCourse/courseMapBasic", ctx);
        }
        public static async Task<HttpResponseMessage> listStudyGoal(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-study-goal", ctx);
        }
        public static async Task<HttpResponseMessage> themeNodeList(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/knowledge/theme-node-list", ctx);
        }
        public static async Task<HttpResponseMessage> getCategoryPage(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/dataApi/getCategoryPage", ctx);
        }
        public static async Task<HttpResponseMessage> listNodeAttrs(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/list-node-attrs", ctx);
        }
        public static async Task<HttpResponseMessage> queryLoginUserIdentity(this Entity entity) 
        { 
            return await entity.client!.GetAsync(EncryptQuery("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/user/queryLoginUserIdentity", entity));
        }
        public static async Task<string> QueryLoginUserIdentity(this Entity entity)
        {
            return await entity.client!.TreenityGetStringAsync(EncryptQuery("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/user/queryLoginUserIdentity", entity));
        }
        public static async Task<HttpResponseMessage> getRecListForStudent(this Entity entity)
        {
            var ctx = EncryptBody(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/studentCourse/pageRecommend", ctx);
        }
    }
}
