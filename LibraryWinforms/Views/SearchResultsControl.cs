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
        }
    }
}
