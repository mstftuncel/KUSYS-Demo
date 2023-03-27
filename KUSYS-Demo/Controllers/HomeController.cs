using KUSYS_Demo.Data;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KUSYS_Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KUSYSContext _context;

        public HomeController(KUSYSContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StudentList()
        {
            var students = _context.Students.ToList();
            return View(students);
        }
        [HttpGet]
        public IActionResult StudentCreate()
        {         
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentCreate([Bind("Id,FirstName,LastName,Email")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Student creation successful.";
                TempData["MessageType"] = "alert-success";
                return RedirectToAction(nameof(StudentList));
            }
            else
            {
                TempData["Message"] = "Invalid input data. Please check your inputs and try again.";
                TempData["MessageType"] = "alert-danger";
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    TempData["Message"] += " " + error.ErrorMessage;
                }
            }
            // Formun doğrulaması geçersiz ise aynı view'i tekrar döndürüyoruz.
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Student successfully deleted.";
            return RedirectToAction(nameof(StudentList));
        }

        [HttpGet]
        public IActionResult UpdateStudent(int id)
        {
            var student = _context.Students.Find(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStudent(int id, [Bind("Id,FirstName,LastName,Email")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["SuccessMessage"] = "Student successfully updated.";
                return RedirectToAction(nameof(StudentList));
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid input data. Please check your inputs and try again.";
            }

            // Formun doğrulaması geçersiz ise aynı view'i tekrar döndürüyoruz.
            return View(student);
        }

        public IActionResult GetStudentDetails(int id)
        {
            var student = _context.Students.Include(s => s.StudentCourses).FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return PartialView("_StudentDetailsPartial", student);
        }

        public IActionResult MatchStudentWithCourse()
        {
            ViewBag.Courses = _context.Courses.ToList();

            return View(_context.Students.ToList());
        }

        [HttpPost]
        public IActionResult MatchStudentWithCourse(int studentId, int courseId)
        {
            // öncelikle seçilen öğrenci ve kursun kaydı var mı diye kontrol ediyoruz
            var existingMatch = _context.StudentCourses.FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseId == courseId);
            if (existingMatch != null)
            {
                // eğer böyle bir kayıt varsa kullanıcıya uyarı veriyoruz ve işlem yapmıyoruz
                TempData["Message"] = "This student is already matched with this course.";
                TempData["MessageType"] = "alert-danger";
            }
            else
            {
                // eğer kayıt yoksa yeni bir kayıt oluşturuyoruz ve kaydediyoruz
                var studentCourse = new StudentCourse
                {
                    StudentId = studentId,
                    CourseId = courseId
                };

                _context.StudentCourses.Add(studentCourse);
                _context.SaveChanges();

                TempData["Message"] = "Match has been added successfully.";
                TempData["MessageType"] = "alert-success";
            }
            TempData.Keep();
            return RedirectToAction("ListStudentCourses");
        }

        public IActionResult ListStudentCourses()
        {
            var studentCourses = _context.StudentCourses
                .Include(sc => sc.Student)
                .Include(sc => sc.Course)
                .ToList();
            ViewBag.Message = TempData["Message"];
            return View(studentCourses);
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }


        public IActionResult CourseList()
        {
            return View();
        }

        public IActionResult StudentDetails(int id)
        {
            // burada id parametresi ile seçilen öğrenciye ait detay sayfasına yönlendirme yapabilirsiniz.
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}