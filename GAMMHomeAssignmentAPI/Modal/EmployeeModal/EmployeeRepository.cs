using AutoMapper;
using GAMMHomeAssignmentAPI.Database_Connection;
using System.Data.SqlClient;

namespace GAMMHomeAssignmentAPI.Modal.EmployeeModal
{
	public class EmployeeRepository
	{
		private ConnectionHelper _ch;

		public EmployeeRepository(IConfiguration configuration)
		{
			_ch = new ConnectionHelper(configuration);		
		}
		public List<Employee> GetAllEmployee()
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
					objAdd.Response = new Response(200, "OK");
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
		public Employee GetSingleEmployee(string ID)
		{
			Employee objR = new Employee();
			try
			{
				_ch.Connect();
				SqlCommand cmd = new SqlCommand("GetSingleEmployee", _ch.DatabaseInstance());
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Employee_ID", ID);
				_ch.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					objR.Employee_ID = reader["Employee_ID"].ToString(); ;
					objR.Employee_Name = reader["Employee_Name"].ToString();
					objR.Employee_Designation = reader["Employee_Designation"].ToString();
					objR.Company_Name = reader["Company_Name"].ToString();
					objR.Response = new Response(200, "OK");
				}
				_ch.Close();
			}
			catch (Exception ex)
			{
				objR.Response = new Response(500, ex.Message);
			}
			return objR;
		}
		public Response SetEmployee(Employee obj)
		{
			Response response = new Response(0,"");
			try
			{
				_ch.Connect();
				SqlCommand cmd = new SqlCommand("SetEmployee", _ch.DatabaseInstance());
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Employee_ID", obj.Employee_ID);
				cmd.Parameters.AddWithValue("@Employee_Name", obj.Employee_Name);
				cmd.Parameters.AddWithValue("@Employee_Designation", obj.Employee_Designation);
				cmd.Parameters.AddWithValue("@Company_Name", obj.Company_Name);
				_ch.Open();
				int i = cmd.ExecuteNonQuery();
				if (i != 0)
				{
					response.Message = "Succesfull!";
					response.Status = 200;
				}
				else
				{
					response.Message = "Unsuccesfull!";
					response.Status = 0;
				}
				_ch.Close();
			}
			catch (Exception ex)
			{
				response.Message = ex.Message;
				response.Status = 500;
			}
			return response;
		}
		public Response UpdateEmployee(Employee obj)
		{
			Response response = new Response(0, "");
			try
			{
				_ch.Connect();
				SqlCommand cmd = new SqlCommand("UpdateEmployee", _ch.DatabaseInstance());
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Employee_ID", obj.Employee_ID);
				cmd.Parameters.AddWithValue("@Employee_Name", obj.Employee_Name);
				cmd.Parameters.AddWithValue("@Employee_Designation", obj.Employee_Designation);
				cmd.Parameters.AddWithValue("@Company_Name", obj.Company_Name);
				_ch.Open();
				int i = cmd.ExecuteNonQuery();
				if (i != 0)
				{
					response.Message = "Succesfull!";
					response.Status = 200;
				}
				else
				{
					response.Message = "Unsuccesfull!";
					response.Status = 0;
				}
				_ch.Close();
			}
			catch (Exception ex)
			{
				response.Message = ex.Message;
				response.Status = 500;
			}
			return response;
		}
		public Response DeleteEmployee(string id)
		{
			Response response = new Response(0, "");
			try
			{
				_ch.Connect();
				SqlCommand cmd = new SqlCommand("DeleteEmployee", _ch.DatabaseInstance());
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Employee_ID", id);
				_ch.Open();
				int i = cmd.ExecuteNonQuery();
				if (i != 0)
				{
					response.Message = "Succesfull!";
					response.Status = 200;
				}
				else
				{
					response.Message = "Unsuccesfull!";
					response.Status = 0;
				}
				_ch.Close();
			}
			catch (Exception ex)
			{
				response.Message = ex.Message;
				response.Status = 500;
			}
			return response;
		}
	}
}
