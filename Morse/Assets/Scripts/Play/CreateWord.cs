using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateWord : MonoBehaviour
{
    public static CreateWord instance;

    public TMP_Text FirstWord;
    public TMP_Text SecondWord;
    public TMP_Text ThirdWord;
    public TMP_Text FourthWord;
    public TMP_Text FifthWord;
    public TMP_Text Code;

    public bool isMorseEnd;
    public bool isWordEnd;
    public bool isEasy;

    public int offsetWordIndex;

    private string map;
    private string morseCode;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        map = PlayManager.instance.map;
        isMorseEnd = true;
        isEasy = GameManager.instance.difficulty == 0;
        if ((int)GameManager.instance.mode == 1)
        {
            GetComponent<RectTransform>().anchorMin = new Vector2(0.1f, 0);
            GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
        }
        
        PlayManager.instance.onOneEnd += () =>
        {
            if (offsetWordIndex < map.Length) PlayManager.instance.offsetMorseCode = PlayManager.instance.morse[map[offsetWordIndex]];
            ShowAlphabet();
        };

        PlayManager.instance.onOneFail += () =>
        {
            if (GameManager.instance.difficulty == 0)
            {
                string[] codes = Code.text.Split(' ');
                codes[PlayManager.instance.offsetMorseIdx] = "<color=#FF0000>" + PlayManager.instance.currentCode + "</color>";
                string morseCode = codes[0];
                for (int i = 1; i < codes.Length; i++)
                {
                    morseCode += " " + codes[i];
                }
                Code.text = morseCode;
            }
        };
    }

    private void ShowAlphabet()
    {
        if (isEasy && offsetWordIndex < map.Length)
        {
            morseCode = "";
            foreach (char c in PlayManager.instance.morse[map[offsetWordIndex]])
            {
                morseCode += "<color=#FFFFFF>";
                morseCode += c;
                morseCode += "</color> ";
            }
            Code.text = morseCode;
        }

        SetEnigmaWord(offsetWordIndex);
    }

    private void SetEnigmaWord(int idx)
    {
        if (idx < map.Length) FirstWord.text = map[idx].ToString();
        else
        {
            FirstWord.text = " ";
            isWordEnd = true;
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
