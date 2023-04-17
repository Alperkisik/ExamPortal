using Microsoft.EntityFrameworkCore;

namespace StudentTrainingPortal.Models.Data
{
    public class PortalContext : DbContext
    {
        public PortalContext(DbContextOptions<PortalContext> options) : base(options)
        {
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<StudentQuestionAnswer> StudentQuestionAnswers { get; set; }
        public DbSet<StudentQuestionAnswerEvaluation> StudentQuestionAnswerEvaluations { get; set; }
    }
}
