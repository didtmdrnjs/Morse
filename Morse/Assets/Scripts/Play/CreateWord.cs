using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateWord : Singleton<CreateWord>
{
    [SerializeField] private TextMeshProUGUI FirstWord;
    [SerializeField] private TextMeshProUGUI SecondWord;
    [SerializeField] private TextMeshProUGUI ThirdWord;
    [SerializeField] private TextMeshProUGUI FourthWord;
    [SerializeField] private TextMeshProUGUI FifthWord;
    [SerializeField] private TextMeshProUGUI Code;

    public bool isMorseEnd;
    public bool isMusicEnd;
    public bool isChangeAlphabet;

    public int offsetWordIndex;

    private string map;
    private string morseCode;

    private void Start()
    {
        map = Singleton<PlayManager>.instance.mapData;
        isMorseEnd = true;
        offsetWordIndex = 0;
    }

    private void Update()
    {
        if (Singleton<PlayManager>.instance.isCountdown && isMorseEnd)
        {
            Debug.Log("sdlfjskdf0");
            isMorseEnd = false;
            if (offsetWordIndex < map.Length)
            {
                Singleton<PlayManager>.instance.offsetMorseCode = Singleton<MorseCode>.instance.morse[map[offsetWordIndex]];
                Singleton<PlayManager>.instance.currentCode = Singleton<PlayManager>.instance.offsetMorseCode[Singleton<PlayManager>.instance.offsetMorseIdx];
            }   
            ShowAlphabet();
        }
    }

    private void ShowAlphabet()
    {
        isChangeAlphabet = true;

        if (offsetWordIndex < map.Length)
        {
            morseCode = "";
            foreach (char c in Singleton<MorseCode>.instance.morse[map[offsetWordIndex]])
            {
                morseCode += c;
                morseCode += " ";
            }
            Code.text = morseCode;
        }

        if (offsetWordIndex < map.Length)
        {
            FirstWord.text = map[offsetWordIndex].ToString();
        }
        else
        {
            FirstWord.text = " ";
            isMusicEnd = true;
        }

        if (offsetWordIndex + 1 < map.Length) SecondWord.text = map[offsetWordIndex + 1].ToString();
        else SecondWord.text = " ";

        if (offsetWordIndex + 2 < map.Length) ThirdWord.text = map[offsetWordIndex + 2].ToString();
        else ThirdWord.text = " ";

        if (offsetWordIndex + 3 < map.Length) FourthWord.text = map[offsetWordIndex + 3].ToString();
        else FourthWord.text = " ";

        if (offsetWordIndex + 4 < map.Length) FifthWord.text = map[offsetWordIndex + 4].ToString();
        else FifthWord.text = " ";
    }
}
