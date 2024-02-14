using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Enigma : MonoBehaviour
{
    private Dictionary<char, int> OriginalPos = new() {
        {'A', 0}, {'B', 1}, {'C', 2}, {'D', 3}, {'E', 4}, {'F', 5}, {'G', 6}, {'H', 7}, {'I', 8}, {'J', 9},
        {'K', 10}, {'L', 11}, {'M', 12}, {'N', 13}, {'O', 14}, {'P', 15}, {'Q', 16}, {'R', 17}, {'S', 18}, {'T', 19},
        {'U', 20}, {'V', 21}, {'W', 22}, {'X', 23}, {'Y', 24}, {'Z', 25}, 
        {'ア', 0}, {'イ', 1}, {'ウ', 2}, {'エ', 3}, {'オ', 4}, {'カ', 5}, {'キ', 6}, {'ク', 7}, {'ケ', 8}, {'コ', 9},
        {'サ', 10}, {'シ', 11}, {'ス', 12}, {'セ', 13}, {'ソ', 14}, {'タ', 15}, {'チ', 16}, {'ツ', 17}, {'テ', 18}, {'ト', 19},
        {'ナ', 20}, {'ニ', 21}, {'ヌ', 22}, {'ネ', 23}, {'ノ', 24}, {'ハ', 25}, {'ヒ', 26}, {'フ', 27}, {'ヘ', 28}, {'ホ', 29},
        {'マ', 30}, {'ミ', 31}, {'ム', 32}, {'メ', 33}, {'モ', 34}, {'ヤ', 35}, {'ユ', 36}, {'ヨ', 37}, {'ラ', 38}, {'リ', 39},
        {'ル', 40}, {'レ', 41}, {'ロ', 42}, {'ワ', 43}, {'ヰ', 44}, {'ヱ', 45}, {'ヲ', 46}, {'ン', 47}, {'゛', 48}, {'゜', 49},
        {'ㄱ', 0}, {'ㄴ', 1}, {'ㄷ', 2}, {'ㄹ', 3}, {'ㅁ', 4}, {'ㅂ', 5}, {'ㅅ', 6}, {'ㅇ', 7}, {'ㅈ', 8}, {'ㅊ', 9},
        {'ㅋ', 10}, {'ㅌ', 11}, {'ㅍ', 12}, {'ㅎ', 13}, {'ㅏ', 14}, {'ㅑ', 15}, {'ㅓ', 16}, {'ㅕ', 17}, {'ㅗ', 18}, {'ㅛ', 19},
        {'ㅜ', 20}, {'ㅠ', 21}, {'ㅡ', 22}, {'ㅣ', 23}, {'ㅐ', 24}, {'ㅔ', 25}
    };

    private string[] EnglishRotors =
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

    private string[] JapaneseRotors =
    {
        "ンタマムイヲオフニチワヰシスケアエミヱヤヌコハリヒモウソノセイ゜トラヨネ゛ツロホヘメクナレキカルテサ",
        "ムノンセネエラハヌロスツフオヲニレチワモマアホミテヰソサイトヱクヘシ゛ナルリケヒキ゜ヤタイヨカウメコ",
        "ヒムキヘミ゛イイハサヤノンヰヨソウルテスフナカ゜コアメマニエヱワロトツタホシチモレクヲオネケリヌラセ",
        "ムイ゛ヤハメホネケミトヱンモスシノサヨレライテクル゜コチセヘツソマタキニヲフウリヰナヒロカアオエヌワ",
        "ツスヌシワケウヒイリキ゜チタヰイヨルモコクマサアハヤセテンオヱカメトニノラレナ゛ネロソムミホフエヘヲ",
        "カメヨネキコイワエウセヒヰムトロスクオフイケモチヤヲニヘホタアヱリラシハミツナヌサノルテ゜レマソ゛ン",
        "ヨムホソタメキ゜サヰラミネリイヱケウヘアルフヌトシロワテノカオマイモセンエスヲハチ゛ヒナニレコツヤク",
        "ニネヲラルモン゛ワカコクハサレトテミエヤスヨチマフリヒソキヱオアノヌイ゜ウホヘツセシイヰケメタロナム"
    };

    private string[] KoreanRotors =
    {
        "ㅎㅡㅏㅇㅐㄱㅛㅍㅂㄴㅊㅕㄹㅌㅁㅠㅈㅅㄷㅋㅓㅜㅔㅣㅗㅑ",
        "ㅓㅅㅁㅇㅠㅈㅛㅕㅎㅑㅐㄱㅡㅍㅌㅔㅏㅣㅗㄴㄷㅂㅜㄹㅋㅊ",
        "ㅓㄷㅏㅈㅅㅣㄹㅋㅕㄱㅍㅔㅑㅂㅌㅐㅡㅗㅠㅜㅇㅎㅊㄴㅛㅁ",
        "ㅡㅣㅌㄱㅓㅍㅎㅅㅋㅗㅁㅛㅏㅈㅕㄷㅊㅜㄹㅐㅂㅑㅇㄴㅠㅔ",
        "ㅅㅍㄴㅜㅓㅋㅛㅐㅑㅗㅁㅠㅕㄹㅌㅂㅇㅔㅣㅈㅡㅎㅊㅏㄱㄷ",
        "ㅑㅡㅂㅍㅣㅏㅋㅇㅔㅗㅎㅜㅅㄱㅠㅈㄹㅊㅁㄴㅕㅓㅛㅐㅌㄷ",
        "ㅍㅇㅎㅓㄹㅗㅐㅅㅠㅁㄱㅕㄴㅈㅛㅑㅏㅣㅡㅋㄷㅜㅊㅌㅔㅂ",
        "ㅜㅁㅛㅏㅕㅋㅎㅗㄱㅅㄹㅑㅂㅐㅊㅠㅌㅔㅇㄴㅓㅍㅣㅈㄷㅡ"
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

        word = GetRotor()[rotor3][OriginalPos[word]];
        RotateRotor(rotor3);
        word = GetRotor()[rotor2][OriginalPos[word]];
        RotateRotor(rotor2);
        word = GetRotor()[rotor1][OriginalPos[word]];
        RotateRotor(rotor1);
        return word;
    }

    private void RotateRotor(int rotorIdx)
    {
        GetRotor()[rotorIdx] += GetRotor()[rotorIdx][0];
        GetRotor()[rotorIdx] = GetRotor()[rotorIdx].Substring(1);
    }

    private string[] GetRotor()
    {
        string language = MusicInfo.instance.datas[MusicInfo.instance.currentMusicIndex].language;

        if (language == "Korean") return KoreanRotors;
        else if (language == "Japanese") return JapaneseRotors;
        else return EnglishRotors;
    }
}
