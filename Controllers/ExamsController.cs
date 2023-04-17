using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentTrainingPortal.Models;
using StudentTrainingPortal.Models.Data;

namespace StudentTrainingPortal.Controllers
{
    public class ExamsController : Controller
    {
        private readonly PortalContext _context;

        public ExamsController(PortalContext context)
        {
            _context = context;
        }

        // GET: Exams
        public async Task<IActionResult> Index()
        {
            return _context.Exams != null ?
                        View(await _context.Exams.Include(i => i.Teacher).Include(i => i.Questions).Include(i => i.Students).Include(i => i.Answers).ToListAsync()) :
                        Problem("Entity set 'PortalContext.Exams'  is null.");
        }

        // GET: Exams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: Exams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Questions,Students,Teacher")] Exam exam)
        {
            if (exam.Questions != null)
            {
                if (exam.Questions.Count > 0)
                {
                    ICollection<Question> Questions = new List<Question>();

                    foreach (var question in exam.Questions)
                    {
                        foreach (var item in Questions)
                        {
                            if (item == question) return View(exam);
                        }
                        Questions.Add(_context.Questions.Find(question.Id));
                    }
                    exam.Questions = Questions;
                }
            }

            if (exam.Students != null)
            {
                if (exam.Students.Count > 0)
                {
                    ICollection<Student> Students = new List<Student>();

                    foreach (var Student in exam.Students)
                    {
                        Students.Add(_context.Students.Find(Student.Id));
                    }
                    exam.Students = Students;
                }
            }

            Teacher t = _context.Teachers.Find(exam.Teacher.Id);
            exam.Teacher = t;

            _context.Add(exam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Exams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exam);
        }

        // GET: Exams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exams == null)
            {
                return Problem("Entity set 'PortalContext.Exams'  is null.");
            }
            var exam = await _context.Exams.FindAsync(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id)
        {
            return (_context.Exams?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> ExamView(int? id)
        {
            /*if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }*/


            var exam = await _context.Exams.Where(i => i.Id == id).Include(i => i.Teacher).Include(i => i.Students).Include(i => i.Questions).FirstOrDefaultAsync();
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        [HttpPost]
        public async Task<IActionResult> ExamView(int ExamId, int StudentId, [Bind("Answers")] ICollection<StudentQuestionAnswer> Answers, [Bind("Questions")] ICollection<Question> Questions)
        {
            Exam exam = _context.Exams.Find(ExamId);
            Student student = _context.Students.Find(StudentId);
            List<StudentQuestionAnswer> _Answers = Answers.ToList();
            List<Question> _Questions = Questions.ToList();

            for (int i = 0; i < _Answers.Count; i++)
            {
                _Answers[i].Student = student;
                _Answers[i].Exam = exam;
                _Answers[i].Question = _context.Questions.Find(_Questions[i].Id);
                if (_Answers[i].Image == null || _Answers[i].Image == "") _Answers[i].Image = "";
                if (_Answers[i].Content == null || _Answers[i].Content == "") _Answers[i].Content = "";
            }

            exam.Answers = _Answers;

            _context.Update(exam);
            await _context.SaveChangesAsync();
            /*Student student = _context.Students.Find(StudentId);
            foreach (var answer in exam.Answers)
            {
                answer.Student = student;
                answer.Question
            }
            /*var exam = await _context.Exams.Where(i => i.Id == id).Include(i => i.Students).Include(i => i.Questions).FirstOrDefaultAsync();
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);*/
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ExamEvaluate(int examId, int studentId)
        {
            Student student = _context.Students.Find(studentId);
            ViewBag.Student = student;
            Exam exam = _context.Exams.Where(i => i.Id == examId).Include(i => i.Answers.Where(a => a.Student == student)).Include(i => i.Questions).Include(i => i.Teacher).FirstOrDefault();

            return View(exam);
        }
        // [Bind("Answers")] ICollection<StudentQuestionAnswer> Answers, [Bind("Evaluations")] ICollection<StudentQuestionAnswerEvaluation> Evaluations

        [HttpPost]
        public async Task<IActionResult> ExamEvaluate(Exam exam)
        {
            //answers->evaluation->image,teacher,answer = null
            //answers->exam,image,question,student = null
            //questions->Image,title = null
            //exam->title = null

            //Student student = exam.Students.ToList()[0];
            //Student student = _context.Students.Find(exam.Students.ToList()[0].Id);
            //Teacher teacher = _context.Teachers.Find(exam.Teacher.Id);
            /*ICollection<Question> _questions = new HashSet<Question>();
            foreach (var question in exam.Questions)
            {
                _questions.Add(_context.Questions.Find(question.Id));
            }
            exam.Questions = _questions;*/

            foreach (var answer in exam.Answers)
            {
                StudentQuestionAnswerEvaluation _evalution = new StudentQuestionAnswerEvaluation();
                var _answer = _context.StudentQuestionAnswers.Find(answer.Id);
                //answer.Exam = exam;
                //answer.Image = "";
                //answer.Student = student;
                //answer.Question = _context.StudentQuestionAnswers.Find(answer.Id).Question;
                _evalution.Image = "";
                _evalution.Teacher = _context.Teachers.Find(exam.Teacher.Id);
                //_evalution.Answer = _answer;
                _evalution.Content = answer.Evaluation.Content;
                _evalution.Score = answer.Evaluation.Score;
                _evalution.IsCorrect = answer.Evaluation.IsCorrect;
                _evalution.AnswerId = _answer.Id;

                //answer.Evaluation.Image = "";
                //answer.Evaluation.Answer = answer;
                //answer.Evaluation.Teacher = exam.Teacher;

                _context.Update(_evalution);
                await _context.SaveChangesAsync();
            }

            /*ICollection<StudentQuestionAnswer> list = exam.Answers;
            string asd = "";
            exam.Students = null;
            exam.Questions = null;
            
            _context.Update(exam);
            await _context.SaveChangesAsync();

            /*Student student = _context.Students.Find(studentId);
            ViewBag.Student = student;
            Exam exam = _context.Exams.Where(i => i.Id == examId).Include(i => i.Answers.Where(a => a.Student == student)).Include(i => i.Questions).Include(i => i.Teacher).FirstOrDefault();

            return View(exam);

            foreach (var answer in Answers)
            {
                answer.Evaluation.Image = "";
                answer.Evaluation.Answer = answer;
                answer.Evaluation.Teacher = exam.Teacher;
                answer.Evaluation.AnswerId = answer.Id;
            }
            exam.Answers = Answers; */
            
            //_context.Update(exam);
            //await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EvaluationDetails(int examId, int studentId)
        {
            Student student = _context.Students.Find(studentId);
            ViewBag.Student = student;
            Exam exam = _context.Exams.Where(i => i.Id == examId).Include(i => i.Answers.Where(a => a.Student == student)).ThenInclude(i=>i.Evaluation).Include(i => i.Questions).Include(i => i.Teacher).FirstOrDefault();

            return View(exam);
        }
    }
}
