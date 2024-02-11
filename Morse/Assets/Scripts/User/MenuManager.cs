using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public Image TitleButton;

    public bool isVolum;
    public bool isKey;
    public bool isTitle;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Start()
    {
        gameObject.SetActive(false);
        isVolum = true;
        isKey = false;
        isTitle = false;
    }

    private void Update()
    {
        ChangeSelected();
        TItleAction();
    }

    public void ChangeSelected()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isKey)
            {
                isVolum = true;
                isKey = false;
                isTitle = false;
            }
            else if (isTitle)
            {
                isVolum = false;
                isKey = true;
                isTitle = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (isVolum)
            {
                isVolum = false;
                isKey = true;
                isTitle = false;
            }
            else if (isKey)
            {
                isVolum = false;
                isKey = false;
                isTitle = true;
            }
        }
    }

    public void TItleAction()
    {
        if (isTitle)
        {
            TitleButton.color = new Color(1, 190 / 255f, 0, 1);
            if (Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene("Title");
        }
        else TitleButton.color = Color.white;
    }
}
