namespace GAMMHomeAssignmentAPI.Modal.UserModal
{
	public class User
	{
		public string? User_ID { get; set; }
		public string? User_Name { get; set; }
		public string? Full_Name { get; set; }
		public string? Password { get; set; }
		public byte[]? PasswordHash { get; set; }
		public byte[]? PasswordSalt { get; set; }
		public string RefreshToken { get; set; } = string.Empty;
		public DateTime TokenCreated { get; set; }
		public DateTime TokenExpires { get; set; }
		public Response Response { get; set; } = new Response(0, "");
	}
}
