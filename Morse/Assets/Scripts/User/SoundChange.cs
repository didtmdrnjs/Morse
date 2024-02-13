using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundChange : MonoBehaviour
{
    public List<GameObject> volums;
    private int idx;

    private void Update()
    {
        idx = User.instance.userSetting.volum;
        if (MenuManager.instance.isVolum)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && idx < 10) User.instance.userSetting.volum++;
            if (Input.GetKeyDown(KeyCode.LeftArrow) && idx > 0) User.instance.userSetting.volum--;
            ChangeState(new Color(1, 190 / 255f, 0, 1));
        }
        else ChangeState(Color.white);
    }

    private void ChangeState(Color _color)
    {
        for (int i = 0; i < idx; i++) volums[i].GetComponent<Image>().color = _color;
        for (int i = idx; i < volums.Count; i++) volums[i].GetComponent<Image>().color = new Color(150 / 255f, 150 / 255f, 150 / 255f, 1);
    }
}
