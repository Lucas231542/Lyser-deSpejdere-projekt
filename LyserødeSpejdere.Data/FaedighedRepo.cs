using System;
using System.Collections.Generic;
using System.Text;
using LyserødeSpejdere.Lib;
using Microsoft.Data.SqlClient;

namespace LyserødeSpejdere.Data
{
    
    {
        public class FaedighedRepo : IFaerdighedRepo
        {
            private readonly DBConnecter  _db = new DBConnecter();

            public void Create(string navn)
            {
                using (SqlConnection conn = _db.GetConnection())
                {


                    conn.Open();

                    string sql =
                        "INSERT INTO Faerdighed (Navn) VALUES (@Navn)";

                    using SqlCommand command =
                        new SqlCommand(sql, conn;

                    command.Parameters.AddWithValue("@Navn", navn);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
