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
            if (GameManager.instance.mode == EMode.OneWord) GameManager.instance.mode = EMode.TwoWord;
            else GameManager.instance.mode = EMode.OneWord;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Options.activeSelf) Options.SetActive(false);
            else Options.SetActive(true);
        }
    }
}
