using GAMMHomeAssignmentAPI.Database_Connection;
using GAMMHomeAssignmentAPI.Modal;
using GAMMHomeAssignmentAPI.Modal.EmployeeModal;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace GAMMHomeAssignmentAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EmployeeController : ControllerBase
	{
		private IConfiguration _config;
		private ConnectionHelper _ch;

		public EmployeeController(IConfiguration configuration)
		{
			_config = configuration;
			_ch = new ConnectionHelper(_config);
		}

		[HttpGet("GettAllEmployee")]
		public List<Employee> GettAllEmployee()
		{
			List<Employee> objRList = new List<Employee>();
			try
			{
				_ch.Connect();
				SqlCommand cmd = new SqlCommand("getAllEmployee", _ch.DatabaseInstance());
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				_ch.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					Employee objAdd = new Employee();
					objAdd.Employee_ID = reader["Employee_ID"].ToString(); ;
					objAdd.Employee_Name = reader["Employee_Name"].ToString();
					objAdd.Employee_Designation = reader["Employee_Designation"].ToString();
					objAdd.Company_Name = reader["Company_Name"].ToString();
					objAdd.Response = new Response(200,"OK");
					objRList.Add(objAdd);
				}
				_ch.Close();
			}
			catch (Exception ex)
			{
				Employee objAdd = new Employee();
				objAdd.Response = new Response(500, ex.Message);
				objRList.Add(objAdd);
			}
			return objRList;
			

		}
	}
}
