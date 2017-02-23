using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.common_data
{
    public static class MySceneManager
    {
        public static void LoadScene(ScenesEnum scene)
        {
            var next_scene = "";

            switch (scene)
            {
                case ScenesEnum.MainMenu:
                    next_scene = "main_menu_scene";
                    break;
                case ScenesEnum.SelectPlayMode:
                    next_scene = "select_playmode_scene";
                    break;
                case ScenesEnum.SettingsMenu:
                    next_scene = "settings_scene";
                    break;
                case ScenesEnum.Shop:
                    next_scene = "shop_scene";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("scene", scene, null);
            }

            SceneManager.LoadSceneAsync(next_scene);
        }
    }


    public enum ScenesEnum
    {
        MainMenu,

        SelectPlayMode,

        SettingsMenu,

        Shop,
    }
}
