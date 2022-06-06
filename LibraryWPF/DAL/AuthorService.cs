using LibraryWPF.Models;
using LibraryWPF.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWPF.DAL
{
    public class AuthorService
    {
        // private static readonly LibraryContext _libraryContext = new LibraryContext();

        public static void SaveAuthorSuggestion(Author author)
        {
            string encodedSuggestion = SuggestionsIOHelper.EncodeSuggestion(author);
            SuggestionsIOHelper.SaveSuggestionToFile(encodedSuggestion);
        }

        public static IEnumerable<Author> FindAuthorSuggestions<T>(string searchProperty, T searchTerm)
        {
            string stringifiedSearchTerm = searchTerm.ToString();

            return SuggestionsIOHelper
                .LoadSuggestionsFromFile()
                .Select(SuggestionsIOHelper.DecodeSuggestion<Author>)
                .Where(author =>
                    author
                    .GetType()
                    .GetProperty(searchProperty)
                    .GetValue(author)
                    .ToString()
                    .Contains(stringifiedSearchTerm)
                );
        }
    }
}
