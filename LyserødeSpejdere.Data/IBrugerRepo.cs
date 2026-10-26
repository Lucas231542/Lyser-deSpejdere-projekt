using LyserødeSpejdere.Lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace LyserødeSpejdere.Data
{
    public interface IBrugerRepo
    {
        public void opret(Bruger bruger);
        public void Delete(Bruger bruger);
        public void GetBruger(int BrugerId);


    }
}
