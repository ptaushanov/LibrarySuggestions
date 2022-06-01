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
using LibraryWPF.Models;

namespace LibraryWPF.ViewModels
{
    public class SearchAuthorVM : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private UserControl _currentControl;

        private ObservableCollection<Author> _suggestions;
        private string _title;
        private string _category;
        private string _firstName;
        private string _lastName;
        private string _publisher;

        private Author _selectedAuthor;
        private string _selectedCategory;

        public RelayCommand ChangeToSearchControlCommand { get; private set; }
        public RelayCommand ChangeToResultsControlCommand { get; private set; }

        public SearchAuthorVM()
        {
            ChangeToSearchControlCommand = new RelayCommand(ChangeToSearchControl);
            ChangeToResultsControlCommand = new RelayCommand(ChangeToResultsControl);
            CurrentControl = new SearchAuthorControl();

            Suggestions = new ObservableCollection<Author>();
            SelectedAuthor = null;
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

        public ObservableCollection<Author> Suggestions
        {
            get { return _suggestions; }
            set { _suggestions = value; PropChanged("Suggestions"); }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                TrySuggestAuthor("Title");
                PropChanged("Title");
            }
        }

        private void TrySuggestAuthor(string v)
        {
            // Remove
        }

        public string Category
        {
            get { return _category; }
            set { _category = value; PropChanged("Category"); }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                TrySuggestAuthor("FirstName");
                PropChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                TrySuggestAuthor("LastName");
                PropChanged("LastName");
            }
        }

        public string Publisher
        {
            get { return _publisher; }
            set
            {
                _publisher = value;
                TrySuggestAuthor("Publisher");
                PropChanged("Publisher");
            }
        }

        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                PropChanged("SelectedCategory");
            }
        }

        public Author SelectedAuthor
        {
            get { return _selectedAuthor; }
            set
            {
                _selectedAuthor = value;
                PropChanged("SelectedAuthor");

                if (value == null) { return; }

                Title = value.Title;
                Category = value.Category;
                FirstName = value.FirstName;
                LastName = value.LastName;
                Publisher = value.Publisher;

                PropChanged("AddEnabled");
                PropChanged("EditEnabled");
            }
        }

        private void ChangeToSearchControl(object _)
        {
            CurrentControl = new SearchAuthorControl();
        }

        private void ChangeToResultsControl(object category)
        {
            SelectedCategory = category as string;
            switch (category as string)
            {
                case "Книги":
                    Category = "Книга";
                    break;
                case "Списания":
                    Category = "Списание";
                    break;
                case "Вестници":
                    Category = "Вестник";
                    break;
                case "Интервюта":
                    Category = "Интервю";
                    break;
                case "Публикации":
                    Category = "Публикация";
                    break;
            }
            CurrentControl = new SearchResultsControl();
        }
    }
}
