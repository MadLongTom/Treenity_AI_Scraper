using System.ComponentModel.DataAnnotations;

namespace Treenity_AI_Scraper.Models.Database
{

    public record Answer(long questionId, List<long>? answers)
    {
        [Key]
        public virtual long questionId { get; set; } = questionId;
        public virtual List<long> answers { get; set; } = answers ?? [];
    }

}

