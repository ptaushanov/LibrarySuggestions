﻿using LibraryWPF.Models;
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
        private static readonly LibraryContext _libraryContext = new LibraryContext();

        public static void AddAuthor(Author newAuthor)
        {
            _libraryContext.Authors.Add(newAuthor);
            _libraryContext.SaveChanges();
        }

        public static void UpdateAuthor(Author oldAuthor, Author newAuthor)
        {
            oldAuthor.Copy(newAuthor);
            _libraryContext.SaveChanges();
        }

        public static IEnumerable<Author> FindLastFive<T>(string searchProperty, T searchTerm)
        {
            string stringifiedSearchTerm = searchTerm.ToString();

            return _libraryContext.Authors
                .AsEnumerable()
                .OrderByDescending(author => author.AuthorId)
                .Take(5)
                .Where(author =>
                {
                    object value =
                        author
                        .GetType()
                        .GetProperty(searchProperty)
                        .GetValue(author);

                    return value != null && ((string)value).Contains(stringifiedSearchTerm);
                })
                .ToList();
        }
    }
}