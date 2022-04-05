using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PhilosophyApi
{
    public class DataBase
    {
        public class DataBaseConnectionOptions
        {
            public const string DataBaseConnection = "DataBaseConnection";

            public string NameDB { get; set; } = String.Empty;
            public string UserNameDB { get; set; } = String.Empty;
            public string UserPasswordDB { get; set; } = String.Empty;
            public string ServerNameDB { get; set; } = String.Empty;

        }

        DataBaseConnectionOptions DB;

        public  SqlConnection sqlConnection;

        public DataBase(IConfiguration configuration)
        {
            DB = new DataBaseConnectionOptions();

            configuration.GetSection(DataBaseConnectionOptions.DataBaseConnection).Bind(DB);

            sqlConnection = new SqlConnection(
            new SqlConnectionStringBuilder()
            {
                DataSource = DB.ServerNameDB,
                InitialCatalog = DB.NameDB,
                UserID = DB.UserNameDB,
                Password = DB.UserPasswordDB
            }.ConnectionString
        );
        }

        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection getConnection()
        {
            return sqlConnection;
        }

    
    }
}
