using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;

    public Dictionary<char, char[]> morse;

    private const char s = '¡¤';
    private const char l = '¡ª';

    public float bpm;
    public string map;

    public bool isFadeOut;
    public bool isCountdown;
    public bool isStartOffset;
    public bool isWaitOffset;

    public char[] offsetMorseCode;
    public int offsetMorseIdx;
    public char currentCode;

    public float offsetTime;

    public bool isInput;
    public bool isFail;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        morse = new Dictionary<char, char[]>()
        {
            {'A', new char[] {s, l} },
            {'B', new char[] {l, s, s, s} },
            {'C', new char[] {l, s, l, s} },
            {'D', new char[] {l, s, s} },
            {'E', new char[] {s} },
            {' ', new char[] { ' ' } }
        };
    }

    private void Start()
    {
        GameManager.instance.isPlayMusic = true;
        MusicData data = MusicInfo.instance.musicList.datas[MusicInfo.instance.currentMusicIndex];

        bpm = data.bpm;
        map = data.mapData;

        currentCode = ' ';
        GameManager.instance.perfectCount = 0;
        GameManager.instance.greateCount = 0;
        GameManager.instance.goodCount = 0;
        GameManager.instance.failCount = 0;
    }

    private void Update()
    {
        if (isCountdown && !isStartOffset) StartCoroutine(WaitOffset());
        if (isWaitOffset) offsetTime += Time.deltaTime;
    }

    IEnumerator WaitOffset()
    {
        isStartOffset = true;
        yield return new WaitForSeconds(GameManager.instance.offset);
        isWaitOffset = true;
    }

    public void ChangeIdx()
    {
        if (currentCode != ' ' && !isInput) isFail = true;
        offsetMorseIdx++;
        isInput = false;
        if (offsetMorseIdx >= offsetMorseCode.Length)
        {
            CreateWord.instance.offsetWordIndex++;
            CreateWord.instance.isMorseEnd = true;
            offsetMorseIdx = 0;
        }
        if (CreateWord.instance.offsetWordIndex == map.Length) return;
        currentCode = morse[map[CreateWord.instance.offsetWordIndex]][offsetMorseIdx];
    }
}
