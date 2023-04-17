using System.ComponentModel.DataAnnotations.Schema;

namespace StudentTrainingPortal.Models
{
    public class StudentQuestionAnswer
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? Content { get; set; }
        public Exam Exam { get; set; }
        public Question Question { get; set; }
        public Student Student { get; set; }
        public StudentQuestionAnswerEvaluation? Evaluation { get; set; }
    }
}
