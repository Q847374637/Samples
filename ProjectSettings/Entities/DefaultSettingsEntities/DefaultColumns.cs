using System;
using System.Collections.Generic;
using System.Linq;
using Settings = ProjectSettings.Properties.Settings;

namespace ProjectSettings.Entities.DefaultSettingsEntities
{
    public static class DefaultColumns
    {
        static List<String> _defaultColumnsEasypay = ConvertStringCollectionToListOfStrings(Settings.Default.defaultColumnsEasypay);
        static List<String> _defaultColumnsDelay = ConvertStringCollectionToListOfStrings(Settings.Default.defaultColumnsDelay);
        static List<String> _defaultFinalColumnsEasypay = ConvertStringCollectionToListOfStrings(Settings.Default.defaultFinalColumnsEasypay);
        static List<String> _defaultFinalColumnsDelay = ConvertStringCollectionToListOfStrings(Settings.Default.defaultFinalColumnsDelay);

        public static List<String> DefaultColumnsEasypay { get { return _defaultColumnsEasypay; } private set { _defaultColumnsEasypay = value; } }
        public static List<String> DefaultColumnsDelay { get { return _defaultColumnsDelay; } private set { _defaultColumnsDelay = value; } }
        public static List<String> DefaultFinalColumnsEasypay { get { return _defaultFinalColumnsEasypay; } private set { _defaultFinalColumnsEasypay = value; } }
        public static List<String> DefaultFinalColumnsDelay { get { return _defaultFinalColumnsDelay; } private set { _defaultFinalColumnsDelay = value; } }

        public static List<string> ConvertStringCollectionToListOfStrings(System.Collections.Specialized.StringCollection stringCollection)
        {
            return stringCollection.Cast<string>().ToList();
        }
    }
}
