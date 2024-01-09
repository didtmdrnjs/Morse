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
    private int failCount;

    private float bpm;
    private float time;

    private float[] startTimes;

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
        bpm = 80;
        noteMax = 20;
        noteCount = 0;
        inputCount = 0;
        isFirstClick = false;
        startTimes = new float[21];
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

            if (isFirstClick)
            {
                if (inputCount < noteCount)
                {
                    inputCount++;
                    failCount++;
                }
                else failCount = 0;
                
                startTimes[noteCount++] = time;
            }
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

            if (inputCount < noteCount) {
                float subTime = time - startTimes[inputCount++];
                sumTime += subTime;
            }
            else failCount++;

            if (failCount == 4) isAgain = true;
        }
    }

    private IEnumerator CorrectionEnd()
    {
        GameManager.instance.offset = sumTime / 20;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(GameManager.instance.lastSceneName);
        GameManager.instance.isPlayMusic = false;
        if (GameManager.instance.lastSceneName == "SelectMusic") Singleton<MusicInfo>.instance.isLoadScene = true;
    }
}
