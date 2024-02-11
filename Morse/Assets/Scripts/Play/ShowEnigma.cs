using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowEnigma : MonoBehaviour
{
    public static ShowEnigma instance;

    public TMP_Text FirstWord;
    public TMP_Text SecondWord;
    public TMP_Text ThirdWord;
    public TMP_Text FourthWord;
    public TMP_Text FifthWord;
    public TMP_Text Code;

    public int offsetEnigmaIdx;

    public string map;
    public string morseCode;

    public bool isEasy;
    public bool isEnigmaEnd;

    public Action<string> AfterSetEnigma;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        offsetEnigmaIdx = 0;
    }

    private void Start()
    {
        if (GameManager.instance.mode == 0)
        {
            isEnigmaEnd = true;
            gameObject.SetActive(false);
        }
        isEasy = GameManager.instance.difficulty == 0;

        map = PlayManager.instance.enigma;

        PlayManager.instance.onTwoEnd += () =>
        {
            if (offsetEnigmaIdx < map.Length) PlayManager.instance.offsetEnigmaCode = PlayManager.instance.morse[map[offsetEnigmaIdx]];
            Show();
        };

        PlayManager.instance.onTwoFail += () =>
        {
            if (GameManager.instance.difficulty == 0)
            {
                string[] codes = Code.text.Split(' ');
                codes[PlayManager.instance.offsetEnigmaIdx] = "<color=#FF0000>" + PlayManager.instance.curEnigmaCode + "</color>";
                string morseCode = codes[0];
                for (int i = 1; i < codes.Length; i++)
                {
                    morseCode += " " + codes[i];
                }
                Code.text = morseCode;
            }
        };
    }

    private void Show()
    {
        if (isEasy && offsetEnigmaIdx < map.Length)
        {
            morseCode = "";
            foreach (char c in PlayManager.instance.morse[map[offsetEnigmaIdx]])
            {
                morseCode += "<color=#FFFFFF>";
                morseCode += c;
                morseCode += "</color> ";
            }
            Code.text = morseCode;
        }

        SetEnigmaWord(offsetEnigmaIdx);
    }

    private void SetEnigmaWord(int idx)
    {
        if (idx < map.Length) FirstWord.text = map[idx].ToString();
        else
        {
            FirstWord.text = " ";
            isEnigmaEnd = true;
        }

        if (idx + 1 < map.Length) SecondWord.text = map[idx + 1].ToString();
        else SecondWord.text = " ";

        if (idx + 2 < map.Length) ThirdWord.text = map[idx + 2].ToString();
        else ThirdWord.text = " ";

        if (idx + 3 < map.Length) FourthWord.text = map[idx + 3].ToString();
        else FourthWord.text = " ";

        if (idx + 4 < map.Length) FifthWord.text = map[idx + 4].ToString();
        else FifthWord.text = " ";
    }
}
