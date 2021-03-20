using Galgenraten.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galgenraten.Core.Models
{
    public class Buchstabe : ViewModelBase
    {
        private char zeichen;

        public char Zeichen
        {
            get { return zeichen; }
            set { SetProperty(ref zeichen,value); }
        }


        private bool richtigErraten;

        public bool RichtigErraten
        {
            get { return richtigErraten; }
            set { SetProperty(ref richtigErraten, value); }
        }


        private bool wurdeGezogen = false;

        public bool WurdeGezogen
        {
            get { return wurdeGezogen; }
            set { SetProperty(ref wurdeGezogen, value); }
        }

        public Buchstabe(char buchstabe, bool wurdeGezogen = false, bool richtigErraten = false)
        {
            Zeichen = buchstabe;
            WurdeGezogen = wurdeGezogen;
            RichtigErraten = richtigErraten;
        }

        public override string ToString()
        {
            return Zeichen.ToString();
        }

        /// <summary>
        /// Markiert die Buchstaben als gezogen
        /// </summary>
        /// <param name="buchstaben"></param>
        /// <param name="buchstabe"></param>
        public static IList<Buchstabe> SetzeBuchstabenAlsGezogen(IList<Buchstabe> buchstaben, char buchstabe)
        {
            //setzt die liste neu
            //wenn das zeichen vor kommt, wird es als gezogen markiert
            return buchstaben.Select(a => new Buchstabe(a.Zeichen, true)).ToList();
        }
    }
}
