﻿using LibraryWPF.Models;
using LibraryWPF.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace LibraryWPF.ViewModels
{
    public class AddAuthorVM : DependencyObject, INotifyPropertyChanged
    {
        private List<Author> _suggestions;
        private string _title;
        private string _category;
        private string _firstName;
        private string _lastName;
        private string _publisher;

        private Author _selectedAuthor;

        public RelayCommand SaveSuggestionCommand { get; private set; }

        public List<string> Categories { get; set; }

        public AddAuthorVM()
        {
            Categories = new List<string>()
            {
                "Книга", "Списание", "Вестник", "Интервю", "Публикация"
            };

            Suggestions = new List<Author>();
            SaveSuggestionCommand = new RelayCommand(SaveSuggestion);
            SelectedAuthor = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void PropChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<Author> Suggestions
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

                Title = value.Title;
                Category = value.Category;
                FirstName = value.FirstName;
                LastName = value.LastName;
                Publisher = value.Publisher;
            }
        }

        private void TrySuggestAuthor(string propertyName)
        {
            // if (SelectedAuthor != null) { return; }
            Suggestions = SuggestionsManager.Suggest<Author, Author>(this, propertyName);
        }

        public void SaveSuggestion(object _)
        {
            Author newAuthor = new Author(null, Title, Category, FirstName, LastName, Publisher);

            try
            {
                SuggestionsManager.SaveSuggestion(newAuthor);
                ClearFields(null);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void ClearFields(object _)
        {
            Title = "";
            Category = "";
            FirstName = "";
            LastName = "";
            Publisher = "";
            Suggestions.Clear();
            SelectedAuthor = null;
        }
    }

}
