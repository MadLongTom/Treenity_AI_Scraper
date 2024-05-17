using static Treenity_AI_Scraper.Extensions.TreenityProtocolExtensions;

namespace Treenity_AI_Scraper.Models.Treenity
{
    internal class GeneralExamDetail : TreenityProtocolBaseModel
    {
        public Rt rt { get; set; }

        public class Rt
        {
            public long? examId { get; set; }
            public long? examTestId { get; set; }
            public int? examType { get; set; }
            public string examName { get; set; }
            public int? paperId { get; set; }
            public long? courseId { get; set; }
            public int? questionNum { get; set; }
            public int? examDuration { get; set; }
            public int? totalScore { get; set; }
            public object knowledgeNum { get; set; }
            public object knowledgeList { get; set; }
            public Lastgeneralexampaperdto lastGeneralExamPaperDto { get; set; }
            public float? historyBestScore { get; set; }
            public int? remainderDuration { get; set; }
            public int? completedQuestionNum { get; set; }
            public int? remainderQuestionNum { get; set; }
            public string examPaperType { get; set; }
            public int? examStatus { get; set; }
            public bool? isObjective { get; set; }
            public int? isTesting { get; set; }
            public object unParsing { get; set; }
            public object copy { get; set; }
        }

        public class Lastgeneralexampaperdto
        {
            public long? examId { get; set; }
            public long? paperId { get; set; }
            public int? paperType { get; set; }
            public long submitTime { get; set; }
            public float score { get; set; }
            public int? lastTestDays { get; set; }
        }

    }
}
