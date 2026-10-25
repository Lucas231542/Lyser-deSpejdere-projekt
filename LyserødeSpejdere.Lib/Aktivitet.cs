using System;
using System.Collections.Generic;
using System.Text;

namespace LyserødeSpejdere.Lib
{
    public class Aktivitet
    {
        private int _aktivitetId;
        private int _aktivitetTypeId;
        private DateTime _startTid;
        private DateTime _slutTid;
        private string _sted;

        public Aktivitet()
        {
            _aktivitetId = 0;
            _aktivitetTypeId = 0;
            _startTid = DateTime.Now;
            _slutTid = DateTime.Now;
            _sted = "";
        }

        public Aktivitet(int aktivitetId, int aktivitetTypeId, DateTime startTid, DateTime slutTid, string sted)
        {
            _aktivitetId = aktivitetId;
            _aktivitetTypeId = aktivitetTypeId;
            _startTid = startTid;
            _slutTid = slutTid;
            _sted = sted;
        }

        public int AktivitetId
        {
            get { return _aktivitetId; }
            set { _aktivitetId = value; }
        }

        public int AktivitetTypeId
        {
            get { return _aktivitetTypeId; }
            set { _aktivitetTypeId = value; }
        }

        public DateTime StartTid
        {
            get { return _startTid; }
            set { _startTid = value; }
        }

        public DateTime SlutTid
        {
            get { return _slutTid; }
            set { _slutTid = value; }
        }

        public string Sted
        {
            get { return _sted; }
            set { _sted = value; }
        }

        public override string ToString()
        {
            return $"AktivitetId: {AktivitetId}, AktivitetTypeId: {AktivitetTypeId}, StartTid: {StartTid}, SlutTid: {SlutTid}, Sted: {Sted}";
        }
    }
}
}
