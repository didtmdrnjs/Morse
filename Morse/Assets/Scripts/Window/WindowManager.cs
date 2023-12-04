using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : Singleton<WindowManager>
{
    public GameObject ActiveWindow;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ActiveWindow.SetActive(false);
            ActiveWindow = null;
        }
    }
}
