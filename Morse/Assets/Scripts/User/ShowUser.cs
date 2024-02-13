using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowUser : MonoBehaviour
{
    public TMP_Text username;
    public TMP_Text lv;
    public Image Icon;
    public Image levelbar;

    public List<Sprite> icons;

    private void Start()
    {
        username.text = User.instance.userInfo.name;
    }

    private void Update()
    {
        lv.text = "LV." + User.instance.userInfo.level;
        levelbar.fillAmount = (float)User.instance.userInfo.exp / User.instance.levels.datas[User.instance.userInfo.level].maxExp;
        SetIcon();
    }

    private void SetIcon()
    {
        int level = User.instance.userInfo.level;
        if (level == 50) Icon.sprite = icons[5];
        else if (level >= 40) Icon.sprite = icons[4];
        else if (level >= 30) Icon.sprite = icons[3];
        else if (level >= 20) Icon.sprite = icons[2];
        else if (level >= 10) Icon.sprite = icons[1];
        else  Icon.sprite = icons[0];
    }
}