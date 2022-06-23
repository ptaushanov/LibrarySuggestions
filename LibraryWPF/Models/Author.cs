using System;

namespace LibraryWPF.Models
{
    public class Author
    {
        public int? AuthorId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Publisher { get; set; }

        public int YearOfPublication { get; set; }

        public Author()
        {
            AuthorId = null;
            Title = "";
            Category = "";
            FirstName = "";
            LastName = "";
            Publisher = "";
            YearOfPublication = 0;
        }

        public Author(
            int? authorId,
            string title, string category,
            string firstName, string lastName, string publisher, int yearOfPublication
        )
        {
            AuthorId = authorId;
            Title = title;
            Category = category;
            FirstName = firstName;
            LastName = lastName;
            Publisher = publisher;
            YearOfPublication = yearOfPublication;
        }

        public void Copy(Author newAuthor)
        {
            Title = newAuthor.Title;
            Category = newAuthor.Category;
            FirstName = newAuthor.FirstName;
            LastName = newAuthor.LastName;
            Publisher = newAuthor.Publisher;
            YearOfPublication = newAuthor.YearOfPublication;
        }
    }
}
