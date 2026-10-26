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
        
        public DBConnecter()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddUserSecrets(Assembly.GetExecutingAssembly(), Optional: true) // jeg kan ikke downloade Microsoft.Extensions.Configuration.UserSecrets. Ved ikke om i kan
            .Build();

            _connectionString = config[""];
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
