using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Assignment
{
    internal class ViewDemo
    {

        static void Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-C250NV4;Database=CareBridgeDB;Trusted_Connection=True;TrustServerCertificate=True;";

            Console.WriteLine("=== HIPAA Secure Access Portal ===");
            Console.WriteLine("1. Clinical Team");
            Console.WriteLine("2. Billing Team");
            Console.WriteLine("3. Analytics Team");

            Console.Write("Select your role: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ExecuteView(connectionString, "vw_Clinical");
                    break;

                case 2:
                    ExecuteView(connectionString, "vw_Billing");
                    break;

                case 3:
                    ExecuteView(connectionString, "vw_Analytics_DeId");
                    break;

                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }

        }

        static void ExecuteView(string connectionString, string viewName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = $"SELECT * FROM {viewName}";  

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write($"{reader.GetName(i)}: {reader[i]}  ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}