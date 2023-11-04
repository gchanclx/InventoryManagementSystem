using IMS.Models.Models.Departments;
using IMS.Repositories.Interfaces.Departments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentsRepository;
        private readonly ILogger _logger;

        public DepartmentsController(IDepartmentRepository departmentsRepository, ILogger<DepartmentsController> logger)
        {
            _departmentsRepository = departmentsRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var result = await _departmentsRepository.GetAllDepartments();
            if (result == null)
            {
                _logger.LogError(message: $"No record found in GetAllDepartment.");
                return NotFound();
            }
            
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var result = await _departmentsRepository.GetDepartmentsById(id);
            if (result == null)
            {
                _logger.LogError(message: $"No record found in GetDepartmentById: ({id})");
                return NotFound();
            }
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewDepartment(Department department)
        {
            if (department == null)
            {
                _logger.LogError(message: $"department is null in CreateNewDepartment Controller.");
                return BadRequest();
            }

            var checkDepartment = await _departmentsRepository.GetDepartmentsByName(department.Name);
            if (checkDepartment != null)
            {
                _logger.LogError(message: $"department is existing in database. CreateNewDepartment Controller.");
                return BadRequest();
            }

            var result = await _departmentsRepository.CreateNewDepartment(department);
            if (result == null)
            {
                _logger.LogError(message: $"department cannot be created in CreateNewDepartment Controller.");
                return BadRequest();
            }
            
            return Ok(result);            
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department department)
        {
            if (id == 0)
            {
                _logger.LogError(message: $"department id is null in UpdateDepartment Controller.");
                return BadRequest();
            }

            if (department == null)
            {
                _logger.LogError(message: $"department is null in UpdateDepartment Controller.");
                return BadRequest();
            }

            var checkDepartment = await _departmentsRepository.GetDepartmentsById(id);
            if (checkDepartment == null)
            {
                _logger.LogError(message: $"department cannot be found in UpdateDepartment Controller.");
                return NotFound();
            }

            var result = await _departmentsRepository.UpdateDepartment(id, department);
            if (result == null)
            {
                _logger.LogError(message: $"department cannot be updated in UpdateDepartment Controller.");
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (id == 0)
            {
                _logger.LogError(message: $"department id is not valid in DeleteDepartment Controller.");
                return BadRequest();
            }

            var checkDepartment = await _departmentsRepository.GetDepartmentsById(id);
            if (checkDepartment == null)
            {
                _logger.LogError(message: $"department cannot be found in DeleteDepartment Controller.");
                return BadRequest();
            }

            var result = await _departmentsRepository.DeleteDepartment(id);
            if (result == null)
            {
                _logger.LogError(message: $"department cannot be deleted in DeleteDepartment Controller.");
                return BadRequest();
            }

            return Ok(result);

        }
    }
}
