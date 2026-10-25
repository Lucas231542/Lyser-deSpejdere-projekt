using LyserødeSpejdere.Lib;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LyserødeSpejdere.Data
{
    public interface IAktivitetRepo
    {
        public List<Aktivitet> GetAll();
        public Aktivitet GetById(int id);
        public void Create(Aktivitet aktivitet);
        public void update(Aktivitet aktivite);
        public void Delete(int id);


    }
}
