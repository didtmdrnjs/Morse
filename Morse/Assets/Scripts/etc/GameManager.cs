using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isPlayMusic;
    public string lastSceneName;
    public float offset;

    public int difficulty;
    public EMode mode;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) Destroy(gameObject);

        difficulty = 0;
        mode = EMode.OneWord;
    }

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
