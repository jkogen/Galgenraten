using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Galgenraten.Core.ViewModels
{
    /// <summary>
    /// Fuer PropertyChanged
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Basis-Methode um für jedes Property die ProeprtyChanged()-Methode aufzurufen
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="property"></param>
        public void SetProperty<T>(ref T storage, T value, [CallerMemberName] string property = null)
        {
            if (Equals(storage, value))
                return;

            storage = value;

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
