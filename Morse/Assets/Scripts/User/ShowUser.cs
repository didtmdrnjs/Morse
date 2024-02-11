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

    private void Start()
    {
        username.text = User.instance.userInfo.name;
        lv.text = "LV." + User.instance.userInfo.level;
    }
}
