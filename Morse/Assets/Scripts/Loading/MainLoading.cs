using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainLoading : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<Image>().fillAmount = 0;
    }

    private void Update()
    {
        if (MusicInfo.instance.isLoadTotal) StartCoroutine(Loading()); 
    }

    IEnumerator Loading()
    {
        MusicInfo.instance.isLoadTotal = false;

        yield return new WaitForSeconds(0.5f);

        for (float i = gameObject.GetComponent<Image>().fillAmount; i < MusicInfo.instance.currentLoadElement / MusicInfo.instance.totalLoadElement; i += 0.001f)
        {
            yield return new WaitForSeconds(0.01f);
            gameObject.GetComponent<Image>().fillAmount = (float)Math.Round(i, 3);
            gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Math.Round((gameObject.GetComponent<Image>().fillAmount * 100), 1) + "%";
        }

        if (gameObject.GetComponent<Image>().fillAmount == 1) StartCoroutine(Success());
    }

    IEnumerator Success()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Title");
    }
}
