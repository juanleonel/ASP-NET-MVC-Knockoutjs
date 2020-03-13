using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace Domain.Entities
{
    public class BootAppConnection
    {
		public static SqlConnection CreateConnection()
		{
			var connection = new SqlConnection(GetConnectionString());

			return connection;
		}
		public static string GetConnectionString()
		{
			return ConfigurationManager.ConnectionStrings["BootsCN"].ConnectionString;
		}
	}
}
