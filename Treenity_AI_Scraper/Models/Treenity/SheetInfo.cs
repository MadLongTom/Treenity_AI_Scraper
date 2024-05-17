using static Treenity_AI_Scraper.Extensions.TreenityProtocolExtensions;

namespace Treenity_AI_Scraper.Models.Treenity
{
    internal class SheetInfo : TreenityProtocolBaseModel
    {
        public class Data
        {
            public List<PartSheetVo> partSheetVos { get; set; }
            public int? answerCount { get; set; }
            public int? questionCount { get; set; }
        }

        public class PartSheetVo
        {
            public long? id { get; set; }
            public string name { get; set; }
            public int? questionCount { get; set; }
            public double? totalScore { get; set; }
            public object partExplanation { get; set; }
            public int? sort { get; set; }
            public List<QuestionSheetVo> questionSheetVos { get; set; }
        }

        public class QuestionSheetVo
        {
            public int? parentPartId { get; set; }
            public int? questionType { get; set; }
            public object parentId { get; set; }
            public object parentQuestionType { get; set; }
            public object parentVersion { get; set; }
            public int? questionId { get; set; }
            public string questionIdHash { get; set; }
            public string questionIdStr { get; set; }
            public int? version { get; set; }
            public string sort { get; set; }
            public int? correct { get; set; }
            public double? score { get; set; }
            public int? answerState { get; set; }
        }
        public Data data { get; set; }
    }
}
