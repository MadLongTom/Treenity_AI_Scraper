using static Treenity_AI_Scraper.Extensions.TreenityProtocolExtensions;

namespace Treenity_AI_Scraper.Models.Treenity
{
    internal class KnowledgePlaned : TreenityProtocolBaseModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Child
        {
            public long floorType { get; set; }
            public long id { get; set; }
            public long courseId { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public long pointType { get; set; }
            public long knowledgeType { get; set; }
            public string difficulties { get; set; }
            public long freeExam { get; set; }
            public string searchKey { get; set; }
            public long childNum { get; set; }
            public long level { get; set; }
            public long levelOrder { get; set; }
            public List<Child> children { get; set; }
            public object knowStastisticDto { get; set; }
            public long? mastery { get; set; }
            public object cogtGoalDtoList { get; set; }
            public object maxFloor { get; set; }
            public object percentage { get; set; }
            public object phaseExamInfoList { get; set; }
            public object courseExamInfo { get; set; }
            public long themeKnowledgeNum { get; set; }
            public long fileNum { get; set; }
            public long questionNum { get; set; }
            public object knwoledgeMastery { get; set; }
            public object maxCogtGoalLevel { get; set; }
            public long emptyResNode { get; set; }
            public long starInTeachingPlan { get; set; }
            public long examStudentNum { get; set; }
            public long freeExamCount { get; set; }
            public long pid { get; set; }
            public object mid { get; set; }
            public object mpid { get; set; }
        }

        public class KnowStastisticDto
        {
            public long knowledgeCount { get; set; }
            public long betterOfKnowledgeCount { get; set; }
            public long generalOfKnowledgeCount { get; set; }
            public long poorOfKnowledgeCount { get; set; }
            public long unpractisedOfKnowledgeCount { get; set; }
        }

        public List<Rt> rt { get; set; }


        public class Rt
        {
            public long examType { get; set; }
            public long duration { get; set; }
            public long? startTime { get; set; }
            public long? endTime { get; set; }
            public double? bestGrades { get; set; }
            public List<long> themeIds { get; set; }
            public long knowledgeCount { get; set; }
            public long? remainingTime { get; set; }
            public long existEmptyKnowledge { get; set; }
            public bool? isObjective { get; set; }
            public string examPaperType { get; set; }
            public long examStatus { get; set; }
            public string startTimeStr { get; set; }
            public string endTimeStr { get; set; }
            public double? totalScore { get; set; }
            public bool? openTestExam { get; set; }
            public long retake { get; set; }
            public long floorType { get; set; }
            public long id { get; set; }
            public long courseId { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public long pointType { get; set; }
            public long knowledgeType { get; set; }
            public string difficulties { get; set; }
            public long freeExam { get; set; }
            public string searchKey { get; set; }
            public long childNum { get; set; }
            public long level { get; set; }
            public long levelOrder { get; set; }
            public List<Child> children { get; set; }
            public KnowStastisticDto knowStastisticDto { get; set; }
            public object mastery { get; set; }
            public object cogtGoalDtoList { get; set; }
            public long maxFloor { get; set; }
            public long percentage { get; set; }
            public object phaseExamInfoList { get; set; }
            public object courseExamInfo { get; set; }
            public long themeKnowledgeNum { get; set; }
            public long fileNum { get; set; }
            public long questionNum { get; set; }
            public object knwoledgeMastery { get; set; }
            public object maxCogtGoalLevel { get; set; }
            public long emptyResNode { get; set; }
            public long starInTeachingPlan { get; set; }
            public long examStudentNum { get; set; }
            public long freeExamCount { get; set; }
            public long pid { get; set; }
            public object mid { get; set; }
            public object mpid { get; set; }
            public long examId { get; set; }
            public long examTestId { get; set; }
        }


    }
}
