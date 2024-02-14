using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;

    public Dictionary<char, char[]> morse;

    private const char s = '·';
    private const char l = '―';

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
            {'0', new char[] { l, l, l, l, l } }, {'1', new char[] { s, l, l, l, l } }, {'2', new char[] { s, s, l, l, l } }, {'3', new char[] { s, s, s, l, l } }, {'4', new char[] { s, s, s, s, l } },
            {'5', new char[] { s, s, s, s, s } }, {'6', new char[] { l, s, s, s, s } }, {'7', new char[] { l, l, s, s, s } }, {'8', new char[] { l, l, l, s, s } }, {'9', new char[] { l, l, l, l, s } },

            {'.', new char[] { s, l, s, l, s, l } }, {',', new char[] { l, l, s, s, l, l } }, {'?', new char[] { s, s, l, l, s, s } }, {'/', new char[] { l, s, s, l, s } }, {'-', new char[] { l, s, s, s, s, l } },
            {'=', new char[] { l, s, s, s, l } }, {':', new char[] { l, l, l, s, s, s } }, {';', new char[] { l, s, l, s, l, s } }, {'(', new char[] { l, s, l, l, s } }, {')', new char[] { l, s, l, l, s, l } },
            {'\'', new char[] { s, l, l, l, l, s } }, {'\"', new char[] { s, l, s, s, l, s } }, {'!', new char[] { s, l, s, s, s} }, {'@', new char[] { s, l, l, s, l, s } }, {'+', new char[] { s, l, s, l, s } },
            {'×', new char[] { s, s, l, s, l } }, {'_', new char[] { s, s, l, l, s, l } }, {'~', new char[] { s, l, s, l } }, 

            {'A', new char[] { s, l } }, {'B', new char[] { l, s, s, s } }, {'C', new char[] { l, s, l, s } },　{'D', new char[] { l, s, s } }, {'E', new char[] { s } }, 
            {'F', new char[] { s, s, l, s } },　{'G', new char[] { l, l, s } }, {'H', new char[] { s, s, s, s } }, {'I', new char[] { s, s } },　{'J', new char[] { s, l, l, l } },
            {'K', new char[] { l, s, l } }, {'L', new char[] { s, l, s, s } },　{'M', new char[] { l, l } }, {'N', new char[] { l, s } }, {'O', new char[] { l, l, l } },
            {'P', new char[] { s, l, l, s } }, {'Q', new char[] { l, l, s, l } }, {'R', new char[] { s, l, s } },　{'S', new char[] { s, s, s } }, {'T', new char[] { l } }, 
            {'U', new char[] { s, s, l } },　{'V', new char[] { s, s, s, l } }, {'W', new char[] { s, l, l } }, {'X', new char[] { l, s, s, l } },　{'Y', new char[] { l, s, l, l } }, 
            {'Z', new char[] { l, l, s, s } },

            {'ア', new char[] { l, l, s, l, l } }, {'イ', new char[] { s, l } }, {'ウ', new char[] { s, s, l } }, {'エ', new char[] { l, s, l, l, l } }, {'オ', new char[] { s, l, s, s, s } },
            {'カ', new char[] { s, l, s, s } }, {'キ', new char[] { l, s, l, s, s } }, {'ク', new char[] { s, s, s, l } }, {'ケ', new char[] { l, s, l, l } }, {'コ', new char[] { l, l, l, l } },
            {'サ', new char[] { l, s, l, s, l } }, {'シ', new char[] { l, l, s, l, s } }, {'ス', new char[] { l, l, l, s, l } }, {'セ', new char[] { s, l, l, l, s } }, {'ソ', new char[] { l, l, l, s } },
            {'タ', new char[] { l, s } }, {'チ', new char[] { s, s, l, s } }, {'ツ', new char[] { s, l, l, s } }, {'テ', new char[] { s, l, s, l, l } }, {'ト', new char[] { s, s, l, s, s } },
            {'ナ', new char[] { s, l, s } }, {'ニ', new char[] { l, s, l, s } }, {'ヌ', new char[] { s, s, s, s } }, {'ネ', new char[] { l, l, s, l } }, {'ノ', new char[] { s, s, l, l } },
            {'ハ', new char[] { l, s, s, s } }, {'ヒ', new char[] { l, l, s, s, l } }, {'フ', new char[] { l, l, s, s } }, {'ヘ', new char[] { s } }, {'ホ', new char[] { l, s, s } },
            {'マ', new char[] { l, s, s, l } }, {'ミ', new char[] { s, s, l, s, l } }, {'ム', new char[] { l } }, {'メ', new char[] { l, s, s, s, l } }, {'モ', new char[] { l, s, s, l, s } },
            {'ヤ', new char[] { s, l, l } }, {'ユ', new char[] { l, s, s, l, l } }, {'ヨ', new char[] { l, l } }, {'ラ', new char[] { s, s, s } }, {'リ', new char[] { l, l, s } },
            {'ル', new char[] { l, s, l, l, s } }, {'レ', new char[] { l, l, l } }, {'ロ', new char[] { s, l, s, l } }, {'ワ', new char[] { l, s, l } }, {'ヰ', new char[] { s, l, s, s, l } },
            {'ヱ', new char[] { s, l, l, s, s } }, {'ヲ', new char[] { s, l, l, l } }, {'ン', new char[] { s, l, s, l, s } }, {'゛', new char[] { s, s } }, {'゜', new char[] { s, s, l, l, s } },

            {'ㄱ', new char[] { s, l, s, s } }, {'ㄴ', new char[] { s, s, l, s } }, {'ㄷ', new char[] { l, s, s, s } }, {'ㄹ', new char[] { s, s, s, l } }, 
            {'ㅁ', new char[] { l, l } }, {'ㅂ', new char[] { s, l, l } }, {'ㅅ', new char[] { l, l, s } }, {'ㅇ', new char[] { l, s, l } },
            {'ㅈ', new char[] { s, l, l, s } }, {'ㅊ', new char[] { l, s, l, s } }, {'ㅋ', new char[] { l, s, s, l } }, {'ㅌ', new char[] { l, l, s, s } },
            {'ㅍ', new char[] { l, l, l } }, {'ㅎ', new char[] { s, l, l, l } }, {'ㅏ', new char[] { s } }, {'ㅑ', new char[] { s, s } },
            {'ㅓ', new char[] { l } }, {'ㅕ', new char[] { s, s, s } }, {'ㅗ', new char[] { s, l } }, {'ㅛ', new char[] { l, s } },
            {'ㅜ', new char[] { s, s, s, s } }, {'ㅠ', new char[] { s, l, s } }, {'ㅡ', new char[] { l, s, s } }, {'ㅣ', new char[] { s, s, l } },
            {'ㅐ', new char[] { l, l, s, l } }, {'ㅔ', new char[] { l, s, l, l } },
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