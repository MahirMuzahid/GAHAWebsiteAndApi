namespace GAMMHomeAssignmentAPI.Modal.EmployeeModal
{
	public class Employee
	{
		public string? Employee_ID { get; set; }
		public string? Employee_Name { get; set; }
		public string? Employee_Designation { get; set; }
		public string? Company_Name { get; set; }
		public Response Response { get; set; } = new Response(0,"");

	}
}
