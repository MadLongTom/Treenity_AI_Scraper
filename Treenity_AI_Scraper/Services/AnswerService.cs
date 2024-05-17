using System.Text.Json;
using Treenity_AI_Scraper.Models.Database;

namespace Treenity_AI_Scraper.Services
{
    internal class AnswerService(ProgramDbContext dbContext)
    {
        public List<long> GetAnswer(long questionId)
        {
            return dbContext.AnswerStores.FirstOrDefault(a => a.questionId == questionId)?.answers ?? [];
        }
        public async Task SetAnswer(long questionId, List<long> rightAnswers)
        {
            var answer = dbContext.AnswerStores.FirstOrDefault(a => a.questionId == questionId);
            if (answer == null)
            {
                dbContext.AnswerStores.Add(new(questionId,rightAnswers));
            }
            else
            {
                answer.answers = rightAnswers;
            }
            await dbContext.SaveChangesAsync();
        }
        public static void MigrateJson2Db(string filePath)
        {
            ProgramDbContext db = new();
            var root = JsonSerializer.Deserialize<List<Answer>>(File.ReadAllText(filePath));
            db.AnswerStores.AddRange(root);
            db.AnswerStores.DistinctBy(a => a.questionId);
            db.SaveChanges();
        }
        public static void SaveToJson(string filePath)
        {
            ProgramDbContext db = new();
            var answers = db.AnswerStores.ToList();
            File.WriteAllText(filePath, JsonSerializer.Serialize(answers));
        }
    }
}
