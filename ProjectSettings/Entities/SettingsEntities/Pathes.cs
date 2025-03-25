using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Settings = ProjectSettings.Properties.Settings;
using ProjectSettings.Entities.DefaultSettingsEntities;


namespace ProjectSettings.Entities.SettingsEntities
{
    public static class Pathes
    {
        static List<String> _pathGetEasypay = ConvertStringCollectionToListOfStrings(Settings.Default.pathGetEasypay);
        static List<String> _pathGetDelay = ConvertStringCollectionToListOfStrings(Settings.Default.pathGetDelay);
        static List<String> _pathCopyEasypay = ConvertStringCollectionToListOfStrings(Settings.Default.pathCopyEasypay);
        static List<String> _pathCopyDelay = ConvertStringCollectionToListOfStrings(Settings.Default.pathCopyDelay);

        public static List<String> PathGetEasypay { get { return _pathGetEasypay; } private set { _pathGetEasypay = value; WriteListOfStringsToStringCollectionSetting(_pathGetEasypay, Settings.Default.pathGetEasypay); } }
        public static List<String> PathGetDelay { get { return _pathGetDelay; } private set { _pathGetDelay = value; WriteListOfStringsToStringCollectionSetting(_pathGetDelay, Settings.Default.pathGetDelay); } }
        public static List<String> PathCopyEasypay { get { return _pathCopyEasypay; } private set { _pathCopyEasypay = value; WriteListOfStringsToStringCollectionSetting(_pathCopyEasypay, Settings.Default.pathCopyEasypay); } }
        public static List<String> PathCopyDelay { get { return _pathCopyDelay; } private set { _pathCopyDelay = value; WriteListOfStringsToStringCollectionSetting(_pathCopyDelay, Settings.Default.pathCopyDelay); } }

        public static List<string> ConvertStringCollectionToListOfStrings(StringCollection stringCollection)
        {
            return stringCollection.Cast<string>().ToList();
        }

        public static void SetDefaultPathGetEasypay()
        {
            PathGetEasypay = DefaultPathes.DefaultPathGetEasypay;
            WriteListOfStringsToStringCollectionSetting(PathGetEasypay, Settings.Default.defaultPathGetEasypay);
        }
        public static void SetDefaultPathGetDelay()
        {
            PathGetDelay = DefaultPathes.DefaultPathGetDelay;
            WriteListOfStringsToStringCollectionSetting(PathGetDelay, Settings.Default.defaultPathGetDelay);
        }
        public static void SetDefaultPathCopyEasypay()
        {
            PathCopyEasypay = DefaultPathes.DefaultPathCopyEasypay;
            WriteListOfStringsToStringCollectionSetting(PathCopyEasypay, Settings.Default.defaultPathCopyEasypay);
        }
        public static void SetDefaultPathCopyDelay()
        {
            PathCopyDelay = DefaultPathes.DefaultPathCopyDelay;
            WriteListOfStringsToStringCollectionSetting(PathCopyDelay, Settings.Default.defaultPathCopyDelay);
        }
        public static void SetDefaultPathesAll()
        {
            SetDefaultPathGetEasypay();
            SetDefaultPathGetDelay();
            SetDefaultPathCopyEasypay();
            SetDefaultPathCopyDelay();
        }

        public static void ModifyPathCopyDelay(int listElementIndex, string elementValue)
        {
            if (!elementValue.EndsWith("\\"))
                elementValue += "\\";

            PathGetDelay.RemoveAt(listElementIndex);
            PathGetDelay.Insert(listElementIndex, elementValue);
            WriteListOfStringsToStringCollectionSetting(PathGetDelay, Settings.Default.pathGetDelay);
        }

        public static void ModifyPathCopyEasypay(int listElementIndex, string elementValue)
        {
            if (!elementValue.EndsWith("\\"))
                elementValue += "\\";
            var a = Settings.Default.pathGetEasypay;
            PathGetEasypay.RemoveAt(listElementIndex);
            PathGetEasypay.Insert(listElementIndex, elementValue);
            WriteListOfStringsToStringCollectionSetting(PathGetEasypay, Settings.Default.pathGetEasypay);
        }

        static void WriteListOfStringsToStringCollectionSetting(List<String> listOfStrings, StringCollection stringCollectionSetting)
        {
            stringCollectionSetting.Clear();
            foreach (string listOfStringsMember in listOfStrings)
                stringCollectionSetting.Add(listOfStringsMember);
            Settings.Default.Save();
            Settings.Default.Upgrade();
            Settings.Default.Reload();
        }
    }
}
