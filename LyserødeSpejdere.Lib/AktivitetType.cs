using System;
using System.Collections.Generic;
using System.Text;

namespace LyserødeSpejdere.Lib
{
    public class AktivitetType
    {
        private int _aktivitetTypeId;
        private string _navn;
        private string _beskrivelse;
        private int _maxDeltagere;
        private int _minAlder;


        public AktivitetType()
        {
            _aktivitetTypeId = 0;
            _navn = "";
            _beskrivelse = "";
            _maxDeltagere = 0;
            _minAlder = 0;
        }

        public AktivitetType(int aktivitetTypeId, string navn, string beskrivelse, int maxDeltagere, int minAlder)
        {
            _aktivitetTypeId = aktivitetTypeId;
            _navn = navn;
            _beskrivelse = beskrivelse;
            _maxDeltagere = maxDeltagere;
            _minAlder = minAlder;
        }

        public int AktivitetTypeId
        {
            get { return _aktivitetTypeId; }
            set { _aktivitetTypeId = value; }
        }

        public string Navn 
        { 
            get { return _navn; } 
            set { _navn = value; } 
        }

        public string Beskrivelse 
        { 
            get { return _beskrivelse; } 
            set { _beskrivelse = value; } 
        }

        public int MaxDeltagere 
        {   
            get { return _maxDeltagere; } 
            set { _maxDeltagere = value; } 
        }

        public int MinAlder 
        { 
            get { return _minAlder; } 
            set { _minAlder = value; } 
        }

        public override string ToString()
        {
            return $"AktivitetId: {AktivitetTypeId}, Aktivitet: {Navn}, Beskrivelse: {Beskrivelse}, MaxDeltagere: {MaxDeltagere}, MinimumAlder: {MinAlder}";
        }
    }
}
