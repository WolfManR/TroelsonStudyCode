using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;

using static System.Console;

namespace DataProviderFactoryADONet
{
    class Program
    {
        static void Main(string[] args)
        {

            WriteLine("Fun with Data Provider Factories\n");

            string dataProvider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.ConnectionStrings["AutoLotSqlProvider"].ConnectionString;

            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProvider);

            using(DbConnection connection = factory.CreateConnection())
            {
                if (connection == null)
                {
                    ShowError("Connection");
                    return;
                }
                WriteLine($"Your connection object is a: {connection.GetType().Name}");
                connection.ConnectionString = connectionString;
                connection.Open();

                // Print out which version of SQL Server is used.
                if (connection is SqlConnection sqlConnection) WriteLine(sqlConnection.ServerVersion);

                // Make command object.
                DbCommand command = factory.CreateCommand();
                if (command == null)
                {
                    ShowError("Command");
                    return;
                }
                WriteLine($"Your command object is a: {command.GetType().Name}");
                command.Connection = connection;
                command.CommandText = "Select * From Inventory";

                // Print out data with data reader.
                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    WriteLine($"Your data reader object is a: {dataReader.GetType().Name}");
                    WriteLine("\n***** Current Inventory *****");
                    
                    while (dataReader.Read()) WriteLine($"-> Car #{dataReader["CarId"]} is a {dataReader["Make"]}.");
                }
            }
            ReadLine();
        }

        private static void ShowError(string v)
        {
            WriteLine($"There was an issue creationg the {v}");
            ReadLine();
        }
    }
}
