using Microsoft.Data.SqlClient;
using System;
using System.Windows;
public sealed class DatabaseConnection
{
	private static readonly DatabaseConnection instance = new DatabaseConnection();
	private SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
	
	private SqlConnection connection;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static DatabaseConnection()
	{
	}

	private DatabaseConnection()
	{
		builder.ConnectionString = $"Data Source=tcp:{Environment.GetEnvironmentVariable("DB_IP")},1433; User ID={Environment.GetEnvironmentVariable("DB_USER")};" +
			$" Password= {Environment.GetEnvironmentVariable("DB_PASS")};Initial Catalog = {Environment.GetEnvironmentVariable("DB_SCHEMA")};TrustServerCertificate=True";
		// Initialize your database connection here
		connection = new SqlConnection(builder.ConnectionString);
		MessageBox.Show(builder.ConnectionString);
	}

	public static DatabaseConnection Instance
	{
		get
		{
			return instance;
		}
	}

	public SqlConnection GetConnection()
	{
		// Check if connection is closed or disposed, then reopen or recreate it
		if (connection == null || connection.State == System.Data.ConnectionState.Closed)
		{
			connection = new SqlConnection(builder.ConnectionString);
		}
		else if (connection.State == System.Data.ConnectionState.Broken)
		{
			connection.Close();
			connection.Open();
		}

		return connection;
	}
}