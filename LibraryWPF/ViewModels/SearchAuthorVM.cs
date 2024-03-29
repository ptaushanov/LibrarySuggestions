﻿using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using LibraryWPF.Utils;
using System.Windows.Controls;
using LibraryWPF.Views;
using LibraryWPF.Models;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace LibraryWPF.ViewModels
{
    public class SearchAuthorVM : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private UserControl _currentControl;

        private List<string> _suggestions;
        private string _title;
        private string _category;
        private string _firstName;
        private string _lastName;
        private string _publisher;

        private Author _selectedAuthor;
        private string _selectedCategory;
        private List<Author> _categoryResults;
        private bool _popupOpen;

        public RelayCommand ChangeToSearchControlCommand { get; private set; }
        public RelayCommand ChangeToResultsControlCommand { get; private set; }
        public RelayCommand NextListBoxSelectedIndexCommand { get; private set; }
        public RelayCommand PreviousListBoxSelectedIndexCommand { get; private set; }
        public RelayCommand SearchCommand { get; private set; }

        public SearchAuthorVM()
        {
            ChangeToSearchControlCommand = new RelayCommand(ChangeToSearchControl);
            ChangeToResultsControlCommand = new RelayCommand(ChangeToResultsControl);
            NextListBoxSelectedIndexCommand = new RelayCommand(NextListBoxSelectedIndex);
            PreviousListBoxSelectedIndexCommand = new RelayCommand(PreviousListBoxSelectedIndex);
            SearchCommand = new RelayCommand(Search);
            CurrentControl = new SearchAuthorControl();
            Suggestions = new List<string>();
            SearchResults = new List<Author>();
            SelectedAuthor = new Author();
            CanSuggest = true;
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

        public List<string> Suggestions
        {
            get { return _suggestions; }
            set { _suggestions = value; PropChanged("Suggestions"); }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (value == null) { return; }
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
                if (value == null) { return; }
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
                if (value == null) { return; }
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
                if (value == null) { return; }
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
            }
        }

        public List<Author> SearchResults { get; private set; }

        public List<Author> CategoryResults
        {
            get { return _categoryResults; }
            set
            {
                _categoryResults = value;
                PropChanged("CategoryResults");
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

        public bool CanSuggest { get; set; }
        private void TrySuggestAuthor(string propertyName)
        {
            if (!CanSuggest) { return; }
            Suggestions = SuggestionsManager.Suggest<Author, string>(this, propertyName, true);
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

            CategoryResults = SearchResults
                .Where(result => result.Category == Category)
                .ToList();

            CurrentControl = new SearchResultsControl();
        }

        private void NextListBoxSelectedIndex(object _listbox)
        {
            ListBox listBox = _listbox as ListBox;

            if (!listBox.IsFocused)
            {
                listBox.Focus();
                listBox.SelectedIndex = (listBox.SelectedIndex + 1) % listBox.Items.Count;
            }
        }

        private void PreviousListBoxSelectedIndex(object _listbox)
        {
            ListBox listBox = _listbox as ListBox;

            if (!listBox.IsFocused)
            {
                listBox.Focus();
                int previousIndex = (listBox.SelectedIndex - 1);
                listBox.SelectedIndex = previousIndex < 0 ? listBox.Items.Count - 1 : previousIndex;
            }
        }

        private void Search(object _)
        {
            Author sampleAuthor = new Author(null, Title, null, FirstName, LastName, Publisher);
            SearchManager.Search(sampleAuthor, SearchResults);
        }

    }
}
