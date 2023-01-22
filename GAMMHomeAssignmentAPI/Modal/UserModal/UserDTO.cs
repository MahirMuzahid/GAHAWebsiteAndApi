﻿namespace GAMMHomeAssignmentAPI.Modal.UserModal
{
	public class UserDTO
	{
		public string? User_Name { get; set; }
		public string? Password { get; set; }

		public UserDTO(string username, string password )
		{
			User_Name = username;
			Password = password;
		}
	}
}
