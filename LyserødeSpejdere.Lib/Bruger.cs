namespace LyserødeSpejdere.Lib
{
    public class Bruger
    {
        private int _brugerId;
        private string _navn;
        private string _mail;
        private string _brugernavn;
        private string _adgangskode;
        private DateTime _fødselsdato;
        private bool _erLeder;


        public Bruger()
        {
            _brugerId = 0;
            _navn = "";
            _mail = "";
            _brugernavn = "";
            _adgangskode = "";
            _fødselsdato = DateTime.Now;
            _erLeder = true;
        }
        

        public Bruger(int brugerId, string navn, string mail, string brugernavn, string adgangskode, DateTime fødselsdato, bool erLeder )
        {
            _brugerId = brugerId;
            _navn = navn;
            _mail = mail;
            _brugernavn = brugernavn;
            _adgangskode = adgangskode;
            _fødselsdato = fødselsdato;
            _erLeder = erLeder;
        }

        public int BrugerId
        { 
        get { return _brugerId; }
        set { _brugerId = value; }
        }

        public string Navn
        {  
        get { return _navn; } 
        set { _navn = value; } 
        }

        public string Mail
        {
        get { return _mail; }
        set { _mail = value; }
        }

        public string Brugernavn
        {
        get { return _brugernavn; }
        set { _brugernavn = value; }
        }

        public string Adgangskode
        {
        get { return _adgangskode; }
        set { _adgangskode = value;}
        }

        public DateTime Fødselsdato
        {
        get { return _fødselsdato; }
        set { _fødselsdato= value; }
        }

        public bool ErLeder
        {
            get { return _erLeder; }
            set { _erLeder = value; }
        }

        public override string ToString()
        {
            return $"Id: {BrugerId}, Navn: {Navn}, Mail: {Mail}, BrugerNavn: {Brugernavn}, Adgangskode: {Adgangskode}, Fødselsdato: {Fødselsdato}, status: {ErLeder}";
        }
    }
} 
