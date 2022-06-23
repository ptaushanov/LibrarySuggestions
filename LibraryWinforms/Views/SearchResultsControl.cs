using LibraryWPF.Models;
using LibraryWPF.ViewModels;
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
    public partial class SearchResultsControl : UserControl
    {
        public SearchAuthorVM SearchAuthorViewModel { get; private set; }
        public SearchResultsControl(SearchAuthorVM searchAuthorVM)
        {
            SearchAuthorViewModel = searchAuthorVM;
            InitializeComponent();
        }

        private void HandleControlLoad(object sender, EventArgs e)
        {
            CategoryLabel
                .DataBindings
                .Add(new Binding("Text", SearchAuthorViewModel, "SelectedCategory"));

            SearchResultsDataGrid
                .DataBindings
                .Add(new Binding("DataSource", SearchAuthorViewModel, "CategoryResults"));


            TitleTextBox
                .DataBindings
                .Add(new Binding("Text", SearchAuthorViewModel, "SelectedAuthor.Title"));

            FirstNameTextBox
                .DataBindings
                .Add(new Binding("Text", SearchAuthorViewModel, "SelectedAuthor.FirstName"));

            LastNameTextBox
                .DataBindings
                .Add(new Binding("Text", SearchAuthorViewModel, "SelectedAuthor.LastName"));

            PublisherTextBox
                .DataBindings
                .Add(new Binding("Text", SearchAuthorViewModel, "SelectedAuthor.Publisher"));
        }


        private void HandleSelectionChanged(object sender, EventArgs e)
        {
            var selectedRows = ((DataGridView)sender).SelectedRows;
            if (selectedRows.Count == 0) { return; }

            Author selectedAuthor = selectedRows[0].DataBoundItem as Author;
            SearchAuthorViewModel.SelectedAuthor = selectedAuthor;
        }
    }
}
