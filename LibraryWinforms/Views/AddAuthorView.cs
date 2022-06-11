using LibraryWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LibraryWinforms.Utils;

namespace LibraryWinforms.Views
{
    public partial class AddAuthorView : Form
    {
        public AddAuthorVM AddAuthorViewModel { get; set; }

        public AddAuthorView()
        {
            InitializeComponent();
            AddAuthorViewModel = new AddAuthorVM();
        }

        private void HandleFormLoad(object sender, EventArgs e)
        {
            TitleTextBox
                .DataBindings
                .Add(new Binding("Text", AddAuthorViewModel, "Title", true, DataSourceUpdateMode.OnPropertyChanged));

            CategoryComboBox
                .DataBindings
                .Add(new Binding("DataSource", AddAuthorViewModel, "Categories", true, DataSourceUpdateMode.OnPropertyChanged));

            CategoryComboBox
                .DataBindings
                .Add(new Binding("Text", AddAuthorViewModel, "Category", true, DataSourceUpdateMode.OnPropertyChanged));

            FirstNameTextBox
                .DataBindings
                .Add(new Binding("Text", AddAuthorViewModel, "FirstName", true, DataSourceUpdateMode.OnPropertyChanged));

            LastNameTextBox
                .DataBindings
                .Add(new Binding("Text", AddAuthorViewModel, "LastName", true, DataSourceUpdateMode.OnPropertyChanged));

            PublisherTextBox
                .DataBindings
                .Add(new Binding("Text", AddAuthorViewModel, "Publisher", true, DataSourceUpdateMode.OnPropertyChanged));

            SuggestionsDataGrid
                .DataBindings
                .Add(new Binding("DataSource", AddAuthorViewModel, "Suggestions", true, DataSourceUpdateMode.OnValidation));
        }

        private void HandleAddSuggestion(object sender, EventArgs e)
        {
            CommandExecutor.Execute(((Button)(sender)).Tag.ToString(), AddAuthorViewModel, null);
        }
    }
}
