using static Treenity_AI_Scraper.Extensions.TreenityProtocolExtensions;

namespace Treenity_AI_Scraper.Models.Treenity
{
    internal class DetailList : TreenityProtocolBaseModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class AllQuestionList
        {
            public AnalysisDto analysisDto { get; set; }
            public string content { get; set; }
            public object createTime { get; set; }
            public int? diff { get; set; }
            public long? id { get; set; }
            public int? isCorrect { get; set; }
            public int? isMarker { get; set; }
            public int? parentId { get; set; }
            public int? questionIndex { get; set; }
            public string questionName { get; set; }
            public List<QuestionOptionList> questionOptionList { get; set; }
            public int? questionTypeId { get; set; }
            public List<string> studentResult { get; set; }
            public object result { get; set; }
            public object analysisUrl { get; set; }
            public string analysis { get; set; }
            public object fileType { get; set; }
            public object fileId { get; set; }
            public object dataId { get; set; }
            public object isManualReview { get; set; }
            public object questionUuID { get; set; }
            public object fileSuffix { get; set; }
            public object fileSize { get; set; }
            public object updateTime { get; set; }
            public object questionFileList { get; set; }
            public object questionDtoList { get; set; }
            public object chapterName { get; set; }
            public object evalList { get; set; }
            public object aiQuestionKnowledgeAimList { get; set; }
            public object contentUnHtml { get; set; }
            public object isDisplay { get; set; }
        }

        public class AnalysisDto
        {
            public object analyze { get; set; }
            public object dataFileVos { get; set; }
        }

        public class QuestionOptionList
        {
            public long? id { get; set; }
            public int? questionID { get; set; }
            public string content { get; set; }
            public object sort { get; set; }
            public int? isCorrect { get; set; }
            public object isFrame { get; set; }
            public int? paperId { get; set; }
        }
        public Rt rt { get; set; }
        public class Rt
        {
            public int? wrongQuestionNum { get; set; }
            public List<AllQuestionList> allQuestionList { get; set; }
            public List<WrongQuestionList> wrongQuestionList { get; set; }
            public List<object> correctQuestionList { get; set; }
            public int? correctQuestionNum { get; set; }
            public int? paperTotalQuestionNum { get; set; }
        }

        public class WrongQuestionList
        {
            public AnalysisDto analysisDto { get; set; }
            public string content { get; set; }
            public object createTime { get; set; }
            public int? diff { get; set; }
            public long? id { get; set; }
            public int? isCorrect { get; set; }
            public int? isMarker { get; set; }
            public int? parentId { get; set; }
            public int? questionIndex { get; set; }
            public string questionName { get; set; }
            public List<QuestionOptionList> questionOptionList { get; set; }
            public int? questionTypeId { get; set; }
            public List<string> studentResult { get; set; }
            public object result { get; set; }
            public object analysisUrl { get; set; }
            public string analysis { get; set; }
            public object fileType { get; set; }
            public object fileId { get; set; }
            public object dataId { get; set; }
            public object isManualReview { get; set; }
            public object questionUuID { get; set; }
            public object fileSuffix { get; set; }
            public object fileSize { get; set; }
            public object updateTime { get; set; }
            public object questionFileList { get; set; }
            public object questionDtoList { get; set; }
            public object chapterName { get; set; }
            public object evalList { get; set; }
            public object aiQuestionKnowledgeAimList { get; set; }
            public object contentUnHtml { get; set; }
            public object isDisplay { get; set; }
        }


    }
}
