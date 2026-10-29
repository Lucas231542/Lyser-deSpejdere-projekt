using LyserødeSpejdere.Lib;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LyserødeSpejdere.Data
{
    public class BrugerRepo : IBrugerRepo

    {
        private readonly DBConnecter _db = new DBConnecter();

        
        private Bruger LavBruger(SqlDataReader r)
            // Hjælpemetode: Laver en række fra databasen om til et bruger-objekt
        // Skrives en gang bruges 3 steder (GetAll, GetById, Login) Undgå at gentage kode
        {
            return new Bruger
            {
                BrugerId = r.GetInt32(0),
                Navn = r.GetString(1),
                Mail = r.GetString(2),
                Brugernavn = r.GetString(3),
                Adgangskode = r.GetString(4),
                Fødselsdato = r.GetDateTime(5),
                ErLeder = r.GetBoolean(6)
            };
        }

        // Read: hent alle brugere
        public List<Bruger> GetAll()
        {
            List<Bruger> brugere = new List<Bruger>();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = "SELECT BrugerId, Navn, Mail, Brugernavn, Adgangskode, Foedselsdato, ErLeder FROM Bruger";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    brugere.Add(LavBruger(r));
                }
            }

            return brugere;
        }


        // Read: Hent en bruger ud fra id 
        public Bruger? GetById(int brugerId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql =
                    "SELECT BrugerId, Navn, Mail, Brugernavn, Adgangskode, Foedselsdato, ErLeder FROM Bruger WHERE  BrugerId = @brugerId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BrugerId", brugerId);
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    return LavBruger(r);
                }
            }

            return null;
        }

        // Read: Tjek brugernavn og adgangskode ved login
        public Bruger? Login(string brugernavn, string adgangskode)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql =
                    "SELECT BrugerId, Navn, Mail, Brugernavn, Adgangskode, Foedselsdato, ErLeder FROM Bruger WHERE Brugernavn = @Brugernavn AND Adgangskode = @Adgangskode ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Brugernavn", brugernavn);
                cmd.Parameters.AddWithValue("@Adgangskode", adgangskode);
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    return LavBruger(r);
                }
            }

            return null;
        }


        // Create: Opret en ny bruger 

        public void Opret(Bruger bruger)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = " INSERT INTO Bruger (Navn, Mail, Brugernavn, Adgangskode, Foedselsdato, ErLeder)" +
                             "VALUES (@Navn, @Mail, @Brugernavn, @Adgangskode, @Foedselsdato, @ErLeder)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Navn", bruger.Navn);
                cmd.Parameters.AddWithValue("@Mail", bruger.Mail);
                cmd.Parameters.AddWithValue("@Brugernavn", bruger.Brugernavn);
                cmd.Parameters.AddWithValue("@Adgangskode", bruger.Adgangskode);
                cmd.Parameters.AddWithValue("@Foedselsdato", bruger.Fødselsdato);
                cmd.Parameters.AddWithValue("@ErLeder", bruger.ErLeder);
                cmd.ExecuteNonQuery();
            }
        }

        // Update: ret en eksisterende bruger

        public void Update(Bruger bruger)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = "UPDATE Bruger SET Navn = @Navn, Mail =@Mail, Brugernavn = @Beugernavn," +
                             "Adgangskode = @Adgangskode, Foedselsdato = @Foedselsdato, ErLeder = @ErLeder" +
                             "WHERE BrugerId = @BrugerId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Navn", bruger.Navn);
                cmd.Parameters.AddWithValue("@Mail", bruger.Mail);
                cmd.Parameters.AddWithValue("@Beugernavn", bruger.Brugernavn);
                cmd.Parameters.AddWithValue("@Adgangskode", bruger.Adgangskode);
                cmd.Parameters.AddWithValue("@Foedselsdato", bruger.Fødselsdato);
                cmd.Parameters.AddWithValue("@ErLeder", bruger.ErLeder);
                cmd.Parameters.AddWithValue("@BrugerId", bruger.BrugerId);
                cmd.ExecuteNonQuery();
            }
        }

        // Delete: Slet en bruger (Og først det, der peger på brugeren)

        public void Delete(int brugerId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM Tilmelding WHERE BrugerId = @BrugerId;" +
                             "DELETE FROM AktivitetLeder WHERE BrugerId = @BrugerId;" +
                             "DELETE FROM BrugerFaerdighed WHERE BrugerId = @BrugerId;" +
                             "DELETE FROM Bruger WHERE BrugerId = @BrugerId;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BrugerId", brugerId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
    