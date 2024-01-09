using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyEvent : MonoBehaviour
{
    [SerializeField] private GameObject Options;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.instance.isPlayMusic = true;
            SceneManager.LoadScene("Play");
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MusicInfo musicInfo = Singleton<MusicInfo>.instance;
            if (musicInfo.mode == Mode.OneWord) musicInfo.mode = Mode.TwoWord;
            else musicInfo.mode = Mode.OneWord;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Options.activeSelf) Options.SetActive(false);
            else Options.SetActive(true);
        }
    }
}
