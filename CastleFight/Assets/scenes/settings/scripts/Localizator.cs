using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scenes.settings.scripts
{
    public static class Localizator
    {
        static Localizator()
        {
            EngDictionary = new Dictionary<string, string>();
            RusDictionary = new Dictionary<string, string>();
            SpainDictionary = new Dictionary<string, string>();
            GerDictionary = new Dictionary<string, string>();
            FraDictionary = new Dictionary<string, string>();
            

            // TODO: Read localization text file
            //  original identificator
            //  eng
            //  rus
            //  spain
            //  ger
            //  fra
            //  empty

            var localizationFile = Resources.Load<TextAsset>("localization.txt");

            var allText = "";

            if (localizationFile == null)
            {
                Debug.LogError("Cannot load localization.txt");
            }
            else
            {
                allText = localizationFile.text;
            }

            var lines = allText.Split('\n');

            // Ignoring last (<7) lines
            var linesCount = (int) Mathf.Floor(lines.Length / 7f) * 7;

            for (int i = 0; i < linesCount; i += 7)
            {
                var index = 0;

                var originalId = lines[i + index];
                index++;

                var eng = lines[i + index];
                index++;

                var rus = lines[i + index];
                index++;

                var spain = lines[i + index];
                index++;

                var ger = lines[i + index];
                index++;

                var fra = lines[i + index];


                EngDictionary.Add(originalId, eng);
                RusDictionary.Add(originalId, rus);
                SpainDictionary.Add(originalId, spain);
                GerDictionary.Add(originalId, ger);
                FraDictionary.Add(originalId, fra);
            }

            switch (GameSettingsProfile.Language)
            {
                case LanguageEnum.English:
                    CurrentDictionary = EngDictionary;
                    break;
                case LanguageEnum.Russian:
                    CurrentDictionary = RusDictionary;
                    break;
                case LanguageEnum.Spain:
                    CurrentDictionary = SpainDictionary;
                    break;
                case LanguageEnum.German:
                    CurrentDictionary = GerDictionary;
                    break;
                case LanguageEnum.France:
                    CurrentDictionary = FraDictionary;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


        }

        private static Dictionary<string, string> EngDictionary;
        private static Dictionary<string, string> RusDictionary;
        private static Dictionary<string, string> SpainDictionary;
        private static Dictionary<string, string> GerDictionary;
        private static Dictionary<string, string> FraDictionary;

        private static Dictionary<string, string> CurrentDictionary;

        public static string GetLocalizedString(string textString)
        {
            if (string.IsNullOrEmpty(textString))
            {
                Debug.LogError("string.IsNullOrEmpty(textString)");

                return string.Empty;
            }

            if (CurrentDictionary.ContainsKey(textString) ==false)
            {
                Debug.LogError(string.Format("Cannot find \"{0}\"", textString));


                return textString + " - " + GameSettingsProfile.Language.ToString();
            }

            return CurrentDictionary[textString];
        }
    }
}