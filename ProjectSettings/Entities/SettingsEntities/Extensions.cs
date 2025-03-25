using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Settings = ProjectSettings.Properties.Settings;
using ProjectSettings.Entities.DefaultSettingsEntities;


namespace ProjectSettings.Entities.SettingsEntities
{
    public static class Extensions
    {
        static List<String> _extensions = ConvertStringCollectionToListOfStrings(Settings.Default.extensions);

        public static List<String> Extension { get { return _extensions; } private set { _extensions = value; WriteListOfStringsToStringCollectionSetting(_extensions, Settings.Default.extensions); } }

        public static List<string> ConvertStringCollectionToListOfStrings(StringCollection stringCollection)
        {
            return stringCollection.Cast<string>().ToList();
        }

        public static void SetDefaultExtensionsAll()
        {
            Extension = DefaultExtensions.DefaultExtension;
            WriteListOfStringsToStringCollectionSetting(Extension, Settings.Default.extensions);
        }
        static void WriteListOfStringsToStringCollectionSetting(List<String> listOfStrings, StringCollection stringCollectionSetting)
        {
            stringCollectionSetting.Clear();
            foreach (string listOfStringsMember in listOfStrings)
                stringCollectionSetting.Add(listOfStringsMember);
            Settings.Default.Save();
            Settings.Default.Reload();
            Settings.Default.Upgrade();
        }
    }
}
