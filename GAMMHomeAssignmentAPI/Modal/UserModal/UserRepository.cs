using GAMMHomeAssignmentAPI.Database_Connection;
using GAMMHomeAssignmentAPI.Modal.EmployeeModal;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GAMMHomeAssignmentAPI.Modal.UserModal
{
	public class UserRepository
	{

		private ConnectionHelper _ch;
		private string _appToken;
		public UserRepository(IConfiguration configuration)
		{
			_ch = new ConnectionHelper(configuration);
			_appToken = configuration.GetSection("AppSettings:Token").Value;

		}
		public List<User> GetAllUser()
		{
			List<User> objRList = new List<User>();
			try
			{
				_ch.Connect();
				SqlCommand cmd = new SqlCommand("GetAllUser", _ch.DatabaseInstance());
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				_ch.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					User objAdd = new User();
					objAdd.User_ID = reader["User_ID"].ToString(); ;
					objAdd.User_Name = reader["User_Name"].ToString();
					objAdd.Full_Name = reader["Full_Name"].ToString();
					objAdd.Password = reader["Password"].ToString();
					objAdd.PasswordHash = reader["PasswordHash"].ToString();
					objAdd.PasswordSalt = reader["PasswordSalt"].ToString();
					objAdd.RefreshToken = reader["RefreshToken"].ToString();
					objAdd.TokenCreated = reader["TokenCreated"].ToString();
					objAdd.TokenExpires = reader["TokenExpires"].ToString();
					objAdd.Response = new Response(200, "OK");
					objRList.Add(objAdd);
				}
				_ch.Close();
			}
			catch (Exception ex)
			{
				User objAdd = new User();
				objAdd.Response = new Response(500, ex.Message);
				objRList.Add(objAdd);
			}
			return objRList;


		}
		public User GetSingleUser(string ID)
		{
			User objR = new User();
			try
			{
				_ch.Connect();
				SqlCommand cmd = new SqlCommand("GetSingleUser", _ch.DatabaseInstance());
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Employee_ID", ID);
				_ch.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					objR.User_ID = reader["User_ID"].ToString(); ;
					objR.User_Name = reader["User_Name"].ToString();
					objR.Full_Name = reader["Full_Name"].ToString();
					objR.Password = reader["Password"].ToString();
					objR.PasswordHash = reader["PasswordHash"].ToString();
					objR.PasswordSalt = reader["PasswordSalt"].ToString();
					objR.RefreshToken = reader["RefreshToken"].ToString();
					objR.TokenCreated = reader["TokenCreated"].ToString();
					objR.TokenExpires = reader["TokenExpires"].ToString();
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
		public Response RegisterUser(User obj)
		{
			Response response = new Response(0, "");
			try
			{
				_ch.Connect();
				SqlCommand cmd = new SqlCommand("SetEmployee", _ch.DatabaseInstance());
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@User_ID", obj.User_ID);
				cmd.Parameters.AddWithValue("@User_Name", obj.User_Name);
				cmd.Parameters.AddWithValue("@Full_Name", obj.Full_Name);
				cmd.Parameters.AddWithValue("@Password", obj.Password);
				cmd.Parameters.AddWithValue("@PasswordHash", obj.PasswordHash);
				cmd.Parameters.AddWithValue("@PasswordSalt", obj.PasswordSalt);
				cmd.Parameters.AddWithValue("@RefreshToken", obj.RefreshToken);
				cmd.Parameters.AddWithValue("@TokenCreated", obj.TokenCreated);
				cmd.Parameters.AddWithValue("@TokenExpires", obj.TokenExpires);
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
		public Response Login( UserDTO userDTO)
		{
			User objR = new User();
			try
			{
				_ch.Connect();
				SqlCommand cmd = new SqlCommand("GetInfoForUserLogin", _ch.DatabaseInstance());
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@User_Name ", userDTO.User_Name);
				cmd.Parameters.AddWithValue("@Password", userDTO.Password);
				_ch.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					objR.User_ID = reader["User_ID"].ToString(); ;
					objR.User_Name = reader["User_Name"].ToString();
					objR.Full_Name = reader["Full_Name"].ToString();
					objR.Password = reader["Password"].ToString();
					objR.PasswordHash = reader["PasswordHash"].ToString();
					objR.PasswordSalt = reader["PasswordSalt"].ToString();
					objR.RefreshToken = reader["RefreshToken"].ToString();
					objR.TokenCreated = reader["TokenCreated"].ToString();
					objR.TokenExpires = reader["TokenExpires"].ToString();
					objR.Response = new Response(200, "OK");
				}
				_ch.Close();
			}
			catch (Exception ex)
			{
				objR.Response = new Response(500, ex.Message);
			}
			var passHashByte = Encoding.ASCII.GetBytes(objR.PasswordHash);
			var passSaltByte = Encoding.ASCII.GetBytes(objR.PasswordSalt);
			if (objR == null || !VerifyPasswordHash(objR.Password, passHashByte, passSaltByte))
			{
				return new Response(1,"Wrong Email Or Password!");
			}

			string token = CreateToken(objR);

			return new Response(200, token);
		}
		#region Authentication
		private string CreateToken(User user)
		{
			List<Claim> claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.Full_Name),
				new Claim(ClaimTypes.Role, "User"),
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_appToken));

			var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: cred
				);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);
			return jwt;
		}
		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				return computedHash.SequenceEqual(passwordHash);
			}
		}
		#endregion
		public Response UpdateUser(User obj)
		{
			Response response = new Response(0, "");
			try
			{
				_ch.Connect();
				SqlCommand cmd = new SqlCommand("UpdateUser", _ch.DatabaseInstance());
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@User_ID", obj.User_ID);
				cmd.Parameters.AddWithValue("@User_Name", obj.User_Name);
				cmd.Parameters.AddWithValue("@Full_Name", obj.Full_Name);
				cmd.Parameters.AddWithValue("@Password", obj.Password);
				cmd.Parameters.AddWithValue("@PasswordHash", obj.PasswordHash);
				cmd.Parameters.AddWithValue("@PasswordSalt", obj.PasswordSalt);
				cmd.Parameters.AddWithValue("@RefreshToken", obj.RefreshToken);
				cmd.Parameters.AddWithValue("@TokenCreated", obj.TokenCreated);
				cmd.Parameters.AddWithValue("@TokenExpires", obj.TokenExpires);
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
		public Response DeleteUser(string id)
		{
			Response response = new Response(0, "");
			try
			{
				_ch.Connect();
				SqlCommand cmd = new SqlCommand("DeleteUser", _ch.DatabaseInstance());
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@@User_ID", id);
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
