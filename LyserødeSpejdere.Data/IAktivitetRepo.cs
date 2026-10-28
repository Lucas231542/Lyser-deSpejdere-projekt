using LyserødeSpejdere.Lib;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LyserødeSpejdere.Data
{
    public interface IAktivitetRepo
    {
        public List<Aktivitet> GetAllaktivitet();
        public Aktivitet GetAktivitetById(int aktivitetId);
        public void CreateAktivitet(Bruger bruger, int AktivitetTypeId, DateTime StartTid, DateTime SlutTid, string Sted);
        public void Update(DateTime StartTid, DateTime SlutTid, string Sted);
        public void Delete(int aktivitetId);
 

    }
}
