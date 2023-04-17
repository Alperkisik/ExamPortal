namespace StudentTrainingPortal.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Exam>? Exams { get; set; }
        //public ICollection<Assigment>? Assigments { get; set; }

        public int GetScore(Exam exam)
        {
            if (exam.Answers == null) return -1;
            if (exam.Answers.Any(i => i.Student.Id == Id))
            {
                int score = 0;
                foreach (var answer in exam.Answers)
                {
                    if (answer.Evaluation == null) return -1;
                    score += answer.Evaluation.Score;
                }
                return score;
            }
            else
            {
                return -1;
            }
               
            
        }
    }
}
