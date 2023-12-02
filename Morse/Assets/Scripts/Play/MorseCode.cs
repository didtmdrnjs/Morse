using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseCode : Singleton<MorseCode>
{
    public Dictionary<char, char[]> morse;

    private const char s = '¡¤';
    private const char l = '¡ª';

    private void Start()
    {
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
}