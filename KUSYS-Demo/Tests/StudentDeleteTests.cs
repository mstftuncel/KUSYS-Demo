using KUSYS_Demo.Controllers;
using KUSYS_Demo.Data;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace KUSYS_Demo.Tests
{
public class StudentDeleteTests
    {
        //private readonly KUSYSContext _context;
        //private readonly HomeController _controller;

        //public StudentDeleteTests()
        //{
        //    var options = new DbContextOptionsBuilder<KUSYSContext>()
        //        .UseInMemoryDatabase(databaseName: "test_database")
        //        .Options;
        //    _context = new KUSYSContext(options);
        //    _controller = new HomeController(new StudentRepository(_context), Mock.Of<ILogger<HomeController>>());
        //}

        //[Fact]
        //public async Task DeleteStudent_Should_Delete_Student_When_Exists()
        //{
        //    // Arrange
        //    var student = new Student { Id = 5, FirstName = "John", LastName = "Doe", Email = "j.doe@koc.com" };
        //    await _context.Students.AddAsync(student);
        //    await _context.SaveChangesAsync();

        //    // Act
        //    var result = await _controller.DeleteStudent(student.Id) as RedirectToActionResult;

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal("StudentList", result.ActionName);

        //    Assert.True(_controller.TempData.ContainsKey("SuccessMessage"));
        //    Assert.Equal("Student successfully deleted.", _controller.TempData["SuccessMessage"]);

        //    var mockRepo = new Mock<IStudentRepository>();
        //    mockRepo.Setup(repo => repo.GetByIdAsync(student.Id)).ReturnsAsync(student);
        //    mockRepo.Setup(repo => repo.Remove(student));
        //    mockRepo.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(1);

        //    await _controller.DeleteStudent(student.Id);

        //    mockRepo.Verify(repo => repo.GetByIdAsync(student.Id), Times.Once);
        //    mockRepo.Verify(repo => repo.Remove(student), Times.Once);
        //    mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        //}
    }
}
