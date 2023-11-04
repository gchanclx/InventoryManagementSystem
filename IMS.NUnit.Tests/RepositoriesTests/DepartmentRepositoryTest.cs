using AutoFixture;
using IMS.Models.Data;
using IMS.Models.Models.Departments;
using IMS.Repositories.implements.Departments;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace IMS.NUnit.Tests.RepositoriesTests
{
    [TestFixture]
    public class DepartmentRepositoryTest : IDisposable
    {
        protected readonly DbContextClass _dbContext;
        private Fixture _fixture;

        public DepartmentRepositoryTest()
        {
            _fixture = new Fixture();
            var options = new DbContextOptionsBuilder<DbContextClass>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new DbContextClass(options);
            _dbContext.Database.EnsureCreated();

            var department = _fixture.CreateMany<Department>(5).ToList();
            _dbContext.Departments.AddRange(department);
            _dbContext.SaveChanges();
        }

        [Test]
        public void GetDepartmentList_ReturnAllDepartments()
        {
            // Arrange
            var sut = new DepartmentRepository(_dbContext);
            var departmentListTest = sut.GetAllDepartments();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_dbContext, Is.Not.Null);
                Assert.That(sut, Is.Not.Null);
                Assert.That(departmentListTest.Result.Count(), Is.GreaterThan(0));
                Assert.That(departmentListTest.Result.Any(), Is.True);
            });
        }

        [Test]
        public void GetDepartmentById_ReturnDepartment()
        {
            // Arrange 
            int id = 99999;
            string deptName = "Testing department";
            string deptDescription = "Testing department Description";
            var sut = new DepartmentRepository(_dbContext);

            Department dept = new()
            {
                Id = id,
                Name = deptName,
                Description = deptDescription
            };
            _ = sut.CreateNewDepartment(dept);

            var departmentTest = sut.GetDepartmentsById(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_dbContext, Is.Not.Null);
                Assert.That(sut, Is.Not.Null);
                Assert.That(departmentTest, Is.Not.Null);
                Assert.That(departmentTest.Result.Id, Is.EqualTo(id));
                Assert.That(departmentTest.Result.Name, Is.EqualTo(deptName));
                Assert.That(departmentTest.Result.Description, Is.EqualTo(deptDescription));
            });
        }

        [Test]
        public void GetDepartmentByName_ReturnDepartment()
        {
            // Arrange 
            int id = 99999;
            string deptName = "Testing department";
            string deptDescription = "Testing department Description";
            var sut = new DepartmentRepository(_dbContext);
            
            Department dept = new()
            {
                Id = id,
                Name = deptName,
                Description = deptDescription
            };

            _ = sut.CreateNewDepartment(dept);

            var departmentTest = sut.GetDepartmentsByName(deptName);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_dbContext, Is.Not.Null);
                Assert.That(sut, Is.Not.Null);
                Assert.That(departmentTest, Is.Not.Null);
                Assert.That(departmentTest.Result.Id, Is.EqualTo(id));
                Assert.That(departmentTest.Result.Name, Is.EqualTo(deptName));
                Assert.That(departmentTest.Result.Description, Is.EqualTo(deptDescription));
            });
        }

        [Test]
        public void CreateNewDepartment_ReturnTheNewDepartemnt()
        {
            // Arrange
            int id = 999999;
            string deptName = "New Department Testing";
            string deptDescription = "New Department Description";

            var sut = new DepartmentRepository( _dbContext);

            Department dept = new()
            {
                Id = id,
                Name = deptName,
                Description = deptDescription
            };

            _ = sut.CreateNewDepartment(dept);

            var departmentTest = sut.GetDepartmentsById(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_dbContext, Is.Not.Null);
                Assert.That(sut, Is.Not.Null);
                Assert.That(departmentTest, Is.Not.Null);
                Assert.That(departmentTest.Result.Id, Is.EqualTo(id));
                Assert.That(departmentTest.Result.Name, Is.EqualTo(deptName));
                Assert.That(departmentTest.Result.Description, Is.EqualTo(deptDescription));
            });
        }

        [Test]
        public void UpdateDepartment_ShouldReturnAmendedDepartment()
        {
            // Arrange
            int id = 99999;
            string deptName = "Department Name";
            string deptDescription = "Department Description";

            var sut = new DepartmentRepository(_dbContext);

            Department dept = new()
            {
                Id = id,
                Name = deptName,
                Description = deptDescription
            };

            _ = sut.CreateNewDepartment(dept);

            Department updateDept = new()
            {
                Id = id,
                Name = "Updated Department Testing",
                Description = "Updated Description Testing"
            };
            _ = sut.UpdateDepartment(id, updateDept);
            var result = sut.GetDepartmentsById(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_dbContext, Is.Not.Null);
                Assert.That(sut, Is.Not.Null);
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Result.Id, Is.EqualTo(id));
                Assert.That(result.Result.Name, Is.EqualTo(updateDept.Name));
                Assert.That(result.Result.Description, Is.EqualTo(updateDept.Description));
            });
        }

        [Test]
        public void DeleteDepartment_ShouldReturnDeletedDepartment()
        {
            // Arrange
            int id = 99999;
            string deptName = "Department Name";
            string deptDescription = "Department Description";

            var sut = new DepartmentRepository(_dbContext);

            Department dept = new()
            {
                Id = id,
                Name = deptName,
                Description = deptDescription
            };

            _ = sut.CreateNewDepartment(dept);

            _ = sut.DeleteDepartment(id);
            var result = sut.GetDepartmentsById(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_dbContext, Is.Not.Null);
                Assert.That(sut, Is.Not.Null);
                Assert.That(result, Is.Not.Null);
            });
        }

        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Database.EnsureDeleted();
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose() 
        { 
            Dispose(true); 
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
