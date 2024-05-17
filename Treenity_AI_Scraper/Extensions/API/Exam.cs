using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using Treenity_AI_Scraper.Models.Runtime;
using Treenity_AI_Scraper.Models.Treenity;

namespace Treenity_AI_Scraper.Extensions.API
{
    internal static class Exam
    {
        static long GetTimeStamp()
        {
            return Convert.ToInt64((DateTime.Now - DateTime.UnixEpoch).TotalMilliseconds);
        }
        static FormUrlEncodedContent Encrypt(this Entity entity, Dictionary<string, object>? dict = null)
        {
            string secret = JsonSerializer.Serialize(dict);
            //Console.WriteLine(secret);
            var post = new Dictionary<string, string>
            {
                ["secretStr"] = entity.TreenityCryptoProvider.yxyz(secret, "3"),
                ["date"] = GetTimeStamp().ToString()
            };
            return new FormUrlEncodedContent(post);
        }
        static string EncryptQuery(string url, Entity entity, Dictionary<string, object> payload)
        {
            string secret = JsonSerializer.Serialize(payload);
            //Console.WriteLine(secret);
            var post = new Dictionary<string, string?>
            {
                ["secretStr"] = entity.TreenityCryptoProvider.yxyz(secret, "3"),
                ["date"] = GetTimeStamp().ToString()
            };
            return QueryHelpers.AddQueryString(url, post);
        }
        public static async Task<HttpResponseMessage> openExam(this Entity entity, long courseId, long examPaperId, long examTestId)
        {
            var payload = new Dictionary<string, object>
            {
                ["examTestId"] = examTestId,
                ["examPaperId"] = examPaperId,
                ["courseId"] = courseId
            };
            var ctx = Encrypt(entity, payload);
            return await entity.client!.PostAsync("https://studentexamtest.zhihuishu.com/gateway/t/v1/exam/user/openExam", ctx);
        }
        public static async Task<string> OpenExam(this Entity entity, long courseId, long examPaperId, long examTestId)
        {
            var payload = new Dictionary<string, object>
            {
                ["examTestId"] = examTestId,
                ["examPaperId"] = examPaperId,
                ["courseId"] = courseId
            };
            var ctx = Encrypt(entity, payload);
            return await entity.client!.TreenityPostStringAsync("https://studentexamtest.zhihuishu.com/gateway/t/v1/exam/user/openExam", ctx);
        }


        public static async Task<HttpResponseMessage> getExamSheetInfo(this Entity entity, string examTestId, string examPaperId)
        {
            var ctx = new Dictionary<string, object>
            {
                ["examTestId"] = examTestId,
                ["examPaperId"] = examPaperId
            };
            return await entity.client!.GetAsync(EncryptQuery("https://studentexamtest.zhihuishu.com/gateway/t/v1/exam/user/getExamSheetInfo", entity, ctx));
        }
        public static async Task<SheetInfo?> GetExamSheetInfo(this Entity entity, string examTestId, string examPaperId)
        {
            var ctx = new Dictionary<string, object>
            {
                ["examTestId"] = examTestId,
                ["examPaperId"] = examPaperId
            };
            return await entity.client!.TreenityGetFromJsonAsync<SheetInfo>(EncryptQuery("https://studentexamtest.zhihuishu.com/gateway/t/v1/exam/user/getExamSheetInfo", entity, ctx));
        }



        public static async Task<HttpResponseMessage> getExamTestUserInfo(this Entity entity, string examTestId, string examPaperId)
        {
            var ctx = new Dictionary<string, object>
            {
                ["examTestId"] = examTestId,
                ["examPaperId"] = examPaperId
            };
            return await entity.client!.GetAsync(EncryptQuery("https://studentexamtest.zhihuishu.com/gateway/t/v1/exam/user/getExamTestUserInfo", entity, ctx));
        }
        public static async Task<string> GetExamTestUserInfo(this Entity entity, string examTestId, string examPaperId)
        {
            var ctx = new Dictionary<string, object>
            {
                ["examTestId"] = examTestId,
                ["examPaperId"] = examPaperId
            };
            return await entity.client!.TreenityGetStringAsync(EncryptQuery("https://studentexamtest.zhihuishu.com/gateway/t/v1/exam/user/getExamTestUserInfo", entity, ctx));
        }

        /*public static async Task<HttpResponseMessage> initExamTestUser(this Entity entity)
        {
            var ctx = _0x40ceaf(entity);
            return await entity.client!.PostAsync("https://studentexamtest.zhihuishu.com/gateway/t/v1/test/initExamTestUser", ctx);
        }*/

        public static async Task<HttpResponseMessage> getExamQuestionInfo(this Entity entity, string examTestId, long examPaperId, long questionId)
        {
            var ctx = new Dictionary<string, object>
            {
                {"examTestId",examTestId},
                {"examPaperId",examPaperId},
                {"questionId",questionId},
                {"version",1}
            };
            return await entity.client!.GetAsync(EncryptQuery("https://studentexamtest.zhihuishu.com/gateway/t/v1/question/getExamQuestionInfo", entity, ctx));
        }
        public static async Task<QuestionInfo?> GetExamQuestionInfo(this Entity entity, string examTestId, long examPaperId, long questionId)
        {
            var ctx = new Dictionary<string, object>
            {
                {"examTestId",examTestId},
                {"examPaperId",examPaperId},
                {"questionId",questionId},
                {"version",1}
            };
            return await entity.client!.TreenityGetFromJsonAsync<QuestionInfo>(EncryptQuery("https://studentexamtest.zhihuishu.com/gateway/t/v1/question/getExamQuestionInfo", entity, ctx));
        }
       

        public static async Task<HttpResponseMessage> submit(this Entity entity, string examTestId, string courseId, string examPaperId, string aiKnlowledgeId = null)
        {
            var payload = new Dictionary<string, object>
            {
                {"examTestId",examTestId},
                {"courseId",courseId},
                {"courseType",2},
                {"examPaperId",examPaperId}
            };
            if (aiKnlowledgeId != null) payload.Add("aiKnlowledgeId", aiKnlowledgeId);
            var ctx = Encrypt(entity, payload);
            return await entity.client!.PostAsync("https://studentexamtest.zhihuishu.com/gateway/t/v1/exam/user/submit", ctx);
        }
        public static async Task<string> Submit(this Entity entity, string examTestId, string courseId, string examPaperId, string aiKnlowledgeId = null)
        {
            var payload = new Dictionary<string, object>
            {
                {"examTestId",examTestId},
                {"courseId",courseId},
                {"courseType",2},
                {"examPaperId",examPaperId}
            };
            if (aiKnlowledgeId != null) payload.Add("aiKnlowledgeId", aiKnlowledgeId);
            var ctx = Encrypt(entity, payload);
            return await entity.client!.TreenityPostStringAsync("https://studentexamtest.zhihuishu.com/gateway/t/v1/exam/user/submit", ctx);
        }
        public static async Task<HttpResponseMessage> submitAI(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://studentexamtest.zhihuishu.com/gateway/t/v1/exam/user/submit", ctx);
        }
        public static async Task<HttpResponseMessage> saveAnswer(this Entity entity, string recruitId, string examTestId, long examPaperId, long questionId, string answer)
        {
            var payload = new Dictionary<string, object>
            {
                {"recruitId",recruitId},
                {"examTestId",examTestId},
                {"examPaperId",examPaperId},
                {"questionId",questionId},
                {"dataVos",null},
                {"answer",answer}
            };
            var ctx = Encrypt(entity, payload);
            return await entity.client!.PostAsync("https://studentexamtest.zhihuishu.com/gateway/t/v1/answer/saveAnswer", ctx);
        }
        public static async Task<string> SaveAnswer(this Entity entity, string recruitId, string examTestId, long examPaperId, long questionId, string answer)
        {
            var payload = new Dictionary<string, object>
            {
                {"recruitId",recruitId},
                {"examTestId",examTestId},
                {"examPaperId",examPaperId},
                {"questionId",questionId},
                {"dataVos",null},
                {"answer",answer}
            };
            var ctx = Encrypt(entity, payload);
            return await entity.client!.TreenityPostStringAsync("https://studentexamtest.zhihuishu.com/gateway/t/v1/answer/saveAnswer", ctx);
        }


        public static async Task<HttpResponseMessage> updateUserUsedTime(this Entity entity, string examTestId, string examPaperId, int heartbeatTime)
        {
            var payload = new Dictionary<string, object>
            {
                {"examTestId",examTestId},
                {"examPaperId",examPaperId},
                {"heartbeatTime",heartbeatTime}
             };
            var ctx = Encrypt(entity, payload);
            return await entity.client!.PostAsync("https://studentexamtest.zhihuishu.com/gateway/t/v1/exam/user/updateUserUsedTime", ctx);
        }
    }
}
