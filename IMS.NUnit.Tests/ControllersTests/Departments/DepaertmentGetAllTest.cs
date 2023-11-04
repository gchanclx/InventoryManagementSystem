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
    public class DepaertmentGetAllTest
    {
        private Mock<IDepartmentRepository> _mockRepo;
        private DepartmentsController _controller;
        private readonly ILogger<DepartmentsController> _logger;
        private Fixture _fixture;

        public DepaertmentGetAllTest()
        {
            _fixture = new Fixture();
            _mockRepo = new Mock<IDepartmentRepository>();            
        }

        [Test]
        public void GettAllDepartments_ShouldReturnStatusCode200()
        {

            // Arrange
            var departmentList = _fixture.CreateMany<Department>(5);

            _mockRepo.Setup(x => x.GetAllDepartments()).ReturnsAsync(departmentList);
            _controller = new DepartmentsController(_mockRepo.Object, _logger);
            var result = new OkObjectResult(_controller.GetAllDepartments());

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_controller, Is.Not.Null);
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
            });
        }

        [Test]
        public void GetAllDepartments_ShouldReturnStatusCode404()
        {            
            var department = new  List<Department>();
            _mockRepo.Setup(x => x.GetAllDepartments()).ReturnsAsync(department);
            _controller = new DepartmentsController(_mockRepo.Object, _logger);
            var result = new NotFoundObjectResult(_controller.GetAllDepartments());
            
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
