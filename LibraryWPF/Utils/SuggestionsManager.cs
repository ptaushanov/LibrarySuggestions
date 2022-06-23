using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                bool nullableValidation = prop.PropertyType.IsGenericType
                    && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);

                if (nullableValidation) { continue; }

                bool stringValidation =
                    prop.PropertyType.Name.ToLower() == "string" &&
                    prop.GetValue(model) == null ||
                    prop.GetValue(model).ToString() == "";

                bool intValidation =
                    prop.PropertyType.Name.ToLower() == "int32" &&
                    (int)prop.GetValue(model) == default;

                if (stringValidation || intValidation)
                    throw new Exception("Някое поле не е попълнено");
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
                 .ToHashSet()
                 .ToList();
        }
    }
}
