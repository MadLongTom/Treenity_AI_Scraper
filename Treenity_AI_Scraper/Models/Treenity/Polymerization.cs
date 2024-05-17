using static Treenity_AI_Scraper.Extensions.TreenityProtocolExtensions;

namespace Treenity_AI_Scraper.Models.Treenity
{
    internal class Polymerization : TreenityProtocolBaseModel
    {
        public Rt rt { get; set; }
        public class Rt
        {
            public string courseId { get; set; }
            public string type { get; set; }
            public int classId { get; set; }
        }
    }
}
