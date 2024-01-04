using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CMetronome : Singleton<CMetronome>
{
    public AudioSource source;
    public bool isEndGuide;
    
    private int noteCount;

    private float bpm;
    private float time;
    private float startTime;

    public int inputCount;
    public float sumTime;
    
    private void Start()
    {
        bpm = 120;
        noteCount = 50;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isEndGuide)
        {
            time += Time.deltaTime;

            metronome();
            InputKey();
        }
    }

    private void metronome()
    {
        if (time >= 60 / bpm)
        {
            time -= 60 / bpm;
            source.pitch = 0.8f;
            source.Play();
            startTime = time;
            if (--noteCount == 0)
            {
                SceneManager.LoadScene(Singleton<GameManager>.instance.lastSceneName);
                Singleton<GameManager>.instance.isPlayMusic = false;
                if (Singleton<GameManager>.instance.lastSceneName == "SelectMusic") Singleton<MusicInfo>.instance.isLoadScene = true;
            }
        }
    }

    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float subTime = time - startTime;
            inputCount++;
            sumTime += subTime;
        }
    }
}
