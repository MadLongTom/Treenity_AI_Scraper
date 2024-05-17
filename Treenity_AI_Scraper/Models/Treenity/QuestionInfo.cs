using static Treenity_AI_Scraper.Extensions.TreenityProtocolExtensions;

namespace Treenity_AI_Scraper.Models.Treenity
{
    internal class QuestionInfo : TreenityProtocolBaseModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public long? id { get; set; }
            public string content { get; set; }
            public int? questionType { get; set; }
            public string questionTypeName { get; set; }
            public object result { get; set; }
            public object childQuestionVos { get; set; }
            public List<UserAnswerVo> userAnswerVo { get; set; }
            public object analysisVo { get; set; }
            public List<OptionVo> optionVos { get; set; }
            public object dataFileVos { get; set; }
        }

        public class OptionVo
        {
            public long? id { get; set; }
            public string content { get; set; }
            public int? isCorrect { get; set; }
            public int? sort { get; set; }
        }

        public Data data { get; set; }


        public class UserAnswerVo
        {
            public long? questionId { get; set; }
            public string answer { get; set; }
            public object isCorrect { get; set; }
            public object score { get; set; }
            public object correctAnswer { get; set; }
            public object dataVos { get; set; }
        }


    }
}
