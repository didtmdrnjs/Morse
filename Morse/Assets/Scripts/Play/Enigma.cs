using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Enigma : MonoBehaviour
{
    private string[] rotors =
    {
        "EKMFLGDQVZNTOWYHXUSPAIBRCJ",
        "AJDKSIRUXBLHWTMCQGZNPYFVOE",
        "BDFHJLCPRTXVZNYEIWGAKMUSQO",
        "ESOVPZJAYQUIRHXLNFTGKDCMWB",
        "VZBRGITYUPSDNHLXAWMJQOFECK",
        "JPGVOUMFYQBENHZRDKASXLICTW",
        "NZJHGRCXMYSWBOUFAIVLPEKQDT",
        "FKQHTLXOCBJSPDZRAMEWNIUYGV"
    };

    private int rotor1;
    private int rotor2;
    private int rotor3;

    private string enigma;

    private void Start()
    {
        GetEnigma(PlayManager.instance.map);
        PlayManager.instance.enigma = enigma;
    }

    private void GetEnigma(string map)
    {
        int s = Random.Range(0, 7);
        rotor1 = s;
        int d = Random.Range(1, 7);
        rotor2 = (s + d) % 8;
        d = Random.Range(1, 7);
        rotor3 = (s + d * 2) % 8;
        enigma = "";
        foreach (var word in map)
        {
            enigma += ChaingeEnigma(word);
        }
    }

    private char ChaingeEnigma(char word)
    {
        if (word == ' ') return ' ';

        word = rotors[rotor3][word - 65];
        RotateRotor(rotor3);
        word = rotors[rotor2][word - 65];
        RotateRotor(rotor2);
        word = rotors[rotor1][word - 65];
        RotateRotor(rotor1);
        return word;
    }

    private void RotateRotor(int rotorIdx)
    {
        rotors[rotorIdx] += rotors[rotorIdx][0];
        rotors[rotorIdx] = rotors[rotorIdx].Substring(1);
    }
}
