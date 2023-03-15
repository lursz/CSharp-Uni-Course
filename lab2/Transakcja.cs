using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2
{
    public class Transakcja : Object
    {
        private RachunekBankowy? rachunekZrodlowy;
        private RachunekBankowy? rachunekDocelowy;
        private decimal kwota;
        private string opis;

        public RachunekBankowy? RachunekZrodlowy { get => rachunekZrodlowy; set => rachunekZrodlowy = value; }
        public RachunekBankowy? RachunekDocelowy { get => rachunekDocelowy; set => rachunekDocelowy = value; }
        public decimal Kwota { get => kwota; set => kwota = value; }
        public string Opis { get => opis; set => opis = value; }

        public Transakcja(RachunekBankowy? rachunekZrodlowy_, RachunekBankowy? rachunekDocelowy_, decimal kwota_, string opis_)
        {
            if (rachunekZrodlowy_ == null && rachunekDocelowy_ == null)
            {
                throw new Exception("Source and destination account cannot be null");
            }

            rachunekZrodlowy = rachunekZrodlowy_;
            RachunekDocelowy = rachunekDocelowy_;
            kwota = kwota_;
            opis = opis_;

        }
        public override string ToString()
        {
            return "Transakcja: " + rachunekZrodlowy?.Numer + " -> " + rachunekDocelowy?.Numer + " " + kwota + " " + opis;
        }
    }
}