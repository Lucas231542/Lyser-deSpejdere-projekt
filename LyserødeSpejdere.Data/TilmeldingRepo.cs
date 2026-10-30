using LyserødeSpejdere.Lib;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace LyserødeSpejdere.Data
{
    public class TilmeldingRepo : ITilmeldingRepo
    { 
        private readonly DBConnecter _db = new DBConnecter();

        // Create: Tilmeld en spejder, hvis reglen er overholdt.
        // Returnerer null hvis det lykkes, ellers årsagen.
        public string? Tilmeld(int brugerId, int AktivitetId)
        {
            if (ErTilmeldt(brugerId, AktivitetId))
            {
                return "Du er allerrede tilmeldt denne aktivitet";
            }

            string? fejl = TjekRegler(brugerId, AktivitetId);
            if (fejl != null)
            {
                return fejl;
            }

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Tilmelding (BrugerId, AktivitetId) VALUES (@BrugerId, @AktivitetId)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BrugerId", brugerId);
                cmd.Parameters.AddWithValue("@AktivitetId", AktivitetId);
                cmd.ExecuteNonQuery();
            }

            return null;
        }


        // Read: er spejderen allerede tilmeldt ?
        public bool ErTilmeldt(int brugerId, int aktivitetId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = "SELECT COUNT (*) FROM Tilmelding WHERE BrugerId = @BrugerId AND AktivitetId = @AktivitetId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BrugerId", brugerId);
                cmd.Parameters.AddWithValue("@AktivitetId", aktivitetId);
                return (int)cmd.ExecuteScalar() > 0;

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
                string sql = @"SELECt a.AktivitetId, a.AktivitetTypeId, a.StartTid, a.SlutTid, a.Sted
                             FROM Aktivitet a
                             JOIN Tilmelding t ON a.AktivitetId = t.AktivitetId
                             WHERE t.BrugerId = @BrugerId
                             ORDER BY a.StartTid";
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
                string sql =
                   "SELECT b.BrugerId, b.Navn, b.Mail, b.Brugernavn, b.Adgangskode, b.Foedselsdato, b.ErLeder " +
                                                "FROM Bruger b " +
                                                "JOIN Tilmelding t ON b.BrugerId = t.BrugerId " +
                                                "WHERE t.AktivitetId = @AktivitetId " +
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

        // Tjek casens regler. Returnerer null hvis OK, ellers årsagen.

        public string? TjekRegler(int brugerId, int aktivitetId)
        {
            int? maxDeltagere;
            int? minAlder;
            DateTime foedselsdato;
            DateTime startTid;

            // Hent aktivitetens regler + spejderens fødselsdato i en SQL

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT Typ.MaxDeltagere, typ.MinAlder, b.Foedselsdato, a.StartTid
                             FROM Aktivitet a 
                             JOIN AktivitetType typ ON a.AktivitetTypeId = typ.AktivitetTypeId
                             CROSS JOIN Bruger b
                             WHERE a.AktivitetId = @AktivitetId AND b.BrugerId = @brugerId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AktivitetId", aktivitetId);
                cmd.Parameters.AddWithValue("@brugerId", brugerId);
                SqlDataReader r = cmd.ExecuteReader();

                if (!r.Read())
                {
                    return "Aktiviteten eller brugeren findes ikke";
                }

                maxDeltagere = r.IsDBNull(0) ? null : r.GetInt32(0);
                minAlder = r.IsDBNull(1) ? null : r.GetInt32(1);
                foedselsdato = r.GetDateTime(2);
                startTid = r.GetDateTime(3);
            }

            // Regel 1: Alder 

            if (minAlder != null)
            {
                int alder = BeregnAlder(foedselsdato, startTid);
                if (alder < minAlder)
                {
                    return $"Du skal være mindst {minAlder} år for at deltage";
                }
            }

            // Regel 2: max deltagere 
            if (maxDeltagere != null && HentDeltagerAntal(aktivitetId) >= maxDeltagere)
            {
                return "Aktiviteten er desværre fuld";
            }
            
            // Regel 3: Krævede tegn
            // HAr spejderenikke alle de tegn, aktiviteten kræver, afvises tilmeldingen.
            // Beskeden viser, hvilke tegn der mangler, fx "Du mangler tegn: Ridemærke"
            List<string> mangler = HentManglendeTegn(brugerId,aktivitetId);
            if (mangler.Count > 0)
            {
                return "Du mangler tegn: " + string.Join(", ", mangler);
            }

            return null; // Alle regler er ok
        }

        // Hjælpemetode: Alder på en bestemt dato 

        private int BeregnAlder(DateTime foedselsdato, DateTime dato)
        {
            int alder = dato.Year - foedselsdato.Year;
            if (foedselsdato.Date > dato.AddYears(-alder).Date)
            {
                alder--; // Har ikke haft fødselsdag endnu det år
            }

            return alder;
        }
        
        // Regel 3: Krævede tegn --- Finder de tegn som aktiviteterne KRÆVER, men som spejder IKKE HAR.
        // Tom liste = spejderen har alle krævede tegn.
        private List<string> HentManglendeTegn(int brugerId, int aktivitetId)
        {
            List<string> mangler = new List<string>();
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                
                // 1. Start med aktiviteten
                // 2. JoinAktivitetTypeKrav: De tegn, aktivitetens TYPE kræver
                // 3. JoinFaerdighed: tegnenes navne 
                // 4. NOT IN: fjern de teg, spejderen allerede har 
                string sql =  "SELECT f.Navn " +
                              "FROM Aktivitet a " +
                              "JOIN AktivitetTypeKrav k ON a.AktivitetTypeId = k.AktivitetTypeId " +
                              "JOIN Faerdighed f ON k.FaerdighedId = f.FaerdighedId " +
                              "WHERE a.AktivitetId = @AktivitetId " +
                              "AND k.FaerdighedId NOT IN " +
                              "(SELECT FaerdighedId FROM BrugerFaerdighed WHERE BrugerId = @BrugerId)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AktivitetId", aktivitetId);
                cmd.Parameters.AddWithValue("@BrugerId", brugerId);
                SqlDataReader r = cmd.ExecuteReader();
                
                // Hver række er et tegn spejderen mangler
                while (r.Read())
                {
                    mangler.Add(r.GetString(0));
                }
            }

            return mangler;
        }
    }
}