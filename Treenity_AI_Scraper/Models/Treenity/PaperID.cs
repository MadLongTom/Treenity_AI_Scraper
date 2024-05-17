using static Treenity_AI_Scraper.Extensions.TreenityProtocolExtensions;

namespace Treenity_AI_Scraper.Models.Treenity
{
    internal class PaperID : TreenityProtocolBaseModel
    {
        public Rt rt { get; set; }
        public class Rt
        {
            public int paperId { get; set; }
            public int examTestId { get; set; }
            public string examPaperType { get; set; }
            public bool withoutQFlag { get; set; }
            public string idHash { get; set; }
            public string idStr { get; set; }
            public bool allowCopy { get; set; }
            public bool lookAnswer { get; set; }
        }

    }
}
