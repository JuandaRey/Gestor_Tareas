using System;
using Microsoft.Data.SqlClient;

namespace TaskManagerAPI
{
    class Program
    {
        static void Main()
        {
            string connectionString = "Server=DESKTOP-DO59ACT\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("✅ Conexión exitosa a SQL Server");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error de conexión:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}