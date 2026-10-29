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

        private string _imagePath;


        public AktivitetType()
        {
            _aktivitetTypeId = 0;
            _navn = "";
            _beskrivelse = "";
            _maxDeltagere = 0;
            _minAlder = 0;
            _imagePath = "";
        }

        public AktivitetType(int aktivitetTypeId, string navn, string beskrivelse, int maxDeltagere, int minAlder, string imagePath)
        {
            _aktivitetTypeId = aktivitetTypeId;
            _navn = navn;
            _beskrivelse = beskrivelse;
            _maxDeltagere = maxDeltagere;
            _minAlder = minAlder;
            _imagePath = imagePath;
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

        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }

        public override string ToString()
        {
            return $"AktivitetId: {AktivitetTypeId}, Aktivitet: {Navn}, Beskrivelse: {Beskrivelse}, MaxDeltagere: {MaxDeltagere}, MinimumAlder: {MinAlder}, billed: {ImagePath}";
        }
    }
}
