﻿namespace Galgenraten.Core.Models;

public class Wort : ViewModelBase
{
    #region Properties
    private IList<Buchstabe> buchstaben;

    public IList<Buchstabe> Buchstaben
    {
        get { return buchstaben; }
        set { SetProperty(ref buchstaben, value); }
    }

    /// <summary>
    /// Prueft ob jeder Buchstabe erraten wurde
    /// </summary>
    /// <returns></returns>
    public bool IstAufgeloest => Buchstaben.Where(a => a.RichtigErraten == false).Count() == 0;
    #endregion

    /// <summary>
    /// Initialisiert das Aktuelle Wort mit den Buchstaben
    /// </summary>
    /// <param name="wort"></param>
    public Wort(string wort)
    {
        Buchstaben = wort.Select(a => new Buchstabe(a)).ToList();
    }

    #region methoden
    /// <summary>
    /// Gibt die Woerter getrennt nach Zeilenumbruch zurueck, die mit Großbuchstaben beginnen
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static IList<string> LeseWoerterEin(string path)
    {
        char[] forbiddenChars = new char[] { 'ä', 'ö', 'ü', 'ß' };

        //dotnet core unterstützt nur ascii, iso8859-1 und unicode, 
        //deswegen ist es ntowendig, diese zu erweitern:
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var nomen = File.ReadAllText(path, Encoding.GetEncoding(1252))
            .Split('\n')
            .Where(a => a.Length > 3 && char.IsUpper(a[0]) //erstes zeichen soll groß sein (nomen)
             && !a.ToLower().Any(b => forbiddenChars.Contains(b)))
            .Select(a => a.ToUpper().Trim())
            .ToList();

        return nomen;
    }

    /// <summary>
    /// Konkateniert alle Buchstaben zu einem Wort (string)
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return string.Concat(Buchstaben.Select(a => a.Zeichen));
    }

    /// <summary>
    /// Aendert den Status der Buchstaben, wenn sie erraten wurden
    /// </summary>
    /// <param name="buchstabe"></param>
    /// <returns></returns>
    public void RateBuchstabe(char buchstabe)
    {
        //setzt die liste neu
        //wenn das zeichen vor kommt, wird es als richtig erraten markiert
        Buchstaben = Buchstaben.Select(a => new Buchstabe(a.Zeichen)
        {
            RichtigErraten = char.ToUpper(a.Zeichen) == char.ToUpper(buchstabe) || a.RichtigErraten,
        }).ToList();
    }

    /// <summary>
    /// Prueft ob das Wort einen bestimmten Buchstaben enthaelt
    /// </summary>
    /// <param name="buchstabe"></param>
    /// <returns></returns>
    public bool EnthaeltBuchstabe(char buchstabe)
    {
        return Buchstaben.Select(a => a.Zeichen).Contains(buchstabe);
    }

    /// <summary>
    /// Setzt alle Buchstaben als erraten
    /// </summary>
    public void Aufloesen()
    {
        Buchstaben = Buchstaben.Select(a => new Buchstabe(a.Zeichen, false, true)).ToList();
    }
    #endregion
}