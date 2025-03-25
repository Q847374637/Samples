using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Settings = ProjectSettings.Properties.Settings;
using ProjectSettings.Entities.DefaultSettingsEntities;

namespace ProjectSettings.Entities.SettingsEntities
{
    public static class Columns
    {
        static List<String> _columnsEasypay = ConvertStringCollectionToListOfStrings(Settings.Default.columnsEasypay);
        static List<String> _columnsDelay = ConvertStringCollectionToListOfStrings(Settings.Default.columnsDelay);
        static List<String> _finalColumnsEasypay = ConvertStringCollectionToListOfStrings(Settings.Default.finalColumnsEasypay);
        static List<String> _finalColumnsDelay = ConvertStringCollectionToListOfStrings(Settings.Default.finalColumnsDelay);

        public static List<String> ColumnsEasypay { get { return _columnsEasypay; } private set { _columnsEasypay = value; WriteListOfStringsToStringCollectionSetting(_columnsEasypay, Settings.Default.columnsEasypay); } }
        public static List<String> ColumnsDelay { get { return _columnsDelay; } private set { _columnsDelay = value; WriteListOfStringsToStringCollectionSetting(_columnsDelay, Settings.Default.columnsDelay); } }
        public static List<String> FinalColumnsEasypay { get { return _finalColumnsEasypay; } private set { _finalColumnsEasypay = value; WriteListOfStringsToStringCollectionSetting(_finalColumnsEasypay, Settings.Default.finalColumnsEasypay); } }
        public static List<String> FinalColumnsDelay { get { return _finalColumnsDelay; } private set { _finalColumnsDelay = value; WriteListOfStringsToStringCollectionSetting(_finalColumnsDelay, Settings.Default.finalColumnsDelay); } }

        static List<string> ConvertStringCollectionToListOfStrings(StringCollection stringCollection)
        {
            return stringCollection.Cast<string>().ToList();
        }

        public static void SetDefaultColumnsEasypay()
        {
            ColumnsEasypay = DefaultColumns.DefaultColumnsEasypay;
            WriteListOfStringsToStringCollectionSetting(ColumnsEasypay, Settings.Default.columnsEasypay);
        }
        public static void SetDefaultColumnsDelay()
        {
            ColumnsDelay = DefaultColumns.DefaultColumnsDelay;
            WriteListOfStringsToStringCollectionSetting(ColumnsDelay, Settings.Default.columnsDelay);
        }
        public static void SetDefaultFinalColumnsEasypay()
        {
            FinalColumnsEasypay = DefaultColumns.DefaultFinalColumnsEasypay;
        }
        public static void SetDefaultFinalColumnsDelay()
        {
            FinalColumnsDelay = DefaultColumns.DefaultFinalColumnsDelay;
        }
        public static void SetDefaultColumnsAll()
        {
            SetDefaultColumnsEasypay();
            SetDefaultColumnsDelay();
            SetDefaultFinalColumnsEasypay();
            SetDefaultFinalColumnsDelay();
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
