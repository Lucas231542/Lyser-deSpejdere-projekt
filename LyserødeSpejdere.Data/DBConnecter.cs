using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace LyserødeSpejdere.Lib
{
    public class DBConnecter
    {
        private readonly String _connectionString;
        private SqlConnection _conn;

        // konstruktør

        public SqlConnection GetConnection()
        {
            var config = new ConfigurationBuilder()
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .Build();

            String ConnectionString = config["DefaultConnection"];
            return new SqlConnection(ConnectionString);
        }

        private void ConnectToDB()
        {
            _conn = new SqlConnection(_connectionString);
            _conn.Open();
        }

        private void DisconnectFromDB() 
        {  _conn.Close(); }
    }
}
