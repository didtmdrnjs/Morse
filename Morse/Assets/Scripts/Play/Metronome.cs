using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TerrainTools;

public class Metronome : MonoBehaviour
{
    [SerializeField] private GameObject spin;

    [SerializeField] private AudioClip Short;
    [SerializeField] private AudioClip Long;

    private AudioSource audioSource;

    private float bpm;
    private string map;

    private float originalTime;

    private int originalWordIndex;
    public char[] originalMorseCode;
    public int originalMorseIdx;

    private void Start()
    {
        bpm = PlayManager.instance.bpm;
        map = PlayManager.instance.map;
        originalWordIndex = 0;
        originalMorseIdx = 0;
        originalMorseCode = new char[] { ' ' };
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (PlayManager.instance.isCountdown) originalTime += Time.deltaTime;

        if (CreateWord.instance.isWordEnd && ShowEnigma.instance.isEnigmaEnd) StartCoroutine(Finish());
        else if (PlayManager.instance.isCountdown) Play();
    }

    private void Play()
    {
        SoundPlay();

        if (PlayManager.instance.offsetTime >= 60 / bpm)
        {
            PlayManager.instance.offsetTime -= 60 / bpm;
            spin.transform.Rotate(0, 0, -22.5f);
            StartCoroutine(LaterChangeIdx());
        }
    }

    private void SoundPlay()
    {
        if (originalWordIndex < map.Length && originalTime >= 60 / bpm)
        {
            originalTime -= 60 / bpm;

            if (PlayManager.instance.morse[map[originalWordIndex]][originalMorseIdx] == '¡¤')
            {
                audioSource.clip = Short;
                audioSource.pitch = 0.8f;
                audioSource.Play();
            }
            else if (PlayManager.instance.morse[map[originalWordIndex]][originalMorseIdx] == '¡ª')
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
                if (originalWordIndex < map.Length) originalMorseCode = PlayManager.instance.morse[map[originalWordIndex]];
            }
        }
    }

    IEnumerator LaterChangeIdx()
    {
        yield return new WaitForSeconds(60 / bpm / 2);
        PlayManager.instance.ChangeIdx();
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(1.5f);
        StopCoroutine("RotateMetronome");
        MusicInfo.instance.isLoadScene = true;
        GameManager.instance.isPlayMusic = false;
        SceneManager.LoadScene("Result");
    }
}
