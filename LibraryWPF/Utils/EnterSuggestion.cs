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
    public class EnterSuggestion<T>
    {
        private static PropertyInfo _inputProperty;

        private static ObservableCollection<T> _suggestions;

        public static void SaveSuggestion(object model)
        {
            if (model == null) { throw new Exception("Няма автор, който де се запази!"); }

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

        public static void EditSuggestion(object oldModel, object newModel)
        {
            if (newModel == null) { throw new Exception("Няма автор, който де се запази!"); }

            PropertyInfo[] modelProperties = newModel.GetType().GetProperties();

            foreach (PropertyInfo prop in modelProperties)
            {
                if (prop.PropertyType.Name.ToLower() != "string") { continue; }
                if (
                    prop.GetValue(newModel) == null ||
                    prop.GetValue(newModel).ToString() == ""
                )
                {
                    throw new Exception("Някое поле не е попълнено");
                }
            }

            ServiceRegistry.Update(oldModel, newModel);
        }

        public static void Suggest(object targetVM)
        {
            string searchProperty = _inputProperty.Name;

            string searchTerm = _inputProperty.GetValue(targetVM).ToString();
            _suggestions.Clear();

            if (searchTerm.Equals(string.Empty)) { return; }

            IEnumerable<T> suggestions =
                ServiceRegistry
                .FindLastFive(typeof(T).Name, searchProperty, searchTerm)
                .Cast<T>();

            suggestions
                .ToList()
                .ForEach(_suggestions.Add);
        }

        public static void SwitchContext(object targetVM, string inputPropertyName,
            ObservableCollection<T> sugestions)
        {
            _inputProperty = targetVM.GetType().GetProperty(inputPropertyName);
            _suggestions = sugestions;
        }
    }
}
