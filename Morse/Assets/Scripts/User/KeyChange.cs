using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class KeyChange : MonoBehaviour
{
    public List<GameObject> buttons;
    public List<TMP_Text> keys;

    private int idx;
    private bool isPressEnter;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (MenuManager.instance.isKey)
        {
            if (isPressEnter)
            {
                isPressEnter = false;
                StartCoroutine(WaitInput());
            }
            ChangeButton();
            InvokeButton();
            ShowKey();
            foreach (GameObject button in buttons)
            {
                button.GetComponent<Image>().color = Color.white;
            }
            buttons[idx].GetComponent<Image>().color = new Color(1, 190 / 255f, 0, 1);
        }
        else
        {
            foreach (GameObject button in buttons)
            {
                button.GetComponent<Image>().color = Color.white;
            }
        }
    }

    private void Init()
    {
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().onClick.AddListener(() => isPressEnter = true);
        }
        ShowKey();
    }

    private void InvokeButton()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            buttons[idx].GetComponent<Button>().onClick?.Invoke();
        }
    }

    IEnumerator WaitInput()
    {
        yield return new WaitUntil(() => Input.anyKeyDown);

        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                if (idx == 0) User.instance.userSetting.OneShortKey = keyCode;
                else if (idx == 1) User.instance.userSetting.OneLongKey = keyCode;
                else if (idx == 2) User.instance.userSetting.TwoFirstShortKey = keyCode;
                else if (idx == 3) User.instance.userSetting.TwoFirstLongKey = keyCode;
                else if (idx == 4) User.instance.userSetting.TwoSecondShortKey = keyCode;
                else if (idx == 5) User.instance.userSetting.TwoSecondLongKey = keyCode;
                break;
            }
        } 
    }

    private void ChangeButton()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && idx > 0) idx--;
        if (Input.GetKeyDown(KeyCode.RightArrow) && idx < 5) idx++;
    }


    private void ShowKey()
    {
        keys[0].text = User.instance.userSetting.OneShortKey.ToString();
        keys[1].text = User.instance.userSetting.OneLongKey.ToString();
        keys[2].text = User.instance.userSetting.TwoFirstShortKey.ToString();
        keys[3].text = User.instance.userSetting.TwoFirstLongKey.ToString();
        keys[4].text = User.instance.userSetting.TwoSecondShortKey.ToString();
        keys[5].text = User.instance.userSetting.TwoSecondLongKey.ToString();
    }
}
