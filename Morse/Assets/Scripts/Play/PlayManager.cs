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
    public char currentCode;

    private void Start()
    {
        GameManager.instance.isPlayMusic = true;
        MusicInfo musicInfo = Singleton<MusicInfo>.instance;

        bpm = musicInfo.musicList.datas[musicInfo.currentMusicIndex].bpm;
        mapData = musicInfo.musicList.datas[musicInfo.currentMusicIndex].mapData;

        originalTime = 0;
        offsetTime = 0;
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
        yield return new WaitForSeconds(GameManager.instance.offset);
        isWaitOffset = true;
    }
}
