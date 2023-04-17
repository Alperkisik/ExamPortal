using System.ComponentModel.DataAnnotations.Schema;
using static System.Formats.Asn1.AsnWriter;

namespace StudentTrainingPortal.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Question>? Questions { get; set; }
        public ICollection<StudentQuestionAnswer>? Answers { get; set; }
        public ICollection<Student>? Students { get; set; }

        public int GetMaxScore()
        {
            if (Questions == null) return 0;

            int maxScore = 0;
            foreach (var Question in Questions)
            {
                maxScore += Question.MaxScore;
            }

            return maxScore;
        }

        public int GetStudentScore(Student student)
        {
            if (Answers == null || Questions == null) return 0;

            var StudentAnswers = Answers.Where(i => i.Student == student).ToList();
            if (StudentAnswers.Count != Questions.Count) return 0;

            int score = 0;

            foreach (var answer in Answers)
            {
                if (answer.Evaluation == null) return 0;

                score += answer.Evaluation.Score;
            }

            return score;
        }

        public List<StudentQuestionAnswer> GetStudentAnswers(Student student)
        {
            return Answers.Where(i => i.Student == student).ToList();
        }

        public bool IsEvaluated(Student student)
        {
            var answers = Answers.Where(i => i.Student == student);
            foreach (var answer in answers)
            {
                if (answer.Evaluation != null) return true;
            }
            return false;
        }
    }
}
