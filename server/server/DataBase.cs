using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using PhilosophyServer;

namespace PhilosophyDB
{
    class DataBase
    {
        SqlConnection sqlConnection = new SqlConnection(
            new SqlConnectionStringBuilder()
            {
                DataSource = Configuration.ServerNameDB,
                InitialCatalog = Configuration.NameDB,
                UserID = Configuration.UserNameDB,
                Password = Configuration.UserPasswordDB
            }.ConnectionString
        );

        public void openConnection()
        {
            if(sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void closeConnection()
        {
            if(sqlConnection.State == System.Data.ConnectionState.Open)
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
