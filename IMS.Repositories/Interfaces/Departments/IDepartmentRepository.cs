using IMS.Models.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Repositories.Interfaces.Departments
{
    public interface IDepartmentRepository
    {
        public Task<IEnumerable<Department>> GetAllDepartments();
        public Task<Department> GetDepartmentsById(int id);
        public Task<Department> GetDepartmentsByName(string name);
        public Task<Department> CreateNewDepartment(Department department);
        public Task<Department> UpdateDepartment(int id, Department department);
        public Task<Department> DeleteDepartment(int id);
    }
}
