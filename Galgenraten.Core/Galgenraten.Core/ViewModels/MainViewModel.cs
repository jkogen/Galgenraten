using Galgenraten.Core.Commands;
using Galgenraten.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Galgenraten.Core.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        #region private fields
        private IList<Spiel> Spiele = new List<Spiel>();
        private int maximaleVersuche = 10;
        #endregion

        #region properties
        private Spiel aktuellesSpiel = new Spiel();

        public Spiel AktuellesSpiel
        {
            get { return aktuellesSpiel; }
            set { SetProperty(ref aktuellesSpiel, value); }
        }

        private int spieleGespielt;

        public int SpieleGespielt
        {
            get { return Spiele.Count(); }
            set { SetProperty(ref spieleGespielt, SpieleGespielt);  }
        }

        private int spieleGewonnen;

        public int SpieleGewonnen
        {
            get { return Spiele.Where(a => a.Gewonnen==true).Count(); }
            set { SetProperty(ref spieleGewonnen, SpieleGewonnen); }
        }



        #endregion

        public MainViewModel()
        {
            AktuellesSpiel.StarteSpiel(maximaleVersuche);
            InitCommands();
        }

        #region commands
        private void InitCommands()
        {
            RateBuchstabeCommand = new RelayCommand(RateBuchstabenExecute, RateBuchstabenCanExecute);
            StarteNeuesSpielCommand = new RelayCommand(StarteNeuesSpielExecute);
            AufloesenCommand = new RelayCommand(AufloesenExecute, AufloesenCanExecute);
            AendereSchwierigkeitCommand = new RelayCommand(AendereSchwierigkeitExecute, AendereSchwierigkeitCanExecute);
        }

        public ICommand RateBuchstabeCommand { get; private set; }
        public ICommand StarteNeuesSpielCommand { get; private set; }
        public ICommand AufloesenCommand { get; private set; }
        public ICommand AendereSchwierigkeitCommand { get; private set; }
        public void RateBuchstabenExecute(object parameter)
        {
            if (parameter as Buchstabe is null 
                //oder buchstabe wurde schon gezogen
                ||AktuellesSpiel.MoeglicheBuchstaben.Where(a => a == parameter as Buchstabe && a.WurdeGezogen==true).Count()>0)
                return;

            AktuellesSpiel.RateBuchstaben(((Buchstabe)parameter).Zeichen);
        }

        public bool RateBuchstabenCanExecute(object paraeter)
        {
            return !AktuellesSpiel.Ende && AktuellesSpiel.FehlerhafteVersuche < AktuellesSpiel.MaximaleAnzahlVonVersuchen;
        }

        public void StarteNeuesSpielExecute(object parameter)
        {
            Spiele.Add(AktuellesSpiel.Clone() as Spiel);
            AktuellesSpiel.StarteSpiel(maximaleVersuche);
            SpieleGespielt = SpieleGespielt; //propertychanged aufrufen
            SpieleGewonnen = SpieleGewonnen; //propertychanged aufrufen
        }

        public bool AufloesenCanExecute(object parameter)
        {
            return !AktuellesSpiel.Gewonnen;
        }

        public void AufloesenExecute(object parameter)
        {
            AktuellesSpiel.Aufloesen();
        }

        public void AendereSchwierigkeitExecute(object parameter)
        {
            if (parameter as string is null)
                throw new ArgumentException("kein string als parameter");

            if (!new List<string>() { "einfach", "mittel", "schwer" }.Contains(parameter.ToString().ToLower()))
                throw new ArgumentException("string enthaelt nicht keines der signalwoerter einfach, mittel oder schwer");

            if (parameter.ToString().ToLower().Contains("einfach"))
                maximaleVersuche = 10;
            else if (parameter.ToString().ToLower().Contains("mittel"))
                maximaleVersuche = 7;
            else if (parameter.ToString().ToLower().Contains("schwer"))
                maximaleVersuche = 5;

            AktuellesSpiel.MaximaleAnzahlVonVersuchen = maximaleVersuche;
        }

        public bool AendereSchwierigkeitCanExecute(object parameter)
        {
            return AktuellesSpiel.Versuche == 0;
        }
        #endregion
    }
}
