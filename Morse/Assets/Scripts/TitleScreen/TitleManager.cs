using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleManager : Singleton<TitleManager>
{
    public GameObject Settings;
    public GameObject Helper;
    public GameObject InputName;

    public bool isEndWriteUser;

    private void Start()
    {
        Settings.SetActive(false);
        Helper.SetActive(false);
        InputName.SetActive(false);
    }
}
