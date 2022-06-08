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
                    AuthorService.SaveAuthorSuggestion(author);
                    break;
                default:
                    throw new Exception($"No service implementation for type {modelType}");
            }
        }

        public static IEnumerable<object> FindLastFive<T>
        (string modelType, string searchProperty, T searchTerm, bool propertyOnly)
        {
            switch (modelType)
            {
                case "Author":
                    return propertyOnly ?
                        AuthorService.FindPropertySuggestions(searchProperty, searchTerm) :
                        AuthorService.FindAuthorSuggestions(searchProperty, searchTerm);
                default:
                    throw new Exception($"No service implementation for type {modelType}");
            }
        }
    }
}
