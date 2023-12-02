using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleManager : Singleton<TitleManager>
{
    public GameObject Options;
    public GameObject ClickMessage;
    public GameObject Settings;
    public GameObject Helper;

    private void Start()
    {
        Options.SetActive(false);
        Settings.SetActive(false);
        Helper.SetActive(false);
        ClickMessage.SetActive(true);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Options.SetActive(true);
            ClickMessage.SetActive(false);
        }
    }
}
