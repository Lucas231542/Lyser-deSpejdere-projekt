using LyserødeSpejdere.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LyserødeSpejdere.Lib
{
    public class AktivitetRepo : IAktivitetRepo
    {
        private readonly DBConnecter _db = new DBConnecter();

        public List<Aktivitet> GetAllaktivitet()
        {
            List<Aktivitet> AktivitetListe = new List<Aktivitet>();
            using (SqlConnection conn = _db.GetConnection()) // GetAlleaktiviteter bruger metoden GetConnection fra DBConnecter som opretter forbindelse til Databasen
            {

                conn.Open(); // connection bliver åbnet

                string sql = $"SELECT AktivitetId, AktivitetTypeId, StartTid, SlutTid, Sted FROM Aktivitet"; // SELECT vælger hvilke parameter der skal vises med metoden
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();



                

                while (r.Read())
                {
                    Aktivitet aktivitet = new Aktivitet
                    {
                        AktivitetId = r.GetInt32(0),
                        AktivitetTypeId = r.GetInt32(1),
                        StartTid = r.GetDateTime(2),
                        SlutTid = r.GetDateTime(3),
                        Sted = r.GetString(4)
                    };
                    AktivitetListe.Add(aktivitet); // mens While løkken kører bliver aktiviteter tilføjet til listen indtil der ikke er flere aktiviteter tilbage
                }


                return AktivitetListe;


            }
        }

        public Aktivitet GetAktivitetById(int AktivitetId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
            conn.Open();
                string sql = $"SELECT AktivitetId, AktivitetTypeId, StartTid, SlutTid, Sted FROM Aktivitet WHERE AktivitetId = @AktivitetId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AktivitetId", AktivitetId);
                SqlDataReader r = cmd.ExecuteReader();



                if (r.Read())
                {
                   return new Aktivitet
                    {
                        AktivitetId = r.GetInt32(0),
                        AktivitetTypeId = r.GetInt32(1),
                        StartTid = r.GetDateTime(2),
                        SlutTid = r.GetDateTime(3),
                        Sted = r.GetString(4)
                    };
                }
                return null;
            }

        }



        public void Create(Bruger bruger, int AktivitetTypeId, DateTime StartTid, DateTime SlutTid, string Sted) 
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
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




        public void Update(DateTime StartTid, DateTime SlutTid, string Sted)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = $"UPDATE Aktivitet SET StartTid = @StartTid, SlutTid = @SlutTid, Sted = @Sted WHERE AktivitetId = @AktivitetId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StartTid", StartTid);
                cmd.Parameters.AddWithValue("@SlutTid", SlutTid);
                cmd.Parameters.AddWithValue("@Sted", Sted);
                cmd.ExecuteNonQuery();
            }
        }


        public void Delete(int AktivitetId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
            conn.Open();
                string sql = $"Delete FROM Aktivitet WHERE AktivitetId = @AktivitetId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AktivitetId", AktivitetId);
                cmd.ExecuteNonQuery();

            }
        }


        
    }
}
