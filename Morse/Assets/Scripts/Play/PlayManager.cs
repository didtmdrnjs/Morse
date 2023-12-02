using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : Singleton<PlayManager>
{
    public float bpm;
    public string mapData;
    public float time;

    public bool isFadeOut;
    public bool isCountdown;

    public char[] morseCode;
    public int morseIdx;

    private void Start()
    {
        MusicInfo musicInfo = Singleton<MusicInfo>.instance;
        
        bpm = musicInfo.musicList.datas[musicInfo.currentMusicIndex].bpm;
        mapData = musicInfo.musicList.datas[musicInfo.currentMusicIndex].mapData;

        time = 0;
    }

    private void Update()
    {
        if (isCountdown) time += Time.deltaTime;
    }
}
