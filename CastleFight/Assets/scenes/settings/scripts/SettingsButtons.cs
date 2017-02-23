using System;
using Assets.common_data;
using Assets.scenes.common_scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.scenes.settings.scripts
{
    public class SettingsButtons : MonoBehaviour
    {
        public SettingsGuiLocalizator SettingsGuiLocalizator;

        public Button NextLang;
        public Button PrevLang;
        public Text LanguageValueText;

        public Slider VolumeSlider;

        public Button NextDifficulty;
        public Button PrevDifficulty;
        public Text DifficultyValueText;

        public Button ExitButton;

        void Awake ()
        {
            DisplayCurrentGameSettings();

            if (SettingsGuiLocalizator == null)
            {
                SettingsGuiLocalizator = FindObjectOfType<SettingsGuiLocalizator>();
            }

            NextLang.onClick.AddListener(NextLangClick);
            PrevLang.onClick.AddListener(PrevLangClick);

            VolumeSlider.onValueChanged.AddListener(VolumeSliderValueChanged);

            NextDifficulty.onClick.AddListener(NextDifficultyClick);
            PrevDifficulty.onClick.AddListener(PrevDifficultyClick);

            ExitButton.onClick.AddListener(ExitClick);
        }

        


        private void DisplayCurrentGameSettings()
        {
            UpdateUI();

            var lang = GameSettingsProfile.Language;

            LanguageValueText.text = Localizator.GetLocalizedString(lang.GetXmlEnumAttributeValueFromEnum());

            var vol = GameSettingsProfile.Volume100;

            VolumeSlider.value = vol;

            var difficulty = GameSettingsProfile.Difficulty;

            DifficultyValueText.text = Localizator.GetLocalizedString(difficulty.GetXmlEnumAttributeValueFromEnum());
        }

        private void PrevLangClick()
        {
            var val = (int) GameSettingsProfile.Language;

            val--;

            SetLanguageValue(val);
        }

        private void NextLangClick()
        {
            var val = (int) GameSettingsProfile.Language;

            val--;

            SetLanguageValue(val);
        }

        private void SetLanguageValue(int val)
        {
            var valuesCount = Enum.GetValues(typeof (LanguageEnum)).Length;

            val += valuesCount;

            val = val%valuesCount;

            GameSettingsProfile.Language = (LanguageEnum)val;

            LanguageValueText.text = Localizator.GetLocalizedString(GameSettingsProfile.Language.GetXmlEnumAttributeValueFromEnum());

            UpdateUI();
        }

        // idk why they require float value, lol
        private void VolumeSliderValueChanged(float value)
        {
            var volume = (int)VolumeSlider.value;

            var audioSources = GameObject.FindObjectsOfType<AudioSource>();

            foreach (var audioSource in audioSources)
            {
                audioSource.volume = volume/100f;
            }

            GameSettingsProfile.Volume100 = volume;
        }
        
        private void NextDifficultyClick()
        {
            var val = (int) GameSettingsProfile.Difficulty;

            val++;

            SetDifficultyValue(val);
        }

        private void PrevDifficultyClick()
        {
            var val = (int)GameSettingsProfile.Difficulty;

            val++;

            SetDifficultyValue(val);
        }

        private void SetDifficultyValue(int val)
        {
            var valuesCount = Enum.GetValues(typeof (GameDifficultyEnum)).Length;

            val += valuesCount;

            val = val%valuesCount;

            GameSettingsProfile.Difficulty = (GameDifficultyEnum)val;

            DifficultyValueText.text =
                Localizator.GetLocalizedString(GameSettingsProfile.Difficulty.GetXmlEnumAttributeValueFromEnum());
        }

        private void ExitClick()
        {
            GameSettingsProfile.SaveToPrefs();

            MySceneManager.LoadScene(ScenesEnum.MainMenu);
        }


        public void UpdateUI()
        {
            SettingsGuiLocalizator.LocalizeGUI();
        }
    }
}
