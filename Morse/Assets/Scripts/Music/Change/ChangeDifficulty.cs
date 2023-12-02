using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeDifficulty : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(0).GetComponent<Image>().color = Color.green;
        transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 0.1f);
    }

    private void Update()
    {
        Change();
    }

    private void Change()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            Singleton<MusicInfo>.instance.difficulty = 0;
            transform.GetChild(0).GetComponent<Image>().color = Color.green;
            transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 0.1f);
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            Singleton<MusicInfo>.instance.difficulty = 1;
            transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 0.1f);
            transform.GetChild(1).GetComponent<Image>().color = Color.red;
        }
    }
}
