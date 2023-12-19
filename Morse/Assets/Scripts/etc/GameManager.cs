using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool isPlayMusic;

    private void Update()
    {
        StartCorrection();
    }

    private void StartCorrection()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isPlayMusic) SceneManager.LoadScene("Correction");
    }
}
