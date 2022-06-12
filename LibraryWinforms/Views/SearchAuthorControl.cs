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
        public SearchAuthorControl(SearchAuthorVM searchAuthorVM)
        {
            SearchAuthorViewModel = searchAuthorVM;
            InitializeComponent();
        }

        private void HandleControlLoad(object sender, EventArgs e)
        {

        }
    }
}
