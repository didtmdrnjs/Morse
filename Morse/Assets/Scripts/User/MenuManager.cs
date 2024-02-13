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

    public Image Button;

    public bool isVolum;
    public bool isKey;
    public bool isButton;

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
        isButton = false;
    }

    private void Update()
    {
        ChangeSelected();
        TItleAction();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            User.instance.WriteUserData();
            gameObject.SetActive(false);
        }
    }

    public void ChangeSelected()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isKey)
            {
                isVolum = true;
                isKey = false;
                isButton = false;
            }
            else if (isButton)
            {
                isVolum = false;
                isKey = true;
                isButton = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (isVolum)
            {
                isVolum = false;
                isKey = true;
                isButton = false;
            }
            else if (isKey)
            {
                isVolum = false;
                isKey = false;
                isButton = true;
            }
        }
    }

    public void TItleAction()
    {
        if (isButton)
        {
            Button.color = new Color(1, 190 / 255f, 0, 1);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (SceneManager.GetActiveScene().name == "SelectMusic") SceneManager.LoadScene("Title");
                else
                {
                    GameManager.instance.lastSceneName = "Title";
                    SceneManager.LoadScene("Correction");
                }
            }
        }
        else Button.color = Color.white;
    }
}
