using System;
using LibraryWPF.Utils;
using LibraryWPF.Views;

namespace LibraryWPF.ViewModels
{
    public class MainVM
    {
        public RelayCommand OpenAddWindowCommand { get; set; }
        public RelayCommand OpenSearchWindowCommand { get; set; }

        public MainVM()
        {
            OpenAddWindowCommand = new RelayCommand(OpenAddWindow);
            OpenSearchWindowCommand = new RelayCommand(OpenSearchWindow);
        }

        private void OpenAddWindow()
        {
            AddAuthorView addAuthorView = new AddAuthorView();
            addAuthorView.Show();
        }

        private void OpenSearchWindow()
        {
            SearchBooksView searchBooksView = new SearchBooksView();
            searchBooksView.Show();
        }
    }
}
