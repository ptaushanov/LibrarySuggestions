using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using LibraryWPF.Utils;
using System.Windows.Controls;
using LibraryWPF.Views;

namespace LibraryWPF.ViewModels
{
    public class SearchAuthorVM : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private UserControl _currentControl;
        private string _currentCategory;

        public RelayCommand ChangeToSearchControlCommand { get; private set; }
        public RelayCommand ChangeToResultsControlCommand { get; private set; }

        public SearchAuthorVM()
        {
            ChangeToSearchControlCommand = new RelayCommand(ChangeToSearchControl);
            ChangeToResultsControlCommand = new RelayCommand(ChangeToResultsControl);
        }

        private void PropChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public UserControl CurrentControl
        {
            get { return _currentControl; }
            set
            {
                _currentControl = value;
                PropChanged("CurrentControl");
            }
        }

        public string CurrentCategory
        {
            get { return _currentCategory; }
            set
            {
                _currentCategory = value;
                PropChanged("CurrentCategory");
            }
        }

        private void ChangeToSearchControl(object _)
        {
            CurrentControl = new SearchAuthorControl();
        }

        private void ChangeToResultsControl(object category)
        {
            CurrentCategory = category as string;
            CurrentControl = new SearchResultsControl();
        }
    }
}
