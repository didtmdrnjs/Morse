using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CMetronome : MonoBehaviour
{
    public static CMetronome instance;

    public AudioSource source;
    public bool isEndGuide;
    public bool isAgain;

    private bool isFirstClick;
    private bool isEnd;
    
    private int noteMax;
    private int noteCount;
    private int inputCount;
    private int failCount;

    private float bpm;
    private float time;

    private float[] startTimes;

    public float sumTime;

    public Action onCorrectionEnd;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
        Reset();
    }

    private void Update()
    {
        if (isEndGuide && !isAgain && !isEnd)
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
        User.instance.WriteUserData();
        isEnd = true;
        yield return new WaitForSeconds(2);
        onCorrectionEnd?.Invoke();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(GameManager.instance.lastSceneName);
        GameManager.instance.isPlayMusic = false;
        if (GameManager.instance.lastSceneName == "SelectMusic") MusicInfo.instance.isLoadScene = true;
    }
}
