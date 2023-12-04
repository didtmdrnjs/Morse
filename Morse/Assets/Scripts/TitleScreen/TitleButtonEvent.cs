using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonEvent : MonoBehaviour
{
    public void GameStart()
    {
        Singleton<MusicInfo>.instance.isLoadScene = true;
        SceneManager.LoadScene("SelectMusic");
    }

    public void OpenSettingWindow()
    {
        GameObject window = Singleton<TitleManager>.instance.Settings;
        window.SetActive(true);
        Singleton<WindowManager>.instance.ActiveWindow = window;
    }

    public void OpenHelperWindow()
    {
        GameObject window = Singleton<TitleManager>.instance.Helper;
        window.SetActive(true);
        Singleton<WindowManager>.instance.ActiveWindow = window;
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif  
    }
}
