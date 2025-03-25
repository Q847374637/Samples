using System;
using System.Collections.Generic;
using System.Linq;
using Settings = ProjectSettings.Properties.Settings;

namespace ProjectSettings.Entities.DefaultSettingsEntities
{
    public static class DefaultExtensions
    {
        static List<String> _defaultExtensions = ConvertStringCollectionToListOfStrings(Settings.Default.defaultExtensions);

        public static List<String> DefaultExtension { get { return _defaultExtensions; } private set { _defaultExtensions = value; } }

        public static List<string> ConvertStringCollectionToListOfStrings(System.Collections.Specialized.StringCollection stringCollection)
        {
            return stringCollection.Cast<string>().ToList();
        }
    }
}
