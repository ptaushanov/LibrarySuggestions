using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using LibraryWPF.DAL;
using LibraryWPF.Models;

namespace LibraryWPF.Utils
{
    public class EnterSuggestion
    {
        private static PropertyInfo _inputProperty;

        private static ObservableCollection<string> _suggestions;

        public static void SaveSuggestion(object model)
        {
            if (model == null) { throw new Exception("Няма автор, който де се запази!"); }

            foreach (PropertyInfo prop in model.GetType().GetProperties())
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

        public static void Suggest(object targetVM, string modelType)
        {
            string searchProperty = _inputProperty.Name;

            string searchTerm = _inputProperty.GetValue(targetVM).ToString();
            _suggestions.Clear();

            IEnumerable<string> suggestions = ServiceRegistry.FindLastFive(modelType, searchProperty, searchTerm);

            suggestions
                .ToList()
                .ForEach(_suggestions.Add);
        }

        public static void SwitchContext(object targetVM, string inputPropertyName,
            ObservableCollection<string> sugestions)
        {
            _inputProperty = targetVM.GetType().GetProperty(inputPropertyName);
            _suggestions = sugestions;
        }
    }
}
