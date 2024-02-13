using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public static Title instance;

    public GameObject Settings;
    public GameObject Helper;
    public GameObject InputName;

    public bool isEndWriteUser;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Settings.SetActive(false);
        Helper.SetActive(false);
        InputName.SetActive(false);
    }

    public void GameStart()
    {
        MusicInfo.instance.isLoadScene = true;
        SceneManager.LoadScene("SelectMusic");
    }

    public void OpenSettingWindow()
    {
        GameObject window = Settings;
        WindowManager.instance.ActiveWindow = window;
    }

    public void OpenHelperWindow()
    {
        GameObject window = Helper;
        window.SetActive(true);
        WindowManager.instance.ActiveWindow = window;
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
