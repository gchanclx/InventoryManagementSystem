using IMS.Models.Models.Departments;
using IMS.Repositories.Interfaces.Departments;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    public class DepartmentsServiceFake : IDepartmentRepository
    {
        private readonly List<Department> _departmentList;

        public DepartmentsServiceFake()
        {
            _departmentList = new List<Department>()
            {
                new Department()
                {
                    Id = 1000001, Name = "Test Name 1000001", Description = "Test Description 1000001"
                },
                new Department()
                {
                    Id = 1000002, Name = "Test Name 1000002", Description = "Test Description 1000002"
                },
                new Department()
                {
                    Id = 1000003, Name = "Test Name 1000003", Description = "Test Description 1000003"
                },
                new Department()
                {
                    Id = 1000004, Name = "Test Name 1000004", Description = "Test Description 1000004"
                },
                new Department()
                {
                    Id = 1000005, Name = "Test Name 1000005", Description = "Test Description 1000005"
                }
            };
        }

        public async Task<Department> CreateNewDepartment(Department department)
        {
            if (department != null)
            {
                _departmentList.Add(department);
                return department;
            }
            return null;
        }

        public async Task<Department> DeleteDepartment(int id)
        {
            if (id == 0)
                return null;

            var result = _departmentList.First(x => x.Id == id);
            if (result != null)
            {
                _departmentList.Remove(result);
                return result;
            }
            return null;
            
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return _departmentList;
        }

        public async Task<Department> GetDepartmentsById(int id)
        {
            return _departmentList.Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public async Task<Department> GetDepartmentsByName(string name)
        {
            return _departmentList.Where(x => x.Name == name)
                .FirstOrDefault();
        }

        public async Task<Department> UpdateDepartment(int id, Department department)
        {
            if (id == 0)
                return null;

            if (department == null)
                return null;

            var checkDepartment = _departmentList.FirstOrDefault(x => x.Id == id);
            if (checkDepartment != null)
            {
                checkDepartment.Name = department.Name;
                checkDepartment.Description = department.Description;
                return checkDepartment;
            }
            return null;
        }
    }
}
