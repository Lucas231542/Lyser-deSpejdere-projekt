using LyserødeSpejdere.Lib;
using Microsoft.Data.SqlClient;

namespace LyserødeSpejdere.Data
{
    public class TilmeldingRepo : ITilmeldingRepo
    {
        private readonly DBConnecter _db = new  DBConnecter();
        
        // Create: Tilmeld en spejder til en aktivitet
        public void Tilmeld(int brugerId, int aktivitetId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Tilmelding (BrugerId, AktivitetId) VALUES (@BrugerId, @AktivitetId)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BrugerId", brugerId);
                cmd.Parameters.AddWithValue("@AktivitetId", aktivitetId);
                cmd.ExecuteNonQuery();
            }
        }
        
        // Delete: Frameld en spejder fra en aktivitet
        public void Frameld(int brugerId, int aktivitetId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM Tilmelding WHERE BrugerId = @BrugerId AND AktivitetId = @AktivitetId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BrugerId", brugerId);
                cmd.Parameters.AddWithValue("@AktivitetId", aktivitetId);
                cmd.ExecuteNonQuery();
            }
        }
        
        // Read: Alle aktiviteter en spejder er tilmeldt (Mine aktiviteter)

        public List<Aktivitet> HentMineAktiviteter(int brugerId)
        {
            List<Aktivitet> liste = new List<Aktivitet>();
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = "SELECt a.AktivitetId, a.AktivitetTypeId, a.StartTid, a.SlutTid, a.Sted" +
                             "FROM Aktivitet a" +
                             "JOIN Tilmelding t ON a.AktivitetId = t.AktivitetId" +
                             "WHERE t.BrugerId = @BrugerId" +
                             "ORDER BY a.StartTid";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BrugerId", brugerId);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    liste.Add(new Aktivitet
                    {
                        AktivitetId = r.GetInt32(0),
                        AktivitetTypeId = r.GetInt32(1),
                        StartTid = r.GetDateTime(2),
                        SlutTid = r.GetDateTime(3),
                        Sted = r.IsDBNull(4) ? "" : r.GetString(4)

                    });
                }

            }

            return liste;
        }
        
        //Read: Alle spejdere tilmeldt en aktivitet (Lederens deltagerliste)

        public List<Bruger> HentDeltagere(int aktivitetId)
        {
            List<Bruger> Liste = new List<Bruger>();
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = "SELECT b.BrugerId, b.Navn, b.Mail, b.Brugernavn, b.Adgangskode, b.Foedselsdato, b.ErLeder" +
                             "FROM Bruger b " +
                             "JOIN Tilmelding t ON b.BrugerId = t.BrugerId" +
                             "WHERE t.AktivitetId = @AktivitetId" +
                             "ORDER BY b.Navn";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@aktivitetId", aktivitetId);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    Liste.Add(new Bruger
                    {
                        BrugerId = r.GetInt32(0),
                        Navn = r.GetString(1),
                        Mail = r.GetString(2),
                        Brugernavn = r.GetString(3),
                        Adgangskode = r.GetString(4),
                        Fødselsdato = r.GetDateTime(5),
                        ErLeder = r.GetBoolean(6)
                    });
                }
            }

            return Liste;

        }
        
        //Read:  Hvor mange er tilmeldt en aktivitet (Til max-tjek)

        public int HentDeltagerAntal(int aktivitetId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();    
                string sql = "SELECT COUNT(*) FROM Tilmelding WHERE AktivitetId = @AktivitetId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AktivitetId", aktivitetId);
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}
