using System;
using System.Collections.Generic;
using System.Linq;
using Settings = ProjectSettings.Properties.Settings;


namespace ProjectSettings.Entities.DefaultSettingsEntities
{
    public static class DefaultPathes
    {
        static List<String> _defaultPathGetEasypay = ConvertStringCollectionToListOfStrings(Settings.Default.defaultPathGetEasypay);
        static List<String> _defaultPathGetDelay = ConvertStringCollectionToListOfStrings(Settings.Default.defaultPathGetDelay);
        static List<String> _defaultPathCopyEasypay = ConvertStringCollectionToListOfStrings(Settings.Default.defaultPathCopyEasypay);
        static List<String> _defaultPathCopyDelay = ConvertStringCollectionToListOfStrings(Settings.Default.defaultPathCopyDelay);

        public static List<String> DefaultPathGetEasypay { get { return _defaultPathGetEasypay; } private set { _defaultPathGetEasypay = value; } }
        public static List<String> DefaultPathGetDelay { get { return _defaultPathGetDelay; } private set { _defaultPathGetDelay = value; } }
        public static List<String> DefaultPathCopyEasypay { get { return _defaultPathCopyEasypay; } private set { _defaultPathCopyEasypay = value; } }
        public static List<String> DefaultPathCopyDelay { get { return _defaultPathCopyDelay; } private set { _defaultPathCopyDelay = value; } }


        public static List<string> ConvertStringCollectionToListOfStrings(System.Collections.Specialized.StringCollection stringCollection)
        {
            return stringCollection.Cast<string>().ToList();
        }
    }
}
