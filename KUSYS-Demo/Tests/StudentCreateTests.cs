using KUSYS_Demo.Models;
using KUSYS_Demo.Controllers;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NuGet.ContentModel;
using KUSYS_Demo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq.Language.Flow;

namespace KUSYS_Demo.Tests
{
    public class StudentCreateTests
    {
        [Fact]
        public async Task Student_Create_Should_Return_RedirectToActionResult()
        {
            // Arrange
            var student = new Student
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com"
            };

            var options = new DbContextOptionsBuilder<KUSYSContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var mockContext = new Mock<KUSYSContext>(options);
            mockContext.Setup(m => m.Add(It.IsAny<Student>())).Returns((Student student) =>
            {
                var entityEntry = new Mock<EntityEntry<Student>>(student);
                entityEntry.Setup(e => e.State).Returns(EntityState.Added);
                return entityEntry.Object;
            });

            mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

            var loggerMock = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockContext.Object, loggerMock.Object);

            // Act
            var result = await controller.StudentCreate(student);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }
    }

}
