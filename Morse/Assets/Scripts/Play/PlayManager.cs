using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : Singleton<PlayManager>
{
    public float bpm;
    public string mapData;
    public float originalTime;
    public float offsetTime;

    public bool isFadeOut;
    public bool isCountdown;
    public bool isStartOffset;
    public bool isWaitOffset;

    public char[] offsetMorseCode;
    public int offsetMorseIdx;

    public float offset;

    private void Start()
    {
        Singleton<GameManager>.instance.isPlayMusic = true;
        MusicInfo musicInfo = Singleton<MusicInfo>.instance;
        
        bpm = musicInfo.musicList.datas[musicInfo.currentMusicIndex].bpm;
        mapData = musicInfo.musicList.datas[musicInfo.currentMusicIndex].mapData;

        originalTime = 0;
        offsetTime = 0;

        offset = 0.35f;
    }

    private void Update()
    {
        if (isCountdown) originalTime += Time.deltaTime;
        if (isCountdown && !isStartOffset) StartCoroutine(WaitOffset());
        if (isWaitOffset) offsetTime += Time.deltaTime;
    }

    IEnumerator WaitOffset()
    {
        isStartOffset = true;
        yield return new WaitForSeconds(offset);
        isWaitOffset = true;
    }
}
