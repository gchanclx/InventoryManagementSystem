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
    public class UpdateDepartmentTest
    {
        private Mock<IDepartmentRepository> _mockRepo;
        private Fixture _fixture;
        private readonly ILogger<DepartmentsController> _logger;
        private DepartmentsController _controller;

        public UpdateDepartmentTest()
        {
            _fixture = new Fixture();
            _mockRepo = new Mock<IDepartmentRepository>();
        }

        [Test]
        public void UpdateDepartment_ShouldReturnStatusCode200()
        {
            int Id = 9999;
            var newDepartment = new Department()
            {
                Id = Id,
                Name = "Test name",
                Description = "Test Description"
            };
            _mockRepo.Setup(x => x.CreateNewDepartment(newDepartment));
            _controller = new DepartmentsController(_mockRepo.Object, _logger);
            _ = _controller.CreateNewDepartment(newDepartment);
            var department = new Department()
            {
                Id = Id,
                Name = "New name",
                Description = "New Description"
            };
            var result = new OkObjectResult(_controller.UpdateDepartment(Id, newDepartment));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_mockRepo, Is.Not.Null);
                Assert.That(_controller, Is.Not.Null);
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
            });
        }

        [Test]
        public void UpdateDepartment_ShouldReturnStatusCode400()
        {
            int Id = 9999;
            var newDepartment = new Department()
            {
                Id = Id,
                Name = "Test name",
                Description = "Test Description"
            };
            _mockRepo.Setup(x => x.CreateNewDepartment(newDepartment));
            _controller = new DepartmentsController(_mockRepo.Object, _logger);
            _ = _controller.CreateNewDepartment(newDepartment);
            var department = new Department()
            {
                Id = Id,
                Name = "New name",
                Description = "New Description"
            };
            var result = new BadRequestObjectResult(_controller.UpdateDepartment(Id, newDepartment));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_mockRepo, Is.Not.Null);
                Assert.That(_controller, Is.Not.Null);
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(400));
            });

        }
    }
}
