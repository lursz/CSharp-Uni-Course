using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2
{
    public class OsobaFizyczna : PosiadaczRachunku
    {
        private string imie;
        private string nazwisko;
        private string drugieImie;
        private string pesel;
        private string numerPaszportu;

        public string Imie { get => imie; set => imie = value; }
        public string Nazwisko { get => nazwisko; set => nazwisko = value; }
        public string DrugieImie { get => drugieImie; set => drugieImie = value; }
        public string Pesel
        {
            get => pesel;
            set
            {
                if (value.Length != 11)
                    throw new Exception("Pesel must be 11 characters long");
                pesel = value;
            }
        }
        public string NumerPaszportu { get => numerPaszportu; set => numerPaszportu = value; }

        /* ------------------------------- */
        public OsobaFizyczna(string imie_, string nazwisko_, string drugieImie_, string pesel_, string numerPaszportu_)
        {
            if (pesel_ == null && numerPaszportu_ == null)
            {
                throw new Exception("Pesel and passport number cannot be null");
            }
            if (pesel_ == null || pesel_.Length != 11)
            {
                throw new Exception("Pesel must be 11 characters long");
            }

            imie = imie_;
            nazwisko = nazwisko_;
            drugieImie = drugieImie_;
            pesel = pesel_;
            numerPaszportu = numerPaszportu_;

        }

        public override string ToString()
        {
            return "OsobaFizyczna: " + imie + " " + nazwisko + " " + drugieImie + " " + pesel + " " + numerPaszportu;
        }
    }
}