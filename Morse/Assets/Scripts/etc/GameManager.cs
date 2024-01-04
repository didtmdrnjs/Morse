using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool isPlayMusic;
    public string lastSceneName;

    private void Update()
    {
        StartCorrection();
    }

    private void StartCorrection()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isPlayMusic)
        {
            lastSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Correction");
        }
    }
}
