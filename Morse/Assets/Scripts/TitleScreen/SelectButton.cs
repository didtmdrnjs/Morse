using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    private int ButtonIdx;

    private void Awake()
    {
        ButtonIdx = 0;
    }

    private void Update()
    {
        if (Singleton<WindowManager>.instance.ActiveWindow == null)
        {
            MovePointer();
            ActiveButton();
        }
    }

    private void MovePointer() { 
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (ButtonIdx > 0)
            {
                gameObject.transform.GetChild(ButtonIdx).GetComponent<Image>().color = new Color(1, 1, 1);
                ButtonIdx--;
                gameObject.transform.GetChild(ButtonIdx).GetComponent<Image>().color = new Color(1, 0.745f, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (ButtonIdx < 3) 
            {
                gameObject.transform.GetChild(ButtonIdx).GetComponent<Image>().color = new Color(1, 1, 1);
                ButtonIdx++;
                gameObject.transform.GetChild(ButtonIdx).GetComponent<Image>().color = new Color(1, 0.745f, 0);
            }
        }
    }

    private void ActiveButton()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameObject.transform.GetChild(ButtonIdx).GetComponent<Button>().onClick.Invoke();
        }
    }
}
