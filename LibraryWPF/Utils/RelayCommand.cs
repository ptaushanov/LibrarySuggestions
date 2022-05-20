using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryWPF.Utils
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action action;

        public bool CanExecute(object parameter) { return true; }

        public RelayCommand(Action execute)
        {
            action = execute;
        }

        public void Execute(object parameter)
        {
            action?.Invoke();
        }
    }
}
