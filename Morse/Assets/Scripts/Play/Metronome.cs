using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TerrainTools;

public class Metronome : Singleton<Metronome>
{
    private float bpm;
    private string map;

    private void Start()
    {
        bpm = Singleton<PlayManager>.instance.bpm;
        map = Singleton<PlayManager>.instance.mapData;
    }

    private void Update()
    {
        if (Singleton<CreateWord>.instance.isMusicEnd)
        {
            Singleton<PlayManager>.instance.isCountdown = false;
            StartCoroutine(Finish());
        }

        if (Singleton<CreateWord>.instance.isChangeAlphabet && Singleton<PlayManager>.instance.isCountdown)
        {
            CountMorse();
        }
    }

    private void CountMorse()
    {
        if (Singleton<PlayManager>.instance.morseIdx == Singleton<PlayManager>.instance.morseCode.Length)
        {
            StartCoroutine(Later());
            Singleton<PlayManager>.instance.morseIdx = 0;
        }

        if (Singleton<PlayManager>.instance.time >= 60 / bpm)
        {
            Singleton<PlayManager>.instance.time -= 60 / bpm;

            Singleton<PlayManager>.instance.morseIdx++;
        }
    }

    IEnumerator Later()
    {
        yield return new WaitForSeconds(60 / bpm / 2);

        Singleton<CreateWord>.instance.isMorseEnd = true;
        Singleton<CreateWord>.instance.isChangeAlphabet = false;
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(1.5f);

        Singleton<MusicInfo>.instance.isLoadScene = true;
        SceneManager.LoadScene("SelectMusic");
    }
}
