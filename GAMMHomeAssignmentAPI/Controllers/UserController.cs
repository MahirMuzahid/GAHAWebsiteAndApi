using AutoMapper;
using GAMMHomeAssignmentAPI.Database_Connection;
using GAMMHomeAssignmentAPI.Modal;
using GAMMHomeAssignmentAPI.Modal.EmployeeModal;
using GAMMHomeAssignmentAPI.Modal.UserModal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace GAMMHomeAssignmentAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private UserRepository _repo;
		private IMapper _mapper1;

		public UserController(IConfiguration configuration, IMapper mapper)
		{
			_repo = new UserRepository(configuration);
			_mapper1 = mapper;
		}

		
		[HttpGet("GetAllUser")]
		//[Authorize(Roles = "Employee")]
		public List<User> GetAllUser()
		{
			return _repo.GetAllUser();
		}
		[HttpGet("GetSingleUser/{id}")]
		//[Authorize]
		public User GetSingleUser(string id)
		{
			return _repo.GetSingleUser(id);
		}
		[HttpPost("RegisterUser")]
		public Response RegisterUser(RegisterUserDTO registerUserDTO)
		{
			//if (!_repo.IsThisUserNameExits(registerUserDTO.User_Name)) return new Response(500, "User Name already exists");
			User user = new User();
			user.User_Name = registerUserDTO.User_Name;
			user.User_ID = registerUserDTO.User_ID;
			user.Password = registerUserDTO.Password;
			user.Full_Name= registerUserDTO.Full_Name;
			user.RefreshToken = "N/A";
			user.TokenCreated = "N/A";
			user.TokenExpired = "N/A";
			var result = _repo.RegisterUser(user);
			return result;
		}

		[HttpPost("Login")]
		public Response Login(UserDTO userDTO)
		{
			return _repo.Login(userDTO);
		}
		[HttpPost("UpdateUser")]
		//[Authorize]
		public Response UpdateUser(User employee)
		{
			return _repo.UpdateUser(employee);
		}
		[HttpPost("DeleteUser/{id}")]
		//[Authorize]
		public Response DeleteUser(string id)
		{
			return _repo.DeleteUser(id);
		}
	}
}
