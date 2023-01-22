using GAMMHomeAssignmentAPI.Database_Connection;
using GAMMHomeAssignmentAPI.Modal;
using GAMMHomeAssignmentAPI.Modal.EmployeeModal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace GAMMHomeAssignmentAPI.Controllers
{
	
	[ApiController]
	[Route("[controller]")]
	public class EmployeeController : ControllerBase
	{
		private EmployeeRepository _repo;

		public EmployeeController(IConfiguration configuration)
		{
			_repo = new EmployeeRepository(configuration);
		}

		[Authorize]
		[HttpGet("GetAllEmployee")]
		public List<Employee> GetAllEmployee()
		{
			return _repo.GetAllEmployee();
		}
		[HttpGet("GetSingleEmployee/{id}")]
		[Authorize]
		public Employee GetSingleEmployee(string id)
		{
			return _repo.GetSingleEmployee(id);
		}
		[HttpPost("SetEmployee")]
		public Response SetEmployee(Employee employee)
		{
			return _repo.SetEmployee(employee);
		}
		[HttpPost("UpdateEmployee")]
		[Authorize]
		public Response UpdateEmployee(Employee employee)
		{
			return _repo.UpdateEmployee(employee);
		}
		[HttpPost("DeleteEmployee/{id}")]
		[Authorize]
		public Response DeleteEmployee(string id)
		{
			return _repo.DeleteEmployee(id);
		}
	}
}
