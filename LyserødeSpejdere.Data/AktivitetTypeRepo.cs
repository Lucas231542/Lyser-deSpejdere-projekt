using LyserødeSpejdere.Lib;
using Microsoft.Data.SqlClient;

namespace LyserødeSpejdere.Data
{
    public class AktivitetTypeRepo : IAktivitetTypeRepo
    {
        private readonly DBConnecter _db = new  DBConnecter();
        
        // Hjælpemetode: En række = et aktivitetType-objekt
        // Null i MaxDeltagere/MinAlder bliver til 0 = "Ingen grænse"

        private AktivitetType LavType(SqlDataReader r)
        {
            return new AktivitetType
            {
                AktivitetTypeId = r.GetInt32(0),
                Navn = r.GetString(1),
                Beskrivelse = r.IsDBNull(2) ? "" : r.GetString(2),
                MaxDeltagere = r.IsDBNull(3) ? 0 : r.GetInt32(3),
                MinAlder = r.IsDBNull(4) ? 0 : r.GetInt32(4),
            };
        }
        
        // Read: alle  aktivitetstyper
        public List<AktivitetType> GetAll()
        {
            List<AktivitetType> liste = new List<AktivitetType>();
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = "SELECT AktivitetTypeId, Navn, Beskrivelse, MaxDeltagere, MinAlder FROM AktivitetType ORDER BY Navn";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    liste.Add(LavType(r));
                }
            }
            return liste;
        }
        
        // Read: en aktivitetstype ud fra id
        public AktivitetType? GetById(int aktivitetTypeId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql =
                    "SELECT AktivitetTypeId, Navn, Beskrivelse, MaxDeltagere, MinAlder FROM AktivitetType WHERE AktivitetTypeId = @AktivitetTypeId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AktivitetTypeId", aktivitetTypeId);
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    return LavType(r);
                }
            }

            return null;
        }
        
        // Read: Hvilke tegn kræver denne aktivitetstype?

        public List<Faerdighed> HentKrav(int aktivitetTypeId)
        {
            List<Faerdighed> liste = new List<Faerdighed>();
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = "SELECT f.FaerdighedId, f.Navn " +
                             "FROM Faerdighed f " +
                             "JOIN AktivitetTypeKrav K ON f.FaerdighedId = k.FaerdighedId " +
                             "WHERE k.AktivitetTypeId = @AktivitetTypeId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AktivitetTypeId", aktivitetTypeId);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    liste.Add(new Faerdighed
                    {
                        FaerdighedId = r.GetInt32(0),
                        Navn = r.GetString(1),
                    });
                }
            }
            return liste;
        }
    }
    }


