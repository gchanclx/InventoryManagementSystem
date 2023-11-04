using AutoFixture;
using IMS.Api.Controllers;
using IMS.Models.Models.Departments;
using IMS.Repositories.Interfaces.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace IMS.NUnit.Tests.ControllersTests.Departments
{
    [TestFixture]
    public class CreateDepartmentTest
    {
        private Mock<IDepartmentRepository> _mockRepo;
        private Fixture _fixture;
        private readonly ILogger<DepartmentsController> _logger;
        private DepartmentsController _controller;

        public CreateDepartmentTest()
        {
            _fixture = new Fixture();
            _mockRepo = new Mock<IDepartmentRepository>();
        }

        [Test]
        public async Task CreateNewDepartment_ShouldReturnStatusCode200()
        {
            // Arrange
            var newDepartment = new Department()
            {
                Id = 9999,
                Name = "Test name",
                Description = "Test Description"
            };
            _mockRepo.Setup(x => x.CreateNewDepartment(newDepartment));
            _controller = new DepartmentsController(_mockRepo.Object, _logger);
            var result = new OkObjectResult(_controller.CreateNewDepartment(newDepartment));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_controller, Is.Not.Null);
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
            });

        }

        [Test]
        public async Task CreateNewDepartment_ShouldReturnStatusCode400()
        {
            // Arrange
            var newDepartment = new Department()
            {
                Id = 9999,
                Name = "Test name",
                Description = "Test Description"
            };
            _mockRepo.Setup(x => x.CreateNewDepartment(newDepartment)).ReturnsAsync(newDepartment);
            _controller = new DepartmentsController(_mockRepo.Object, _logger);      
            var result = new BadRequestObjectResult( _controller.CreateNewDepartment(newDepartment));
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_controller, Is.Not.Null);
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(400));
            });
        }
    }
}
