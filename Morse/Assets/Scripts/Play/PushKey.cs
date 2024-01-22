using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.TerrainTools;
using UnityEngine;

public class PushKey : MonoBehaviour
{
    private float bpm;

    private void Start()
    {
        bpm = PlayManager.instance.bpm;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && PlayManager.instance.currentCode == '¡¤')
        {
            PlayManager.instance.isInput = true;
            float inputTime = PlayManager.instance.offsetTime;
            float verdict = inputTime >= (60 / bpm / 2) ? (60 / bpm) - inputTime : inputTime;
            if (verdict <= (60 / bpm / 7))
            {
                GetComponent<TMP_Text>().text = "<color=green>perfect</color>";
                GameManager.instance.perfectCount++;
            }
            else if (verdict <= (60 / bpm / 4))
            {
                GetComponent<TMP_Text>().text = "<color=#64FF00>greate</color>";
                GameManager.instance.greateCount++;
            }
            else if (verdict <= (60 / bpm / 2.5))
            {
                GetComponent<TMP_Text>().text = "<color=#FF6400>good</color>";
                GameManager.instance.goodCount++;
            }
        }

        if (Input.GetKeyDown(KeyCode.J) && PlayManager.instance.currentCode == '¡ª')
        {
            PlayManager.instance.isInput = true;
            float inputTime = PlayManager.instance.offsetTime;
            float verdict = inputTime >= (60 / bpm / 2) ? (60 / bpm) - inputTime : inputTime;
            if (verdict <= (60 / bpm / 7))
            {
                GetComponent<TMP_Text>().text = "<color=green>perfect</color>";
                GameManager.instance.perfectCount++;
            }
            else if (verdict <= (60 / bpm / 4))
            {
                GetComponent<TMP_Text>().text = "<color=#64FF00>greate</color>";
                GameManager.instance.greateCount++;
            }
            else if (verdict <= (60 / bpm / 2.5))
            {
                GetComponent<TMP_Text>().text = "<color=#FF6400>good</color>";
                GameManager.instance.goodCount++;
            }
        }

        if (PlayManager.instance.isFail)
        {
            GetComponent<TMP_Text>().text = "<color=red>fail</color>";
            GameManager.instance.failCount++;
            PlayManager.instance.isFail = false;
        }
    }
}
