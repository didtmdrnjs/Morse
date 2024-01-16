using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.TerrainTools;
using UnityEngine;

public class PushKey : MonoBehaviour
{
    private float bpm;

    private void Start()
    {
        bpm = Singleton<PlayManager>.instance.bpm;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Singleton<PlayManager>.instance.currentCode == '¡¤')
        {
            Singleton<PlayManager>.instance.isInput = true;
            float inputTime = Singleton<PlayManager>.instance.offsetTime;
            float verdict = inputTime >= (60 / bpm / 2) ? (60 / bpm) - inputTime : inputTime;
            if (verdict <= (60 / bpm / 7)) GetComponent<TMP_Text>().text = "<color=green>perfect</color>";
            else if (verdict <= (60 / bpm / 4)) GetComponent<TMP_Text>().text = "<color=#64FF00>greate</color>";
            else if (verdict <= (60 / bpm / 2.5)) GetComponent<TMP_Text>().text = "<color=#FF6400>good</color>";
        }

        if (Input.GetKeyDown(KeyCode.J) && Singleton<PlayManager>.instance.currentCode == '¡ª')
        {
            Singleton<PlayManager>.instance.isInput = true;
            float inputTime = Singleton<PlayManager>.instance.offsetTime;
            float verdict = inputTime >= (60 / bpm / 2) ? (60 / bpm) - inputTime : inputTime;
            if (verdict <= (60 / bpm / 7)) GetComponent<TMP_Text>().text = "<color=green>perfect</color>";
            else if (verdict <= (60 / bpm / 4)) GetComponent<TMP_Text>().text = "<color=#64FF00>greate</color>";
            else if (verdict <= (60 / bpm / 2.5)) GetComponent<TMP_Text>().text = "<color=#FF6400>good</color>";
        }

        if (Singleton<PlayManager>.instance.isFail)
        {
            GetComponent<TMP_Text>().text = "<color=red>fail</color>";
            Singleton<PlayManager>.instance.isFail = false;
        }
    }
}
