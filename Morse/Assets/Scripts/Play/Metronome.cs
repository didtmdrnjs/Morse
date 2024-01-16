using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TerrainTools;

public class Metronome : MonoBehaviour
{
    [SerializeField] private AudioClip Short;
    [SerializeField] private AudioClip Long;

    private AudioSource audioSource;

    private float bpm;
    private string map;

    private int originalWordIndex;
    public char[] originalMorseCode;
    public int originalMorseIdx;

    private void Start()
    {
        bpm = Singleton<PlayManager>.instance.bpm;
        map = Singleton<PlayManager>.instance.mapData;
        originalWordIndex = 0;
        originalMorseIdx = 0;
        originalMorseCode = new char[] { ' ' };
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Singleton<CreateWord>.instance.isMusicEnd)
        {
            Singleton<CreateWord>.instance.isMusicEnd = false;
            StartCoroutine(Finish());
        }

        if (Singleton<CreateWord>.instance.isChangeAlphabet && Singleton<PlayManager>.instance.isCountdown) CountMorse();
    }

    private void CountMorse()
    {
        if (originalWordIndex < map.Length && Singleton<PlayManager>.instance.originalTime >= 60 / bpm)
        {
            Singleton<PlayManager>.instance.originalTime -= 60 / bpm;

            if (Singleton<MorseCode>.instance.morse[map[originalWordIndex]][originalMorseIdx] == '¡¤')
            {
                audioSource.clip = Short;
                audioSource.pitch = 0.8f;
                audioSource.Play();
            }
            else if (Singleton<MorseCode>.instance.morse[map[originalWordIndex]][originalMorseIdx] == '¡ª')
            {
                audioSource.clip = Long;
                audioSource.pitch = 0.8f;
                audioSource.Play();
            }

            originalMorseIdx++;

            if (originalMorseIdx == originalMorseCode.Length)
            {
                originalMorseIdx = 0;
                originalWordIndex++;
                if (originalWordIndex < map.Length) originalMorseCode = Singleton<MorseCode>.instance.morse[map[originalWordIndex]];
            }
        }

        if (Singleton<PlayManager>.instance.offsetTime >= 60 / bpm)
        {
            Singleton<PlayManager>.instance.offsetTime -= 60 / bpm;
            StartCoroutine(LaterChangeIdx());
        }
    }

    IEnumerator LaterChangeIdx()
    {
        yield return new WaitForSeconds(60 / bpm / 2);
        if (Singleton<PlayManager>.instance.currentCode != ' ' && !Singleton<PlayManager>.instance.isInput) Singleton<PlayManager>.instance.isFail = true;
        Singleton<PlayManager>.instance.offsetMorseIdx++;
        Singleton<PlayManager>.instance.isInput = false;
        if (Singleton<PlayManager>.instance.offsetMorseIdx >= Singleton<PlayManager>.instance.offsetMorseCode.Length)
        {
            Singleton<CreateWord>.instance.offsetWordIndex++;
            Singleton<CreateWord>.instance.isMorseEnd = true;
            Singleton<CreateWord>.instance.isChangeAlphabet = false;
            Singleton<PlayManager>.instance.offsetMorseIdx = 0;
        }
        Singleton<PlayManager>.instance.currentCode = Singleton<PlayManager>.instance.offsetMorseCode[Singleton<PlayManager>.instance.offsetMorseIdx];
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(1.5f);
        Singleton<PlayManager>.instance.isCountdown = false;
        Singleton<MusicInfo>.instance.isLoadScene = true;
        GameManager.instance.isPlayMusic = false;
        SceneManager.LoadScene("SelectMusic");
    }
}
