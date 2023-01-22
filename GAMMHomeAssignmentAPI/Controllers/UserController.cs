﻿using AutoMapper;
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

		[Authorize]
		[HttpGet("GetAllUser")]
		public List<User> GetAllUser()
		{
			return _repo.GetAllUser();
		}
		[HttpGet("GetSingleUser/{id}")]
		[Authorize]
		public User GetSingleUser(string id)
		{
			return _repo.GetSingleUser(id);
		}
		[HttpPost("RegisterUser")]
		public Response RegisterUser(RegisterUserDTO registerUserDTO)
		{
			var user = _mapper1.Map<User>(registerUserDTO);
			var result = _repo.RegisterUser(user);

			if(result.Status != 200) return result;

			return Login(new UserDTO(user.User_Name, user.Password));
		}

		[HttpPost("Login")]
		public Response Login(UserDTO userDTO)
		{
			return _repo.Login(userDTO);
		}
		[HttpPost("UpdateUser")]
		[Authorize]
		public Response UpdateUser(User employee)
		{
			return _repo.UpdateUser(employee);
		}
		[HttpPost("DeleteUser/{id}")]
		[Authorize]
		public Response DeleteUser(string id)
		{
			return _repo.DeleteUser(id);
		}
	}
}