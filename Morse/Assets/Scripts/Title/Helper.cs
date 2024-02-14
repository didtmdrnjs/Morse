using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Helper : MonoBehaviour
{
    public List<Sprite> imgs;
    public Image messageImg;
    public TMP_Text page;

    private int idx;

    private void OnEnable()
    {
        idx = 0;
        SetImage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && idx > 0)
        {
            idx--;
            SetImage();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && idx < imgs.Count - 1)
        {
            idx++;
            SetImage();
        }
    }

    private void SetImage()
    {
        messageImg.sprite = imgs[idx];
        page.text = "< 0" + (idx + 1) + " / 0" + imgs.Count + " >";
    }
}
