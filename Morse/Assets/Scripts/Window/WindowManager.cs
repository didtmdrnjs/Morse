using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager instance;
    public GameObject ActiveWindow;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (ActiveWindow != null)
        {
            ActiveWindow.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ActiveWindow.SetActive(false);
                ActiveWindow = null;
            }
        }
    }
}
