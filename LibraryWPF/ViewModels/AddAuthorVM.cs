using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LibraryWPF.ViewModels
{
    class AddAuthorVM
    {
        public ObservableCollection<string> Categories { get; set; }

        public AddAuthorVM()
        {
            Categories = new ObservableCollection<string>()
            {
                "Книга", "Списание", "Вестник", "Интервю", "Публикация"
            };
        }

    }
}
