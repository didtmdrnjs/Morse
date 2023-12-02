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
    public int currentWordIndex;

    private string map;
    private string morseCode;

    private void Start()
    {
        currentWordIndex = -1;
        map = Singleton<PlayManager>.instance.mapData;

        isMorseEnd = true;
    }

    private void Update()
    {
        if (Singleton<PlayManager>.instance.isCountdown && isMorseEnd)
        {
            isMorseEnd = false;
            currentWordIndex++;
            if (currentWordIndex < map.Length) Singleton<PlayManager>.instance.morseCode = Singleton<MorseCode>.instance.morse[map[currentWordIndex]];
            ShowAlphabet();
        }
    }

    private void ShowAlphabet()
    {
        isChangeAlphabet = true;

        if (currentWordIndex < map.Length)
        {
            morseCode = "";
            foreach (char c in Singleton<MorseCode>.instance.morse[map[currentWordIndex]])
            {
                morseCode += c;
                morseCode += " ";
            }
            Code.text = morseCode;
        }

        if (currentWordIndex < map.Length)
        {
            FirstWord.text = map[currentWordIndex].ToString();
        }
        else
        {
            FirstWord.text = " ";
            isMusicEnd = true;
        }

        if (currentWordIndex + 1 < map.Length) SecondWord.text = map[currentWordIndex + 1].ToString();
        else SecondWord.text = " ";

        if (currentWordIndex + 2 < map.Length) ThirdWord.text = map[currentWordIndex + 2].ToString();
        else ThirdWord.text = " ";

        if (currentWordIndex + 3 < map.Length) FourthWord.text = map[currentWordIndex + 3].ToString();
        else FourthWord.text = " ";

        if (currentWordIndex + 4 < map.Length) FifthWord.text = map[currentWordIndex + 4].ToString();
        else FifthWord.text = " ";
    }
}
