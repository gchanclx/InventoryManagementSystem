using IMS.Models.Data;
using IMS.Models.Models.Departments;
using IMS.Repositories.Interfaces.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace IMS.Repositories.implements.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DbContextClass _dbContextClass;

        public DepartmentRepository(DbContextClass dbContextClass)
        {
            _dbContextClass = dbContextClass;
        }

        public async Task<Department> CreateNewDepartment(Department department)
        {
            var checkDepartment = await _dbContextClass.Departments.FirstOrDefaultAsync(x => x.Name == department.Name);
            if (checkDepartment == null)
            {
                var result = await _dbContextClass.Departments.AddAsync(department);
                if (result != null)
                {
                    await _dbContextClass.SaveChangesAsync();
                    return result.Entity;
                }
            }
            return null;
        }

        public async Task<Department> DeleteDepartment(int id)
        {
            var checkDepartemnt = await _dbContextClass.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (checkDepartemnt != null)
            {
                var result = _dbContextClass.Departments.Remove(checkDepartemnt);
                await _dbContextClass.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            var result = await _dbContextClass.Departments.ToListAsync();
            if (result != null)
                return result;

            return null;
        }

        public async Task<Department> GetDepartmentsById(int id)
        {
            var result = await _dbContextClass.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null) 
                return result;

            return null;
        }

        public async Task<Department> GetDepartmentsByName(string name)
        {
            var result = await _dbContextClass.Departments.FirstOrDefaultAsync(x => x.Name == name);
            if (result != null) 
                return result;

            return null;
        }

        public async Task<Department> UpdateDepartment(int id, Department department)
        {
            if (id == 0)
                return null;

            if (department == null)
                return null;

            var checkDepartment = await _dbContextClass.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (checkDepartment != null)
            {
                checkDepartment.Name = department.Name;
                checkDepartment.Description = department.Description;
                //var result = _dbContextClass.Departments.Update(checkDepartment);
                await _dbContextClass.SaveChangesAsync();

                return checkDepartment;
            }

            return null;
        }
    }
}
