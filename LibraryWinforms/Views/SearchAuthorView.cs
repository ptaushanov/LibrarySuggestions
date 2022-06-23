using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryWPF.ViewModels;
using LibraryWinforms.Utils;
using LibraryWPF.Models;

namespace LibraryWinforms.Views
{
    public partial class SearchAuthorView : Form
    {
        private SearchAuthorControl _searchAuthorControl;
        private SearchResultsControl _searchResultsControl;

        public UserControl SelectedUserControl { get; set; }
        public SearchAuthorVM SearchAuthorViewModel { get; set; }

        public SearchAuthorView()
        {
            SearchAuthorViewModel = new SearchAuthorVM();

            _searchAuthorControl = new SearchAuthorControl(SearchAuthorViewModel);
            _searchResultsControl = new SearchResultsControl(SearchAuthorViewModel);

            InitializeComponent();
            ShowSearchAuthorUC();
        }

        private void ShowSearchAuthorUC()
        {
            UCPanel.Controls.Clear();
            UCPanel.Controls.Add(_searchAuthorControl);
        }

        private void ShowSearchResultsUC()
        {
            UCPanel.Controls.Clear();
            UCPanel.Controls.Add(_searchResultsControl);
            SearchAuthorViewModel.SelectedAuthor = new Author();
        }

        private void HandleSwitchToSearch(object sender, EventArgs e)
        {
            ShowSearchAuthorUC();
        }

        private void HandleSwitchToResults(object sender, EventArgs e)
        {
            Button senderButton = sender as Button;
            string commandName = senderButton.Tag as string;
            string commandParamiter = senderButton.Text;


            CommandExecutor.Execute(commandName, SearchAuthorViewModel, commandParamiter);
            ShowSearchResultsUC();
        }
    }

}
