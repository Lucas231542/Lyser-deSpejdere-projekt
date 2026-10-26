using LyserødeSpejdere.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LyserødeSpejdere.Lib
{
    public class AktivitetRepo
    {
       
       
        public void Create(Bruger bruger, int AktivitetTypeId, DateTime StartTid, DateTime SlutTid, string Sted)
        {
           
        string sql = $"Insert INTO Aktivitet (AktivitetTypeId, StartTid, SlutTid, Sted) VALUES (@AktivitetTypeId, @StartTid, @SlutTid, @Sted)";
            SqlCommand cmd = new SqlCommand(sql, conn);
        if (bruger.ErLeder)
        {
                cmd.Parameters.AddWithValue("@AktivitetTypeId", AktivitetTypeId);
                cmd.Parameters.AddWithValue("@StartTid", StartTid);
                cmd.Parameters.AddWithValue("@SlutTid", SlutTid);
                cmd.Parameters.AddWithValue("@Sted", Sted);
                cmd.ExecuteNonQuery();
        }
        else
        {
                throw new Exception("er ikke leder");
        }

        }

    }
}
