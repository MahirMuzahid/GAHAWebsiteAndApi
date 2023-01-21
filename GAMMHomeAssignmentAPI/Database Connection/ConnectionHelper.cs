using System.Data;
using System.Data.SqlClient;

namespace GAMMHomeAssignmentAPI.Database_Connection
{
	public class ConnectionHelper
	{
		private SqlConnection _connect;
		private IConfiguration _config;

		public ConnectionHelper( IConfiguration config)
		{
			_config= config;
			
		}

		public void Connect()
		{
			string connectionString = _config["ConnectionStrings:gammhaCS"];
			if (connectionString == null) { return; }
			_connect = new SqlConnection(connectionString);
		}
		public void Open()
		{
			_connect.Open();
		}
		public void Close()
		{
			_connect.Close();
		}

		public SqlConnection DatabaseInstance()
		{
			return _connect;
		}
	}
}
