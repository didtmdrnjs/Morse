using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Images : MonoBehaviour
{
    public List<Sprite> imgs;
    
    private int idx;

    private void Start()
    {
        StartCoroutine(ShowImg());
    }

    IEnumerator ShowImg()
    {
        GetComponent<Image>().sprite = imgs[idx];
        FaidIn();
        yield return new WaitForSeconds(5f);
        FaidOut();
        idx++;
        if (idx >= imgs.Count) idx = 0;
        StartCoroutine(ShowImg());
    }

    private void FaidIn()
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, i);
        }
    }

    private void FaidOut()
    {
        for (float i = 1; i > 0; i -= 0.01f)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, i);
        }
    }
}
