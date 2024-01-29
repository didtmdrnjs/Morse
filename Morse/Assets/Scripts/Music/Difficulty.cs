using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(0).GetComponent<Image>().color = new Color(213 / 255f, 255 / 255f, 172 / 255f, 1f);
        transform.GetChild(0).GetComponentInChildren<TMP_Text>().color = Color.green;
        transform.GetChild(1).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.1f);
        transform.GetChild(1).GetComponentInChildren<TMP_Text>().color = new Color(1f, 1f, 1f, 0.1f);
    }

    private void Update()
    {
        Change();
    }

    private void Change()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            GameManager.instance.difficulty = 0;
            transform.GetChild(0).GetComponent<Image>().color = new Color(213 / 255f, 255 / 255f, 172 / 255f, 1f);
            transform.GetChild(0).GetComponentInChildren<TMP_Text>().color = Color.green;
            transform.GetChild(1).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.1f);
            transform.GetChild(1).GetComponentInChildren<TMP_Text>().color = new Color(1f, 1f, 1f, 0.1f);
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            GameManager.instance.difficulty = 1;
            transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.1f);
            transform.GetChild(0).GetComponentInChildren<TMP_Text>().color = new Color(1f, 1f, 1f, 0.1f);
            transform.GetChild(1).GetComponent<Image>().color = new Color(255 / 255f, 192 / 255f, 192 / 255f, 1);
            transform.GetChild(1).GetComponentInChildren<TMP_Text>().color = Color.red;
        }
    }
}
