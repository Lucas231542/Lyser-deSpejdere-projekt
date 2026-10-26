using System;
using System.Collections.Generic;
using System.Text;

namespace LyserødeSpejdere.Lib
{
    public class Faerdighed
    {
        private int _faerdighedId;
        private string _navn;

        public Faerdighed()
        {
            _faerdighedId = 0;
            _navn = "";
        }

        public Faerdighed(int faerdighedId, string navn)
        {
            _faerdighedId = faerdighedId;
            _navn = navn;
        }

        public int FaerdighedId
        {
            get { return _faerdighedId; }
            set { _faerdighedId = value; }
        }

        public string Navn
        {
            get { return _navn; }
            set { _navn = value; }
        }

        public override string ToString()
        {
            return $"FaerdighedId: {FaerdighedId}, Navn: {Navn}";
        }
    }
}
