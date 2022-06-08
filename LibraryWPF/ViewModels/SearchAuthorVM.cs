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

        private ObservableCollection<string> _suggestions;
        private string _title;
        private string _category;
        private string _firstName;
        private string _lastName;
        private string _publisher;

        private Author _selectedAuthor;
        private string _selectedCategory;
        private bool _popupOpen;
        private TextBox _selectedTextBox;

        public RelayCommand ChangeToSearchControlCommand { get; private set; }
        public RelayCommand ChangeToResultsControlCommand { get; private set; }
        public RelayCommand FocusChangedCommand { get; private set; }

        public SearchAuthorVM()
        {
            ChangeToSearchControlCommand = new RelayCommand(ChangeToSearchControl);
            ChangeToResultsControlCommand = new RelayCommand(ChangeToResultsControl);
            FocusChangedCommand = new RelayCommand(FocusChanged);
            CurrentControl = new SearchAuthorControl();

            Suggestions = new ObservableCollection<string>();
            SelectedAuthor = null;
            SelectedTextBox = null;
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

        public ObservableCollection<string> Suggestions
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
            }
        }

        public bool PopupOpen
        {
            get { return _popupOpen; }
            set
            {
                if (_popupOpen == value) { return; }
                _popupOpen = value;
                PropChanged("PopupOpen");
            }
        }

        public TextBox SelectedTextBox
        {
            get { return _selectedTextBox; }
            set
            {
                _selectedTextBox = value;
                PropChanged("SelectedTextBox");
            }
        }

        private void TrySuggestAuthor(string propertyName)
        {
            SuggestionsManager.SwitchContext(this, propertyName);
            SuggestionsManager.Suggest<Author, string>(this, Suggestions, true);
            PopupOpen = Suggestions.Count > 0;
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

        private void FocusChanged(object textField)
        {
            MessageBox.Show(textField.GetType().ToString());
        }

    }
}
