using static Treenity_AI_Scraper.Extensions.TreenityProtocolExtensions;

namespace Treenity_AI_Scraper.Models.Treenity
{
    internal class PracticeInfo : TreenityProtocolBaseModel
    {
        public Rt rt { get; set; }
        public class Rt
        {
            public string idStr { get; set; }
            public string freeExam { get; set; }
            public object masteryScore { get; set; }
            public int? count { get; set; }
            public int? questionNum { get; set; }
            public string idHash { get; set; }
            public object paperTime { get; set; }
            public long? paperId { get; set; }
            public long? examTestId { get; set; }
            public int? status { get; set; }
        }
    }
}
