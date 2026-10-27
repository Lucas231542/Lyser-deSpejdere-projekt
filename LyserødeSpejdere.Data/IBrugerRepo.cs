using LyserødeSpejdere.Lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace LyserødeSpejdere.Data
{
    public interface IBrugerRepo
    {
        List<Bruger> GetAll();
        Bruger? GetById(int brugerId);
        Bruger? Login(string brugernavn, string adgangskode);
        void Opret(Bruger bruger);
        void Update(Bruger bruger);
        void Delete(int brugerId);


    }
}
