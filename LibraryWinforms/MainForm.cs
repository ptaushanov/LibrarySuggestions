using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryWinforms.Views;

namespace LibraryWinforms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void HandleOpenAddWindow(object sender, EventArgs e)
        {
            AddAuthorView addAuthorView = new AddAuthorView();
            addAuthorView.Show();
        }

        private void HandleOpenSearchWindow(object sender, EventArgs e)
        {
            SearchAuthorView searchBooksView = new SearchAuthorView();
            searchBooksView.Show();
        }
    }
}
