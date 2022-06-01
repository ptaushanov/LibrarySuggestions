using LibraryWPF.Models;
using LibraryWPF.Utils;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace LibraryWPF.ViewModels
{
    class AddAuthorVM : DependencyObject, INotifyPropertyChanged
    {
        private ObservableCollection<Author> _suggestions;
        private int? _id = null;
        private string _title;
        private string _category;
        private string _firstName;
        private string _lastName;
        private string _publisher;

        private Author _selectedAuthor;

        public RelayCommand SaveSuggestionCommand { get; private set; }
        public RelayCommand EditSuggestionCommand { get; private set; }
        public RelayCommand DeselectCommand { get; private set; }

        public ObservableCollection<string> Categories { get; set; }

        public AddAuthorVM()
        {
            Categories = new ObservableCollection<string>()
            {
                "Книга", "Списание", "Вестник", "Интервю", "Публикация"
            };

            Suggestions = new ObservableCollection<Author>();
            SaveSuggestionCommand = new RelayCommand(SaveSuggestion);
            DeselectCommand = new RelayCommand(Deselect);
            EditSuggestionCommand = new RelayCommand(EditSuggestion);
            SelectedAuthor = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void PropChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Author> Suggestions
        {
            get { return _suggestions; }
            set { _suggestions = value; PropChanged("Suggestions"); }
        }

        public int? Id
        {
            get { return _id; }
            private set { _id = value; }
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

        public Author SelectedAuthor
        {
            get { return _selectedAuthor; }
            set
            {
                _selectedAuthor = value;
                PropChanged("SelectedAuthor");

                if (value == null) { return; }

                Id = value.AuthorId;
                Title = value.Title;
                Category = value.Category;
                FirstName = value.FirstName;
                LastName = value.LastName;
                Publisher = value.Publisher;

                PropChanged("AddEnabled");
                PropChanged("EditEnabled");
            }
        }

        public bool AddEnabled { get => SelectedAuthor == null; }
        public bool EditEnabled { get => SelectedAuthor != null; }

        private void TrySuggestAuthor(string propertyName)
        {
            if (SelectedAuthor != null) { return; }

            EnterSuggestion<Author>.SwitchContext(this, propertyName, Suggestions);
            EnterSuggestion<Author>.Suggest(this);
        }

        public void SaveSuggestion(object _)
        {
            Author newAuthor = new Author(null, Title, Category, FirstName, LastName, Publisher);

            try
            {
                EnterSuggestion<Author>.SaveSuggestion(newAuthor);
                Deselect(null);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void EditSuggestion(object _)
        {
            Author editedAuthor = new Author(null, Title, Category, FirstName, LastName, Publisher);
            try
            {
                EnterSuggestion<Author>.EditSuggestion(SelectedAuthor, editedAuthor);
                Deselect(null);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void Deselect(object _)
        {
            Id = null;
            Title = "";
            Category = "";
            FirstName = "";
            LastName = "";
            Publisher = "";
            SelectedAuthor = null;
            Suggestions.Clear();

            PropChanged("AddEnabled");
            PropChanged("EditEnabled");
        }
    }

}
