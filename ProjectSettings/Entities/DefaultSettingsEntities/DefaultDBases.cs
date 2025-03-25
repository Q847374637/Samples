using System;
using System.Collections.Generic;
using System.Linq;
using Settings = ProjectSettings.Properties.Settings;

namespace ProjectSettings.Entities.DefaultSettingsEntities
{
    public static class DefaultDBases
    {
        static List<String> _defaultDBasesEasypay = ConvertStringCollectionToListOfStrings(Settings.Default.defaultPathGetEasypay);
        static List<String> _defaultDBasesDelay = ConvertStringCollectionToListOfStrings(Settings.Default.defaultPathGetDelay);

        public static List<String> DefaultDbasesEasypay { get { return _defaultDBasesEasypay; } private set { _defaultDBasesEasypay = value; } }
        public static List<String> DefaultDBasesDelay { get { return _defaultDBasesDelay; } private set { _defaultDBasesDelay = value; } }

        public static List<string> ConvertStringCollectionToListOfStrings(System.Collections.Specialized.StringCollection stringCollection)
        {
            return stringCollection.Cast<string>().ToList();
        }
    }
}
