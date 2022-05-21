using LibraryWPF.Models;
using LibraryWPF.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace LibraryWPF.ViewModels
{
    class AddAuthorVM : DependencyObject, INotifyPropertyChanged
    {
        private ObservableCollection<string> _suggestions;
        private string _title;
        private string _category;
        private string _firstName;
        private string _lastName;
        private string _publisher;
        private Author _currentAuthor;

        public RelayCommand SaveSuggestionCommand { get; set; }

        public ObservableCollection<string> Categories { get; set; }

        public AddAuthorVM()
        {
            Categories = new ObservableCollection<string>()
            {
                "Книга", "Списание", "Вестник", "Интервю", "Публикация"
            };

            Suggestions = new ObservableCollection<string>();
            SaveSuggestionCommand = new RelayCommand(SaveSuggestion);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void PropChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<string> Suggestions
        {
            get { return _suggestions; }
            set
            {
                _suggestions = value;
                PropChanged("Suggestions");
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                EnterSuggestion.SwitchContext(this, "Title", Suggestions);
                EnterSuggestion.Suggest(this, "Author");
                PropChanged("Title");
            }
        }

        public string Category
        {
            get { return _category; }
            set { _category = value; PropChanged("Category"); }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; PropChanged("FirstName"); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; PropChanged("LastName"); }
        }

        public string Publisher
        {
            get { return _publisher; }
            set { _publisher = value; PropChanged("Publisher"); }
        }

        public Author CurrentAuthor
        {
            get { return _currentAuthor; }
            set
            {
                _currentAuthor = value;
                PropChanged("Title");
                PropChanged("Author");
                PropChanged("FirstName");
                PropChanged("LastName");
                PropChanged("Publisher");
            }
        }

        public void SaveSuggestion()
        {
            Author newAuthor = new Author(Title, Category, FirstName, LastName, Publisher);

            try
            {
                EnterSuggestion.SaveSuggestion(newAuthor);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
