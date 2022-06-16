﻿using LibraryWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryWinforms.Views
{
    public partial class SearchAuthorControl : UserControl
    {
        public SearchAuthorVM SearchAuthorViewModel { get; private set; }
        private TextBox _focusedTextBox;
        public SearchAuthorControl(SearchAuthorVM searchAuthorVM)
        {
            SearchAuthorViewModel = searchAuthorVM;
            _hasSelectedSuggestion = false;
            InitializeComponent();
        }

        private void HandleControlLoad(object sender, EventArgs e)
        {
            SuggestionsListBox
                .DataBindings
                .Add(new Binding("DataSource", SearchAuthorViewModel, "Suggestions", true, DataSourceUpdateMode.Never));

            TitleTextBox
                .DataBindings
                .Add(new Binding("Text", SearchAuthorViewModel, "Title", true, DataSourceUpdateMode.OnPropertyChanged));

            FirstNameTextBox
                .DataBindings
                .Add(new Binding("Text", SearchAuthorViewModel, "FirstName", true, DataSourceUpdateMode.OnPropertyChanged));

            LastNameTextBox
                .DataBindings
                .Add(new Binding("Text", SearchAuthorViewModel, "LastName", true, DataSourceUpdateMode.OnPropertyChanged));

            PublisherTextBox
                .DataBindings
                .Add(new Binding("Text", SearchAuthorViewModel, "Publisher", true, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void HandleTextBoxEnter(object sender, EventArgs e)
        {
            _focusedTextBox = sender as TextBox;
            SearchAuthorViewModel.CanSuggest = true;
        }

        private void HandleSelectedValueChanged(object sender, EventArgs e)
        {
            _focusedTextBox.Text = (sender as ListBox).SelectedItem as string;
            SearchAuthorViewModel.CanSuggest = false;
        }
    }
}
