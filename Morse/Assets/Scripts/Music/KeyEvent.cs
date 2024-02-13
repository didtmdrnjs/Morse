using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyEvent : MonoBehaviour
{
    public GameObject menu;

    private void Update()
    {
        if (!menu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameManager.instance.isPlayMusic = true;
                SceneManager.LoadScene("Play");
            }

        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf) menu.SetActive(false);
            else menu.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            MusicInfo.instance.currentMusicIndex = Random.Range(0, MusicInfo.instance.datas.Count);
            GameManager.instance.isPlayMusic = true;
            SceneManager.LoadScene("Play");
        }
    }
}
