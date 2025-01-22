using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ContactManagement.Tests
{
	public class DatabaseConnectionTests
	{
		private readonly string _connectionString;

		public DatabaseConnectionTests()
		{
			_connectionString = "Server=fiap-lucas.database.windows.net;Database=ContactManagment;User Id=dbapp;Password=Fiap@2025;";
		}

		[Fact]
		public void Should_ConnectToDatabase_Successfully()
		{
			// Realizar o teste de conexão
			bool result = TestConnection();

			// Validar o resultado
			Assert.True(result, "Não foi possível conectar ao banco de dados");
		}

		public bool TestConnection()
		{
			try
			{
				using (var connection = new SqlConnection(_connectionString))
				{
					connection.Open();
					return connection.State == System.Data.ConnectionState.Open;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erro ao conectar ao banco de dados: {ex.Message}");
				Console.WriteLine($"Detalhes do erro: {ex.StackTrace}");

				return false;
			}
		}
	}
}
