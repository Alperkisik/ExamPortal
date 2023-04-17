using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace StudentTrainingPortal.Models
{
    public class StudentQuestionAnswerEvaluation
    {
        public int Id { get; set; }
        [ForeignKey("Answer")]
        public int AnswerId { get; set; }
        public StudentQuestionAnswer Answer { get; set; }
        public Teacher? Teacher { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        [DefaultValue(false)]
        public bool IsCorrect { get; set; }
        [DefaultValue(0)]
        public int Score { get; set; }
    }
}
