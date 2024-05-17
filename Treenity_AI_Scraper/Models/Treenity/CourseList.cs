using static Treenity_AI_Scraper.Extensions.TreenityProtocolExtensions;

namespace Treenity_AI_Scraper.Models.Treenity
{
    internal class CourseList : TreenityProtocolBaseModel
    {
        public Rt[] rt { get; set; }

        public class Rt
        {
            public string courseId { get; set; }
            public string courseName { get; set; }
            public string content { get; set; }
            public string startTime { get; set; }
            public string endTime { get; set; }
            public int courseStatus { get; set; }
            public object createUserId { get; set; }
            public object createUserName { get; set; }
            public int classId { get; set; }
            public string className { get; set; }
            public int schoolId { get; set; }
            public string schoolName { get; set; }
            public int knowledgeNum { get; set; }
            public int teacherUserId { get; set; }
            public string teacherUserName { get; set; }
            public int courseType { get; set; }
            public string startOpenTime { get; set; }
        }

    }
}
