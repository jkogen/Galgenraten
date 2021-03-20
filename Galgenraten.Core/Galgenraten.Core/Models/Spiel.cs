using Galgenraten.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galgenraten.Core.Models
{
    public class Spiel : ViewModelBase
    {
        private IList<string> Woerter;

        private Wort aktuellesWort;

        public Wort AktuellesWort
        {
            get { return aktuellesWort; }
            set { SetProperty(ref aktuellesWort, value); }
        }

        private IList<Buchstabe> moeglicheBuchstaben;

        public IList<Buchstabe> MoeglicheBuchstaben
        {
            get { return moeglicheBuchstaben; }
            set { SetProperty(ref moeglicheBuchstaben, value); }
        }

        public int MaximaleAnzahlVonVersuchen { get; set; } = 16;


        private bool gewonnen;

        public bool Gewonnen
        {
            get { return gewonnen; }
            set { SetProperty(ref gewonnen, value); }
        }
        
        public bool Ende { get; set; } = false;

        private int erfolgreicheVersuche = 0;

        public int ErfolgreicheVersuche
        {
            get { return erfolgreicheVersuche; }
            set 
            {
                SetProperty(ref erfolgreicheVersuche, value);
                Versuche = Versuche;
            }
        }


        private int fehlerhafteVersuche=0;

        public int FehlerhafteVersuche
        {
            get { return fehlerhafteVersuche; }
            set 
            {
                SetProperty(ref fehlerhafteVersuche, value); 
                Versuche = Versuche;
            }
        }


        private int versuche;

        public int Versuche
        {
            get
            {
                return ErfolgreicheVersuche + FehlerhafteVersuche;
            }
            set 
            {
                SetProperty(ref versuche, ErfolgreicheVersuche + FehlerhafteVersuche);
                versuche = ErfolgreicheVersuche + FehlerhafteVersuche;
            }
        }



        public Spiel(string dictionary = null)
        {
            if(dictionary is null)
                Woerter = Wort.LeseWoerterEin($@"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\Ressources\german.dic").Select(a => a).ToList();
            else
                Woerter = Wort.LeseWoerterEin(dictionary).Select(a => a).ToList();
        }

        #region Methoden
        /// <summary>
        /// Setzt ein neues zufaelliges Wort 
        /// </summary>
        public void StarteSpiel()
        {
            var x = new Random();
            var zufallsIndex = x.Next(0, Woerter.Count);
            AktuellesWort = new Wort(Woerter[zufallsIndex]);
            MoeglicheBuchstaben = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Select(a => new Buchstabe(a)).ToList();
            Ende = false;
            Gewonnen = false;
            ErfolgreicheVersuche = 0;
            FehlerhafteVersuche = 0;
        }

        /// <summary>
        /// Überprüft ob der Buchstabe im Wort vor kommt
        /// </summary>
        /// <param name="buchstabe"></param>
        public void RateBuchstaben(char buchstabe)
        {
            AktuellesWort.RateBuchstabe(buchstabe);
            MoeglicheBuchstaben = Buchstabe.SetzeBuchstabenAlsGezogen(MoeglicheBuchstaben, buchstabe);

            var richtigerZug = AktuellesWort.EnthaeltBuchstabe(buchstabe);

            if (richtigerZug)
                ErfolgreicheVersuche++;
            else
                FehlerhafteVersuche++;

            if (FehlerhafteVersuche == MaximaleAnzahlVonVersuchen || AktuellesWort.IstAufgeloest())
                Ende = true;

            if (AktuellesWort.IstAufgeloest())
                Gewonnen = true;
        }

        /// <summary>
        /// Loest das Wort auf
        /// </summary>
        public void Aufloesen()
        {
            AktuellesWort.Aufloesen();
        }
        #endregion
    }
}
