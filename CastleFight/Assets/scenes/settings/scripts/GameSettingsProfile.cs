using System;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.scenes.settings.scripts
{
    public static class GameSettingsProfile
    {
        static GameSettingsProfile()
        {
            var lang = PlayerPrefs.GetInt("Language", 0);

            if (lang < 0 || lang >= Enum.GetValues(typeof (LanguageEnum)).Length)
            {
                lang = 0;
            }

            Language = (LanguageEnum) lang;


            var dif = PlayerPrefs.GetInt("Difficulty", 0);

            if (dif < 0 || dif >= Enum.GetValues(typeof (GameDifficultyEnum)).Length)
            {
                dif = 0;
            }

            Difficulty = (GameDifficultyEnum) dif;


            var vol = PlayerPrefs.GetInt("Volume", 100);

            if (vol < 0 || vol > 100)
            {
                vol = 100;
            }

            Volume100 = vol;

            savingLocker = new object();

            SaveToPrefs();
        }

        public static LanguageEnum Language;

        // Volume [0; 100]
        public static int Volume100;

        public static GameDifficultyEnum Difficulty;

        private static object savingLocker;

        public static void SaveToPrefs()
        {
            #region Code breakes here...PlayerPrefs should be used in "Main" thread.

            // Code breakes here...PlayerPrefs should be used in "Main" thread.
            //lock (savingLocker)
            //{
            //    var t = new Thread(() =>
            //    {
            //        PlayerPrefs.SetInt("Language", (int)Language);
            //        PlayerPrefs.SetInt("Difficulty", (int)Difficulty);
            //        PlayerPrefs.SetInt("Volume", Volume100);
            //        PlayerPrefs.Save();
            //    })
            //    {
            //        IsBackground = true,
            //    };

            //    t.Start();
            //}

            #endregion


            PlayerPrefs.SetInt("Language", (int)Language);
            PlayerPrefs.SetInt("Difficulty", (int)Difficulty);
            PlayerPrefs.SetInt("Volume", Volume100);
            PlayerPrefs.Save();
        }

    }

    public enum LanguageEnum
    {
        [XmlEnum("LanguageEnum.English")]
        English,
        [XmlEnum("LanguageEnum.Russian")]
        Russian,
        [XmlEnum("LanguageEnum.Spain")]
        Spain,
        [XmlEnum("LanguageEnum.German")]
        German,
        [XmlEnum("LanguageEnum.France")]
        France,
    }

    public enum GameDifficultyEnum
    {
        [XmlEnum("GameDifficultyEnum.Easy")]
        Easy,
        [XmlEnum("GameDifficultyEnum.Normal")]
        Normal,
        [XmlEnum("GameDifficultyEnum.Hard")]
        Hard,
    }

    public static class EnumsHelper
    {
        // http://stackoverflow.com/questions/18737950/get-xmlenumattribute-from-enum
        public static string GetXmlEnumAttributeValueFromEnum<TEnum>(this TEnum value) where TEnum : struct, IConvertible
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum) return null;//or string.Empty, or throw exception

            var member = enumType.GetMember(value.ToString()).FirstOrDefault();
            if (member == null) return null;//or string.Empty, or throw exception

            var attribute = member.GetCustomAttributes(false).OfType<XmlEnumAttribute>().FirstOrDefault();
            if (attribute == null) return null;//or string.Empty, or throw exception
            return attribute.Name;
        }

    }
}
