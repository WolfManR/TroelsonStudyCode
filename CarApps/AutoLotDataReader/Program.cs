using System.Data.SqlClient;

using static System.Console;

namespace AutoLotDataReader
{
    class Program
    {
        static void FirstVersion()
        {
            WriteLine("***** Fun with Data Readers *****\n");
            // Create and open a connection.

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = @"Data Source=(localdb)\mssqllocaldb;Integrated Security=true;" +
                                               "Initial Catalog=AutoLot";
                connection.Open();

                // Create a SQL command object.
                string sql = "Select * From Inventory";
                SqlCommand myCommand = new SqlCommand(sql, connection);

                // Obtain a data reader a la ExecuteReader().
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    // Loop over the results.
                    while (myDataReader.Read()) WriteLine($"-> Make: {myDataReader["Make"]}, " +
                                                             $"PetName: {myDataReader["PetName"]}, " +
                                                             $"Color: {myDataReader["Color"]}.");
                }
            }
            ReadLine();
        }

        static void Main(string[] args)
        {
        }
    }
}
