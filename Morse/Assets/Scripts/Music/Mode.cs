using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mode : MonoBehaviour
{
    public Image left;
    public Image center;
    public Image right;

    public Sprite mode_1;
    public Sprite mode_2;

    private void Start()
    {
        GameManager.instance.mode = EMode.OneWord;
        left.sprite = mode_1;
        center.sprite = mode_1;
        right.sprite = mode_1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameManager.instance.mode == 0)
            {
                GameManager.instance.mode = EMode.TwoWord;
                left.sprite = mode_2;
                center.sprite = mode_2;
                right.sprite = mode_2;
            }
            else
            {
                GameManager.instance.mode = EMode.OneWord;
                left.sprite = mode_1;
                center.sprite = mode_1;
                right.sprite = mode_1;
            }
        }
    }
}
