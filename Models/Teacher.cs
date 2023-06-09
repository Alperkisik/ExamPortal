﻿namespace StudentTrainingPortal.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Exam>? Exams { get; set; }
    }
}
