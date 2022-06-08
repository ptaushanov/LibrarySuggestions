using LibraryWPF.Models;
using LibraryWPF.Utils;
using System.Collections.Generic;
using System.Linq;

namespace LibraryWPF.DAL
{
    public class AuthorService
    {
        private static readonly LibraryContext _libraryContext = new LibraryContext();

        public static void SaveAuthorSuggestion(Author author)
        {
            string encodedSuggestion = SuggestionsIOHelper.EncodeSuggestion(author);
            SuggestionsIOHelper.SaveSuggestionToFile(encodedSuggestion);

            _libraryContext.Authors.Add(author);
            _libraryContext.SaveChanges();
        }

        public static IEnumerable<Author> FindAuthorSuggestions<T>(string searchProperty, T searchTerm)
        {
            string stringifiedSearchTerm = searchTerm.ToString();

            return SuggestionsIOHelper
                .LoadSuggestionsFromFile()
                .Select(encodedSuggestion =>
                    SuggestionsIOHelper.DecodeSuggestion<Author>(encodedSuggestion)
                ).Where(author =>
                    author
                    .GetType()
                    .GetProperty(searchProperty)
                    .GetValue(author)
                    .ToString()
                    .Contains(stringifiedSearchTerm)
                );
        }

        public static IEnumerable<object> FindPropertySuggestions<T>(string searchProperty, T searchTerm)
        {
            string stringifiedSearchTerm = searchTerm.ToString();

            return FindAuthorSuggestions(searchProperty, searchTerm)
                .Select(author =>
                   author
                        .GetType()
                        .GetProperty(searchProperty)
                        .GetValue(author)
                );
        }
    }
}
