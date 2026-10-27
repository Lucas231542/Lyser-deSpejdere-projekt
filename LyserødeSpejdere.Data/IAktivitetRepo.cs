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
        public void CreateAktivitet(Aktivitet aktivitet, Bruger bruger);
        public void Update(Aktivitet aktivite);
        public void Delete(int aktivitetId);
 

    }
}
