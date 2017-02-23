using Assets.common_data;
using Assets.scenes.common_scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.scenes.main_menu.scripts
{
    public class MainMenuButtons : MonoBehaviour
    {
        public MainMenuLocalizator MainMenuLocalizator;

        public Button PlayButton;
        public Button ShopButton;
        public Button SettingsButton;
        public Button ExitButton;


        public GameObject ExitConfirmPanel;
        public Button ExitConfirmButton;
        public Button CancelExitButton;

        // Use this for initialization
        void Awake()
        {
            if (MainMenuLocalizator == null)
            {
                MainMenuLocalizator = GameObject.FindObjectOfType<MainMenuLocalizator>();
            }

            UpdateUI();

            ExitConfirmPanel.SetActive(false);

            PlayButton.onClick.AddListener(PlayClick);
            ShopButton.onClick.AddListener(ShopClick);
            SettingsButton.onClick.AddListener(SettingsClick);
            ExitButton.onClick.AddListener(ExitClick);

            ExitConfirmButton.onClick.AddListener(ExitConfirmClick);
            CancelExitButton.onClick.AddListener(CancelExitClick);
        }

        
        private void PlayClick()
        {
            MySceneManager.LoadScene(ScenesEnum.SelectPlayMode);
        }

        private void ShopClick()
        {
            MySceneManager.LoadScene(ScenesEnum.Shop);
        }

        private void SettingsClick()
        {
            MySceneManager.LoadScene(ScenesEnum.SettingsMenu);
        }

        private void ExitClick()
        {
            DisplayConfirmExitPanel();
        }

        private void DisplayConfirmExitPanel()
        {
            ExitConfirmPanel.SetActive(true);

            // Disable all other buttons to prevent click on them, while showing "confirm exit" panel
            foreach (var btn in GameObject.FindObjectsOfType<Button>())
            {
                if (btn == ExitConfirmButton || btn == CancelExitButton)
                {
                    continue;
                }

                btn.enabled = false;
            }

        }

        private void ExitConfirmClick()
        {
            Application.Quit();
        }

        private void CancelExitClick()
        {
            HideConfirmExitPanel();
        }

        private void HideConfirmExitPanel()
        {
            ExitConfirmPanel.SetActive(false);


            // Enable back all other buttons to enable back click on them, after hiding "confirm exit" panel
            foreach (var btn in GameObject.FindObjectsOfType<Button>())
            {
                if (btn == ExitConfirmButton || btn == CancelExitButton)
                {
                    continue;
                }

                btn.enabled = true;
            }
        }

        private void UpdateUI()
        {
            MainMenuLocalizator.LocalizeGUI();
        }

    }
}
