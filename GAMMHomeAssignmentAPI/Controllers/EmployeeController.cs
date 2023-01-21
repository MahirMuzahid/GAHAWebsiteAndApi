using GAMMHomeAssignmentAPI.Database_Connection;
using GAMMHomeAssignmentAPI.Modal;
using GAMMHomeAssignmentAPI.Modal.EmployeeModal;
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

		[HttpGet("GetAllEmployee")]
		public List<Employee> GetAllEmployee()
		{
			return _repo.GetAllEmployee();
		}
		[HttpGet("GetSingleEmployee")]
		public Employee GetSingleEmployee(Employee employee)
		{
			return _repo.GetSingleEmployee(employee.Employee_ID);
		}
		[HttpGet("SetEmployee")]
		public Response SetEmployee(Employee employee)
		{
			return _repo.SetEmployee(employee);
		}
		[HttpGet("UpdateEmployee")]
		public Response UpdateEmployee(Employee employee)
		{
			return _repo.UpdateEmployee(employee);
		}
		[HttpGet("DeleteEmployee/{id}")]
		public Response DeleteEmployee(string id)
		{
			return _repo.DeleteEmployee(id);
		}
	}
}
