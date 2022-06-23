using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LibraryWPF.DAL;

namespace LibraryWPF.Utils
{
    public static class SearchManager
    {
        public static void Search<T>(T model, List<T> searchResults)
        {
            searchResults.Clear();
            IEnumerable<T> _searchResults = ServiceRegistry.Find(model).Cast<T>();
            _searchResults
                .ToList()
                .ForEach(searchResults.Add);
        }
    }
}
