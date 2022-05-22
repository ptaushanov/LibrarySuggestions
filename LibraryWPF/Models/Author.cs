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

        public Author()
        {
            AuthorId = null;
            Title = "";
            Category = "";
            FirstName = "";
            LastName = "";
            Publisher = "";
        }

        public Author(
            int? authorId,
            string title, string category,
            string firstName, string lastName, string publisher
        )
        {
            AuthorId = authorId;
            Title = title;
            Category = category;
            FirstName = firstName;
            LastName = lastName;
            Publisher = publisher;
        }

        public void Copy(Author newAuthor)
        {
            AuthorId = newAuthor.AuthorId;
            Title = newAuthor.Title;
            Category = newAuthor.Category;
            FirstName = newAuthor.FirstName;
            LastName = newAuthor.LastName;
            Publisher = newAuthor.Publisher;
        }
    }
}
