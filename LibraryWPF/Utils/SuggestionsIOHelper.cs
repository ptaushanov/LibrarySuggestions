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
                .Select(property =>
                    property
                        .GetValue(model)
                        .ToString()
                ).ToArray();

            return String.Join(EncodeDecodeDelimiter, values);
        }

        public static T DecodeSuggestion<T>(string encodedSuggestion)
        {
            IEnumerable<string> propertyValues =
                encodedSuggestion
                .Split(new string[] { EncodeDecodeDelimiter }, StringSplitOptions.None);

            T resultModel = default;

            PropertyInfo[] modelProperies = resultModel.GetType().GetProperties();

            if (propertyValues.Count() != modelProperies.Count())
                throw new Exception("Открита е грешка при декодиране");

            for (int i = 0; i < modelProperies.Count(); i++)
            {
                modelProperies[i].SetValue(resultModel, propertyValues.ElementAt(i));
            }

            return resultModel;
        }


        public static void SaveSuggestionToFile(string encodedSuggestion)
        {
            IEnumerable<string> lines = File.ReadAllLines(SuggestionsPath);

            string[] updatedSuggestions = lines
                .Append(encodedSuggestion)
                .ToArray();

            if (updatedSuggestions.Length > HistoryAmount)
                updatedSuggestions.Skip(1);

            File.WriteAllLines(SuggestionsPath, updatedSuggestions);
        }

        public static string[] LoadSuggestionsFromFile()
        {
            return File.ReadAllLines(SuggestionsPath);
        }
    }
}
