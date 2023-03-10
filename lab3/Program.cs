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
}