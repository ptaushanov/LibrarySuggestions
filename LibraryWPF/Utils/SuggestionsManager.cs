using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LibraryWPF.DAL;

namespace LibraryWPF.Utils
{
    public static class SuggestionsManager
    {
        public static void SaveSuggestion(object model)
        {
            if (model == null) { throw new ArgumentNullException(); }

            PropertyInfo[] modelProperties = model.GetType().GetProperties();

            foreach (PropertyInfo prop in modelProperties)
            {
                if (prop.PropertyType.Name.ToLower() != "string") { continue; }
                if (
                    prop.GetValue(model) == null ||
                    prop.GetValue(model).ToString() == ""
                )
                {
                    throw new Exception("Някое поле не е попълнено");
                }
            }

            ServiceRegistry.Add(model);
        }

        public static List<S> Suggest<M, S>
        (object targetVM, string inputPropertyName, bool searchPropertyOnly = false)
        {
            PropertyInfo searchProperty = targetVM
                .GetType()
                .GetProperty(inputPropertyName);

            string searchPropertyName = searchProperty.Name;

            string searchTerm = searchProperty.GetValue(targetVM).ToString();

            if (searchTerm.Equals(string.Empty)) { return new List<S>(); }

            return
                 ServiceRegistry
                 .FindLastFive(typeof(M).Name, searchPropertyName, searchTerm, searchPropertyOnly)
                 .Cast<S>()
                 .ToList();
        }
    }
}
