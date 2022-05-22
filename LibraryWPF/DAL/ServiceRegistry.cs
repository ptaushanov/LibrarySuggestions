using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryWPF.Models;

namespace LibraryWPF.DAL
{
    public class ServiceRegistry
    {
        public static void Add(object model)
        {
            string modelType = model.GetType().Name;

            switch (modelType)
            {
                case "Author":
                    Author author = (Author)model;
                    AuthorService.AddAuthor(author);
                    break;
                default:
                    throw new Exception($"No service implementation for type {modelType}");
            }
        }

        public static void Update(object oldModel, object newModel)
        {
            string modelType = oldModel.GetType().Name;
            switch (modelType)
            {
                case "Author":
                    Author oldAuthor = (Author)oldModel;
                    Author newAuthor = (Author)newModel;

                    AuthorService.UpdateAuthor(oldAuthor, newAuthor);
                    break;
                default:
                    throw new Exception($"No service implementation for type {modelType}");
            }
        }

        public static IEnumerable<object> FindLastFive<T>(string modelType, string searchProperty, T searchTerm)
        {
            switch (modelType)
            {
                case "Author":
                    return AuthorService.FindLastFive(searchProperty, searchTerm);
                default:
                    throw new Exception($"No service implementation for type {modelType}");
            }
        }
    }
}
