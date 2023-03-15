using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2
{
    public class OsobaPrawna : PosiadaczRachunku
    {
        private string nazwa;
        private string siedziba;
        public string Nazwa { get => nazwa; }
        public string Siedziba { get => siedziba; }

        /* ------------------------------- */
        public OsobaPrawna(string nazwa_, string siedziba_)
        {
            nazwa = nazwa_;
            siedziba = siedziba_;
        }

        public override string ToString()
        {
            return "OsobaPrawna: " + nazwa + " " + siedziba;
        }
    }
}