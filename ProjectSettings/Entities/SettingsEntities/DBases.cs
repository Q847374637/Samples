using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Settings = ProjectSettings.Properties.Settings;
using ProjectSettings.Entities.DefaultSettingsEntities;

namespace ProjectSettings.Entities.SettingsEntities
{
    public static class DBases
    {
        static List<String> _dBasesEasypay = ConvertStringCollectionToListOfStrings(Settings.Default.dBasesEasypay);
        static List<String> _dBasesDelay = ConvertStringCollectionToListOfStrings(Settings.Default.dBasesDelay);

        public static List<String> DbasesEasypay { get { return _dBasesEasypay; } private set { _dBasesEasypay = value; WriteListOfStringsToStringCollectionSetting(_dBasesEasypay, Settings.Default.dBasesEasypay); } }
        public static List<String> DBasesDelay { get { return _dBasesDelay; } private set { _dBasesDelay = value; WriteListOfStringsToStringCollectionSetting(_dBasesDelay, Settings.Default.dBasesDelay); } }

        public static List<string> ConvertStringCollectionToListOfStrings(StringCollection stringCollection)
        {
            return stringCollection.Cast<string>().ToList();
        }

        public static void SetDefaultDbasesEasypay()
        {
            DbasesEasypay = DefaultDBases.DefaultDbasesEasypay;
            WriteListOfStringsToStringCollectionSetting(DbasesEasypay, Settings.Default.dBasesEasypay);
        }
        public static void SetDefaultDBasesDelay()
        {
            DBasesDelay = DefaultDBases.DefaultDBasesDelay;
            WriteListOfStringsToStringCollectionSetting(DBasesDelay, Settings.Default.dBasesDelay);
        }
        public static void SetDefaultDbasesAll()
        {
            SetDefaultDbasesEasypay();
            SetDefaultDBasesDelay();
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
