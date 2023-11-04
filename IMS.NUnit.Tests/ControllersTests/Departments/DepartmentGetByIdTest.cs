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
    public class DepartmentGetByIdTest
    {
        private Mock<IDepartmentRepository> _mockRepo;
        private Fixture _fixture;
        private readonly ILogger<DepartmentsController> _logger;
        private DepartmentsController _controller;

        public DepartmentGetByIdTest()
        {
            _fixture = new Fixture();
            _mockRepo = new Mock<IDepartmentRepository>();
        }

        [Test]
        public void GetDepartmentsById_ShouldReturnStatusCode200()
        {

            // Arrange
            var department = _fixture.Create<Department>();
            _mockRepo.Setup(x => x.GetDepartmentsById(It.IsAny<int>())).ReturnsAsync(department);
            _controller = new DepartmentsController(_mockRepo.Object, _logger);
            var result = new OkObjectResult(_controller.GetDepartmentById(It.IsAny<int>()));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_controller, Is.Not.Null);
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
            });
        }

        [Test]
        public void GetDepartmentById_ShouldReturnStatusCode404()
        {
            var department = new Department();
            _mockRepo.Setup(x => x.GetDepartmentsById(It.IsAny<int>())).ReturnsAsync(department);
            _controller = new DepartmentsController(_mockRepo.Object, _logger);
            var result = new NotFoundObjectResult(_controller.GetDepartmentById(It.IsAny<int>()));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_controller, Is.Not.Null);
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(404));
            });

        }
    }
}
