using LibraryWPF.Models;
using LibraryWPF.Utils;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

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

        public static IEnumerable<Author> FindAuthors(Author sampleAuthor)
        {
            Debug.WriteLine(sampleAuthor.FirstName);
            return _libraryContext
                .Authors
                .Where(currentAuthor =>
                    (currentAuthor.Title == sampleAuthor.Title || string.IsNullOrEmpty(sampleAuthor.Title)) &&
                    (currentAuthor.FirstName == sampleAuthor.FirstName || string.IsNullOrEmpty(sampleAuthor.FirstName)) &&
                    (currentAuthor.LastName == sampleAuthor.LastName || string.IsNullOrEmpty(sampleAuthor.LastName)) &&
                    (currentAuthor.Publisher == sampleAuthor.Publisher || string.IsNullOrEmpty(sampleAuthor.Publisher))
                );
        }
    }
}
