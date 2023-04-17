namespace StudentTrainingPortal.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Image { get; set; }
        public int MaxScore { get; set; }

        public ICollection<Exam>? Exams { get; set; }
    }
}
