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

        #region properties
        private Spiel aktuellesSpiel = new Spiel();

        public Spiel AktuellesSpiel
        {
            get { return aktuellesSpiel; }
            set { SetProperty(ref aktuellesSpiel, value); }
        }
        #endregion


        public MainViewModel()
        {
            AktuellesSpiel.StarteSpiel();
            RateBuchstabeCommand = new RelayCommand(RateBuchstabenExecute, RateBuchstabenCanExecute);
            StarteNeuesSpielCommand = new RelayCommand(StarteNeuesSpielExecute);
            AufloesenCommand = new RelayCommand(AufloesenExecute, AufloesenCanExecute);
        }

        #region commands
        public ICommand RateBuchstabeCommand { get; private set; }
        public ICommand StarteNeuesSpielCommand { get; private set; }
        public ICommand AufloesenCommand { get; private set; }

        public void RateBuchstabenExecute(object parameter)
        {
            if (parameter as Buchstabe is null)
                return;

            AktuellesSpiel.RateBuchstaben(((Buchstabe)parameter).Zeichen);
        }

        public bool RateBuchstabenCanExecute(object paraeter)
        {
            return !AktuellesSpiel.Ende && AktuellesSpiel.FehlerhafteVersuche < AktuellesSpiel.MaximaleAnzahlVonVersuchen;
        }

        public void StarteNeuesSpielExecute(object parameter)
        {
            AktuellesSpiel.StarteSpiel();
        }

        public bool AufloesenCanExecute(object parameter)
        {
            return RateBuchstabenCanExecute(null);
        }

        public void AufloesenExecute(object parameter)
        {
            AktuellesSpiel.Aufloesen();
        }
        #endregion
    }
}
