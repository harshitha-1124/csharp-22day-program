using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SQL_Assignement
{
    class Program
    {
        static void Main(string[] args)
        {
            // string connectionString = "Server=(localdb)\\MSSQLLocalDB ;Database=CareBridgeDB;Trusted_Connection=True;";
            //string connectionString = "Server=DESKTOP-C250NV4;Database=CareBridgeDB;Trusted_Connection=True;";

            string connectionString = "Server=DESKTOP-C250NV4;Database=CareBridgeDB;Trusted_Connection=True;TrustServerCertificate=True;";

            Console.WriteLine("Select an option:");
            Console.WriteLine("1. 30-Day Readmissions");
            Console.WriteLine("2. High-Risk Patients");
            Console.WriteLine("3. Provider Workload");
            Console.WriteLine("4. Revenue Analysis");

            Console.Write("Enter choice: ");
            int a = Convert.ToInt32(Console.ReadLine()); // ✅ take input

            switch (a)
            {
                case 1:
                    ExecuteProcedure(connectionString, "sp_30_day_readmission");
                    break;

                case 2:
                    ExecuteProcedure(connectionString, "sp_high_risk_patients");
                    break;

                case 3:
                    ExecuteProcedure(connectionString, "sp_high_workload_provider");
                    break;

                case 4:
                    ExecuteProcedure(connectionString, "SP_CLAIMS"); // reused ✅
                    break;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }

        static void ExecuteProcedure(string connectionString, string procedureName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(procedureName, conn);
                cmd.CommandType = CommandType.StoredProcedure;

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