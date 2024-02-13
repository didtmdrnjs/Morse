using System;
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
    public string enigma;

    public bool isFadeOut;
    public bool isCountdown;
    public bool isStartOffset;
    public bool isWaitOffset;

    public char[] offsetMorseCode;
    public int offsetMorseIdx;
    public char currentCode;

    public char[] offsetEnigmaCode;
    public int offsetEnigmaIdx;
    public char curEnigmaCode;

    public float offsetTime;

    public bool isOneInput;
    public bool isTwoInput;
    public bool isFail;

    public Action onOneFail;
    public Action onTwoFail;

    public Action onOneEnd;
    public Action onTwoEnd;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        morse = new Dictionary<char, char[]>()
        {
            {'A', new char[] {s, l} }, {'B', new char[] {l, s, s, s} }, {'C', new char[] {l, s, l, s} },
            {'D', new char[] {l, s, s} }, {'E', new char[] {s} }, {'F', new char[] {s, s, l, s } },
            {'G', new char[] { l, l, s } }, {'H', new char[] { s, s, s, s } }, {'I', new char[] { s, s } },
            {'J', new char[] { s, l, l, l } }, {'K', new char[] { l, s, l } }, {'L', new char[] { s, l, s, s } },
            {'M', new char[] { l, l } }, {'N', new char[] { l, s } }, {'O', new char[] { l, l, l } },
            {'P', new char[] { s, l, l, s } }, {'Q', new char[] { l, l, s, l } }, {'R', new char[] { s, l, s } },
            {'S', new char[] { s, s, s } }, {'T', new char[] { l } }, {'U', new char[] { s, s, l } },
            {'V', new char[] { s, s, s, l } }, {'W', new char[] { s, l, l } }, {'X', new char[] { l, s, s, l } },
            {'Y', new char[] { l, s, l, l } }, {'Z', new char[] { l, l, s, s } },
            {' ', new char[] { ' ' } }
        };
    }

    private void Start()
    {
        GameManager.instance.isPlayMusic = true;
        MusicData data = MusicInfo.instance.datas[MusicInfo.instance.currentMusicIndex];

        bpm = data.bpm;
        map = data.mapData;

        currentCode = ' ';
        curEnigmaCode = ' ';

        GetComponent<AudioSource>().volume = User.instance.userSetting.volum / 10f;
    }

    private void Update()
    {
        if (isCountdown && !isStartOffset) StartCoroutine(WaitOffset());
        if (isWaitOffset) offsetTime += Time.deltaTime;
    }

    IEnumerator WaitOffset()
    {
        isStartOffset = true;
        yield return new WaitForSeconds(User.instance.userSetting.offset);
        isWaitOffset = true;
    }

    public void ChangeIdx()
    {
        OneModeChange();
        if (GameManager.instance.mode == EMode.TwoWord) TwoModeChange();
    }

    private void OneModeChange()
    {
        if (currentCode != ' ' && !isOneInput)
        {
            Score.instance.combo = 0;
            onOneFail?.Invoke();
        }
        offsetMorseIdx++;
        isOneInput = false;
        if (offsetMorseIdx >= offsetMorseCode.Length)
        {
            CreateWord.instance.offsetWordIndex++;
            onOneEnd?.Invoke();
            offsetMorseIdx = 0;
        }
        if (CreateWord.instance.offsetWordIndex >= map.Length) return;
        currentCode = morse[map[CreateWord.instance.offsetWordIndex]][offsetMorseIdx];
    }

    private void TwoModeChange()
    {
        if (curEnigmaCode != ' ' && !isTwoInput)
        {
            Score.instance.combo = 0;
            onTwoFail?.Invoke();
        }
        offsetEnigmaIdx++;
        isTwoInput = false;
        if (offsetEnigmaIdx >= offsetEnigmaCode.Length)
        {
            ShowEnigma.instance.offsetEnigmaIdx++;
            onTwoEnd?.Invoke();
            offsetEnigmaIdx = 0;
        }
        if (ShowEnigma.instance.offsetEnigmaIdx >= enigma.Length) return;
        curEnigmaCode = morse[enigma[ShowEnigma.instance.offsetEnigmaIdx]][offsetEnigmaIdx];
    }
}