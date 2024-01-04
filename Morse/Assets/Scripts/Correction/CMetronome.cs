using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CMetronome : Singleton<CMetronome>
{
    public AudioSource source;
    public bool isEndGuide;
    public bool isAgain;

    private bool isFirstClick;
    
    private int noteMax;
    private int noteCount;
    private int inputCount;

    private float bpm;
    private float time;

    private float[] startTimes = new float[31];

    public float sumTime;
    
    private void Start()
    {
        Reset();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isEndGuide && !isAgain)
        {
            time += Time.deltaTime;

            metronome();
            InputKey();
        }
    }

    private void Reset()
    {
        bpm = 120;
        noteMax = 30;
        noteCount = 0;
        inputCount = 0;
        isFirstClick = false;
        startTimes = new float[31];
    }

    private void metronome()
    {
        if (time >= 60 / bpm)
        {
            time -= 60 / bpm;
            source.pitch = 0.8f;
            source.Play();

            if (noteCount == noteMax) StartCoroutine(CorrectionEnd());
            else if (noteCount > noteMax) return;

            if (isFirstClick) startTimes[noteCount++] = time;
        }
    }

    private void InputKey()
    {
        if (inputCount > noteMax) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isFirstClick)
            {
                isFirstClick = true;
                return;
            }
            float subTime = time - startTimes[inputCount++];
            sumTime += subTime;

            if (Mathf.Abs(inputCount - noteCount) == 3) isAgain = true;
        }
    }

    private IEnumerator CorrectionEnd()
    {
        Singleton<GameManager>.instance.offset = sumTime / 30;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(Singleton<GameManager>.instance.lastSceneName);
        Singleton<GameManager>.instance.isPlayMusic = false;
        if (Singleton<GameManager>.instance.lastSceneName == "SelectMusic") Singleton<MusicInfo>.instance.isLoadScene = true;
    }
}
