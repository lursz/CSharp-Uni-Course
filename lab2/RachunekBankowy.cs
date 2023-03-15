using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2
{
    public class RachunekBankowy : Object
    {
        private string numer;
        private decimal stanRachunku;
        private bool czyDozwolonyDebet;
        List<PosiadaczRachunku> _PosiadaczeRachunku = new List<PosiadaczRachunku>();
        public string Numer { get => numer; set => numer = value; }
        public decimal StanRachunku { get => stanRachunku; set => stanRachunku = value; }
        public bool CzyDozwolonyDebet { get => czyDozwolonyDebet; set => czyDozwolonyDebet = value; }
        public List<PosiadaczRachunku> PosiadaczeRachunku { get => _PosiadaczeRachunku; set => _PosiadaczeRachunku = value; }
        List<Transakcja> _Transakcje = new List<Transakcja>();
        public List<Transakcja> Transakcje { get => _Transakcje; set => _Transakcje = value; }

        /* ------------------------------- */
        public RachunekBankowy(string numer_, decimal stanRachunku_, bool czyDozwolonyDebet_, List<PosiadaczRachunku> _PosiadaczeRachunku_)
        {
            if (_PosiadaczeRachunku_.Count == 0)
            {
                throw new Exception("List of account holders cannot be empty");
            }
            numer = numer_;
            stanRachunku = stanRachunku_;
            czyDozwolonyDebet = czyDozwolonyDebet_;
            PosiadaczeRachunku = _PosiadaczeRachunku_;
        }
        public static void DokonajTransakcji(RachunekBankowy from_, RachunekBankowy to_, decimal kwota, string opis)
        {
            if (kwota < 0 || from_ == null && to_ == null || from_ != null && from_.CzyDozwolonyDebet == false && kwota > from_.StanRachunku)
            {
                throw new Exception("Invalid transaction");
            }
            else
            {
                if (from_ == null)
                {
                    to_.StanRachunku += kwota;
                    to_.Transakcje.Add(new Transakcja(null, to_, kwota, opis));
                }
                else if (to_ == null)
                {
                    from_.StanRachunku -= kwota;
                    from_.Transakcje.Add(new Transakcja(from_, null, kwota, opis));
                }
                else
                {
                    from_.StanRachunku -= kwota;
                    to_.StanRachunku += kwota;
                    from_.Transakcje.Add(new Transakcja(from_, to_, kwota, opis));
                    to_.Transakcje.Add(new Transakcja(from_, to_, kwota, opis));
                }
            }
        }
        // Operator overloading
        public static RachunekBankowy operator +(RachunekBankowy one, PosiadaczRachunku another)
        {
            if (one.PosiadaczeRachunku.Contains(another))
            {
                throw new Exception("Taki posiadacz rachunku już istnieje");
            }
            one.PosiadaczeRachunku.Add(another);
            return one;
        }

        public static RachunekBankowy operator -(RachunekBankowy one, PosiadaczRachunku another)
        {
            if (one.PosiadaczeRachunku.Count == 1)
            {
                throw new Exception("List posiadaczy rachunku nie może być pusta");
            }
            if (!one.PosiadaczeRachunku.Contains(another))
            {
                throw new Exception("Posiadacza rachunku nie ma na liście");
            }
            one.PosiadaczeRachunku.Remove(another);
            return one;
        }

        public override string ToString()
        {
            return "RachunekBankowy: " + numer + " " + stanRachunku + " " + czyDozwolonyDebet + " " + PosiadaczeRachunku;
        }

    }
}