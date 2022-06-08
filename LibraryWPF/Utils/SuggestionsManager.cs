using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using LibraryWPF.DAL;

namespace LibraryWPF.Utils
{
    public static class SuggestionsManager
    {
        private static PropertyInfo _inputProperty;

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

        public static void Suggest<M, S>
        (object targetVM, ObservableCollection<S> sugestions, bool searchPropertyOnly = false)
        {
            string searchProperty = _inputProperty.Name;

            string searchTerm = _inputProperty.GetValue(targetVM).ToString();
            sugestions.Clear();

            if (searchTerm.Equals(string.Empty)) { return; }

            IEnumerable<S> suggestions =
                ServiceRegistry
                .FindLastFive(typeof(M).Name, searchProperty, searchTerm, searchPropertyOnly)
                .Cast<S>();

            suggestions
                .ToList()
                .ForEach(sugestions.Add);
        }

        public static void SwitchContext(object targetVM, string inputPropertyName)
        {
            _inputProperty = targetVM.GetType().GetProperty(inputPropertyName);
        }
    }
}
