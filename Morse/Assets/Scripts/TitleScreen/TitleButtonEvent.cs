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
        GameObject window = Instantiate(Singleton<TitleManager>.instance.Settings, Singleton<WindowManager>.instance.Parent);
        window.SetActive(true);
        Singleton<WindowManager>.instance.OpenWindowStack.Push(window);
    }

    public void OpenHelperWindow()
    {
        GameObject window = Instantiate(Singleton<TitleManager>.instance.Helper, Singleton<WindowManager>.instance.Parent);
        window.SetActive(true);
        Singleton<WindowManager>.instance.OpenWindowStack.Push(window);
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
