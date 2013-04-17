using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;

namespace WpfFrenchChampionship.ViewModel
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected class BasicCommand : ICommand
        { 
            public delegate void CommandMethod(object parameter);
            private CommandMethod _method;

            public BasicCommand(CommandMethod method)
            {
                this._method = method;
            }
            public bool  CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler  CanExecuteChanged;

            public void Execute(object parameter)
            {
                this._method(parameter);
            }
        }

        public void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
