using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public abstract class ViewModelBase:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// //SetProperty sets _save value and raises PropertyChanged event
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="newvalue"></param>
        /// <param name="propertyName"></param>
        // ReSharper disable once RedundantAssignment
        protected virtual void SetProperty<T>(ref T value, T newvalue, [CallerMemberName] string propertyName = null)
        {
            value = newvalue;
            OnPropertyChanged(propertyName);
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
