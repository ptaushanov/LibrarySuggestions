using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;

namespace LibraryWPF.Utils
{
    public static class SuggestionsIOHelper
    {
        private const string SuggestionsPath = "suggestions.txt";
        private const string EncodeDecodeDelimiter = "#&#";
        private const int HistoryAmount = 5;

        public static string EncodeSuggestion(object model)
        {

            PropertyInfo[] modelProperies = model.GetType().GetProperties();
            string[] values = modelProperies
                .Where(property => property.Name != model.GetType().Name + "Id")
                .Select(property =>
                    property
                        .GetValue(model)
                        .ToString()
                ).ToArray();

            return string.Join(EncodeDecodeDelimiter, values);
        }

        public static T DecodeSuggestion<T>(string encodedSuggestion)
        {
            IEnumerable<string> propertyValues = encodedSuggestion
                .Split(new string[] { EncodeDecodeDelimiter }, StringSplitOptions.None);

            T resultModel = (T)Activator.CreateInstance(typeof(T));
            List<PropertyInfo> modelProperies = typeof(T)
                .GetProperties()
                .ToList();

            modelProperies.RemoveAll(property => property.Name == typeof(T).Name + "Id");

            int valuesCount = propertyValues.Count();
            int propertiesCount = modelProperies.Count();

            if (valuesCount != propertiesCount)
                throw new Exception("Открита е грешка при декодиране");

            for (int i = 0; i < modelProperies.Count(); i++)
            {
                modelProperies[i].SetValue(resultModel, propertyValues.ElementAt(i));
            }

            return resultModel;
        }


        public static void SaveSuggestionToFile(string encodedSuggestion)
        {
            IEnumerable<string> lines;

            if (File.Exists(SuggestionsPath))
                lines = File.ReadAllLines(SuggestionsPath);
            else lines = Array.Empty<string>();

            string[] updatedSuggestions = lines
                .Where(line => !string.IsNullOrEmpty(line))
                .Append(encodedSuggestion)
                .ToArray();

            if (updatedSuggestions.Length > HistoryAmount)
            {
                updatedSuggestions = updatedSuggestions
                    .Skip(1)
                    .ToArray();
            }

            File.WriteAllLines(SuggestionsPath, updatedSuggestions);
        }

        public static string[] LoadSuggestionsFromFile()
        {
            if (!File.Exists(SuggestionsPath)) { return Array.Empty<string>(); }
            return File.ReadAllLines(SuggestionsPath);
        }
    }
}
