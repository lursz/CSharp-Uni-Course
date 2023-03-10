using System;
using System.Collections.Generic;
using System.IO;

// ?? operator
// x ?? y (x & y are of the same type)
// if (x == null) return y
// else return x
/* ---------------------------- PosiadaczRachunku --------------------------- */
public abstract class PosiadaczRachunku
{
    // abstract ToString() overload
    public abstract override string ToString();
}
/* ------------------------------ OsobaFizyczna ----------------------------- */
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
        if (pesel_ == null || numerPaszportu_ == null)
        {
            throw new Exception("Pesel and passport number cannot be null");
        }
        if (pesel_.Length != 11)
        {
            throw new Exception("Pesel must be 11 characters long");
        }

        imie = imie_;
        nazwisko = nazwisko_;
        drugieImie = drugieImie_;
        pesel = pesel_;
        numerPaszportu = numerPaszportu_;

    }
    // pesel_ = pesel ?? throw new Exception("Pesel and passport number cannot be null");
    // numerPaszportu_ = numerPaszportu ?? throw new Exception("Pesel and passport number cannot be null");

    public override string ToString()
    {
        return "OsobaFizyczna: " + imie + " " + nazwisko + " " + drugieImie + " " + pesel + " " + numerPaszportu;
    }
}
/* ------------------------------- OsobaPrawna ------------------------------ */
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
/* ----------------------------- RachunekBankowy ---------------------------- */
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
/* ------------------------------- Transakcja ------------------------------- */
public class Transakcja : Object
{
    private RachunekBankowy rachunekZrodlowy;
    private RachunekBankowy rachunekDocelowy;
    private decimal kwota;
    private string opis;

    public RachunekBankowy RachunekDocelowy { get => rachunekDocelowy; set => rachunekDocelowy = value; }
    public RachunekBankowy RachunekZrodlowy { get => rachunekZrodlowy; set => rachunekZrodlowy = value; }
    public decimal Kwota { get => kwota; set => kwota = value; }
    public string Opis { get => opis; set => opis = value; }

    public Transakcja(RachunekBankowy rachunekZrodlowy_, RachunekBankowy rachunekDocelowy_, decimal kwota_, string opis_)
    {
        if (rachunekZrodlowy_ == null || rachunekDocelowy_ == null)
        {
            throw new Exception("Source and destination account cannot be null");
        }

        rachunekZrodlowy = rachunekZrodlowy_;
        rachunekDocelowy = rachunekDocelowy_;
        kwota = kwota_;
        opis = opis_;

    }
    public override string ToString()
    {
        return "Transakcja: " + rachunekZrodlowy.Numer + " -> " + rachunekDocelowy.Numer + " " + kwota + " " + opis;
    }
}



/* -------------------------------------------------------------------------- */
/*                                MAIN PROGRAM                                */
/* -------------------------------------------------------------------------- */
    class Program
    {
        static public void Main(string[] args)
        {
                 }
    }
