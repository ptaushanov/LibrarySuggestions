using LibraryWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWPF.DAL
{
    public class AuthorService
    {
        private static readonly LibraryContext _libraryContext = new LibraryContext();

        public static void AddAuthor(Author newAuthor)
        {
            _libraryContext.Authors.Add(newAuthor);
            _libraryContext.SaveChanges();
        }

        public static IEnumerable<T> FindLastFive<T>(string searchProperty, T searchTerm)
        {
            IEnumerable<T> result = _libraryContext.Authors
                .AsEnumerable()
                //.OrderByDescending(author => author.AuthorId)
                .Take(5)
                //.Where(author =>
                //    author
                //        .GetType()
                //        .GetProperty(searchProperty)
                //        .GetValue(author)
                //        .Equals(searchTerm))
                .Select(author =>
                    author
                        .GetType()
                        .GetProperty(searchProperty)
                        .GetValue(author))
                .Cast<T>();

            List<T> resultList = result.ToList();
            return result;
        }
    }
}
