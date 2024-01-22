using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateWord : MonoBehaviour
{
    public static CreateWord instance;

    [SerializeField] private TextMeshProUGUI FirstWord;
    [SerializeField] private TextMeshProUGUI SecondWord;
    [SerializeField] private TextMeshProUGUI ThirdWord;
    [SerializeField] private TextMeshProUGUI FourthWord;
    [SerializeField] private TextMeshProUGUI FifthWord;
    [SerializeField] private TextMeshProUGUI Code;

    public bool isMorseEnd;
    public bool isMusicEnd;

    public int offsetWordIndex;

    private string map;
    private string morseCode;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
    }

    private void Start()
    {
        map = PlayManager.instance.map;
        isMorseEnd = true;
    }

    private void Update()
    {
        if (PlayManager.instance.isCountdown && isMorseEnd)
        {
            isMorseEnd = false;
            if (offsetWordIndex < map.Length)
            {
                PlayManager.instance.offsetMorseCode = PlayManager.instance.morse[map[offsetWordIndex]];
            }   
            ShowAlphabet();
        }
    }

    private void ShowAlphabet()
    {
        if (offsetWordIndex < map.Length)
        {
            morseCode = "";
            foreach (char c in PlayManager.instance.morse[map[offsetWordIndex]])
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
