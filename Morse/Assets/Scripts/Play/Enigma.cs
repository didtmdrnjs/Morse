using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Enigma : MonoBehaviour
{
    private Dictionary<char, int> OriginalPos = new() {
        {'0', 0 }, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4}, {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9},
        {'.', 10 }, {',', 11}, {'?', 12}, {'/', 13}, {'-', 14}, {'=', 15}, {':', 16}, {';', 17}, {'(', 18}, {')', 19},
        {'\'', 20 }, {'\"', 21}, {'!', 22}, {'@', 23}, {'+', 24}, {'×', 25}, {'_', 26}, {'~', 27}, 
        {'A', 28}, {'B', 29}, {'C', 30}, {'D', 31}, {'E', 32}, {'F', 33}, {'G', 34}, {'H', 35}, {'I', 36}, {'J', 37},
        {'K', 38}, {'L', 39}, {'M', 40}, {'N', 41}, {'O', 42}, {'P', 43}, {'Q', 44}, {'R', 45}, {'S', 46}, {'T', 47},
        {'U', 48}, {'V', 49}, {'W', 50}, {'X', 51}, {'Y', 52}, {'Z', 53}, 
        {'ア', 28}, {'イ', 29}, {'ウ', 30}, {'エ', 31}, {'オ', 32}, {'カ', 33}, {'キ', 34}, {'ク', 35}, {'ケ', 36}, {'コ', 37},
        {'サ', 38}, {'シ', 39}, {'ス', 40}, {'セ', 41}, {'ソ', 42}, {'タ', 43}, {'チ', 44}, {'ツ', 45}, {'テ', 46}, {'ト', 47},
        {'ナ', 48}, {'ニ', 49}, {'ヌ', 50}, {'ネ', 51}, {'ノ', 52}, {'ハ', 53}, {'ヒ', 54}, {'フ', 55}, {'ヘ', 56}, {'ホ', 57},
        {'マ', 58}, {'ミ', 59}, {'ム', 60}, {'メ', 61}, {'モ', 62}, {'ヤ', 63}, {'ユ', 64}, {'ヨ', 65}, {'ラ', 66}, {'リ', 67},
        {'ル', 68}, {'レ', 69}, {'ロ', 70}, {'ワ', 71}, {'ヰ', 72}, {'ヱ', 73}, {'ヲ', 74}, {'ン', 75}, {'゛', 76}, {'゜', 77},
        {'ㄱ', 28}, {'ㄴ', 29}, {'ㄷ', 30}, {'ㄹ', 31}, {'ㅁ', 32}, {'ㅂ', 33}, {'ㅅ', 34}, {'ㅇ', 35}, {'ㅈ', 36}, {'ㅊ', 37},
        {'ㅋ', 38}, {'ㅌ', 39}, {'ㅍ', 40}, {'ㅎ', 41}, {'ㅏ', 42}, {'ㅑ', 43}, {'ㅓ', 44}, {'ㅕ', 45}, {'ㅗ', 46}, {'ㅛ', 47},
        {'ㅜ', 48}, {'ㅠ', 49}, {'ㅡ', 50}, {'ㅣ', 51}, {'ㅐ', 52}, {'ㅔ', 53}
    };

    private string[] EnglishRotors =
    {
        "F6@!PJZ2V5Q3U-:HMY_NI0K)O/X=D,CG?RS4T.BL\"E×;(71A~+'89W",
        "@GZMHA,2+P!T=-4(IYBO)Q~WJ'7E\"RD10/5UF8?L.KCS6V:×9;3_NX",
        "@2Q0_7WJ+,)8H(=MP?I9BYX35-./!16E4VZ\"GS:'D;TOUARFK~C×NL",
        "5W6HZT+LC-RNO7(41QB9;.?AX)3K'8M:D0\"VJG×~_!PYI@F2USE=/,",
        "W5P_\"QVF@17MB8:!C+=J4GO(3X.ITSZ-/L~)'K×NRD?E6U;92HY,0A",
        "PY(~KHT.?OG;M_84LJ7DC+)0F@=WZ5362':VIS9-X!N/\"1Q,ARE×BU",
        "U1ZSKMP×Y4L6~=2Q/ADR,O3)@NG9.?5IB0CFWH!+J\"7;_-XE:8V'T(",
        "I\"6LZF4/KD×X;ERA9!U1GJ08(3~@,72SB:WQ=+CHN.M)5?V-YT_PO'"
    };

    private string[] JapaneseRotors =
    {
        "\":ラツネ゛+ヨテシ~ケエ5コヒヌヱモ=クマフ0ヤル.メ゜19×チム,ヰアスセ@イ6;ワミ4キ7!_ホ/ンサナイオウノカ23リロ'タ?レ-)ニ(ハヲト8ヘソ",
        "シヱリニヨヲヒ-_ソイ7ト@コノ~マス;+4ヰケ゜/.,(ホナ9ロオツ6タ?ル15'ク2サア8!ハイレワ゛ンチヤメヌセムモネミ×:フキカヘラテウエ=3\")0",
        "ヨ(\"ネヱケスフ+ワレ2タハ9ナクテ,ミロエ6ツトホ~゛.3ヲラヤムモノ5ア×セマソコ7ヰルメシ8チオ゜ウ_ヌヘ;イイ?ヒリ)サ:ニカ4=0キ1-!@ン'/",
        "レセ)~ホイコ+4_モケフ3;ノ!8エメタヌクソヘワヤチ-ン×,ヱミマ9ニ6キヒト゜ラネ2ヨ0ロサムス@ウテルイ5\"ナ:カ゛ヰ1リ'(.オ7=?ヲハツシ/ア",
        "゛チイヒ~17ヤテカモツ9ヱハスエレシ,アサウル)トメケヌム4/ロソイヲ(ワ=?ノ;0+セミ3ヨ×ヰニクマンネ@オ゜\"キリホ.!-コ26_ナ'5フタ:ヘ8ラ",
        "ヰニ;ホレ/.イフ゜×ムミツオンイ4スヒエシアタヌ5ソヘク('ハカヤリナ2チ@?ヱ1゛7セテコ:ル-9サケ~=ワウ)ロメト3\"ノヲモ6マラ8+ネ,!キ_0ヨ",
        "チライキイヲ6/ンサ3フク!4カネル)エミオ-セ5ヤ17シ8゜ハツトウ;コ9レナヘ_ケヨタニス'メノ~ヌ0+ヰ:゛?\"アリワマ,(テホロ@2ヒ.ムヱ×ソモ=",
        "カモ@サ'ンマ1ロ3ハヨフ9メケヒ;イエクヰホネ+\"ミツレワ×)ノチ7,ヱ4オ=8コシ゜(キア0ヘナ~テス2:ルト6ウニ゛ムタラ_5ヌセソ/-!ヤ?ヲイ.リ"
    };

    private string[] KoreanRotors =
    {
        "ㅔㅑ;ㅡㅗㅍㄱㅏㅊ_28),/6!ㅜㅠㅂ4ㅓ-×5ㅅ0ㅛㄴ=1ㅎㅐㅋㅣ?.ㅈ(9:ㅌㅕ'+ㅁ@ㅇ~ㄷㄹ37\"",
        "ㅜ4(ㅅ'ㄱ9@ㅗㅂ2ㄹ6/1;+3_)!ㅑㅋ5ㅊㄴ:ㅇ8ㅍㅐㅌㅎㄷ?×0-7ㅏ\"ㅕㅓㅡㅁㅣ,~.ㅛㅠㅈㅔ=",
        "'ㅁ2:ㅌ-ㅅㅜ×ㅂ?417@ㅕ=ㄹ+ㅗㄴㅔ\"0ㅍ.ㅛㄱ/ㅇㅐㅏ853;(ㅑㄷ9ㅈ_ㅣㅎㅊ~ㅋㅓㅡ)ㅠ6!,",
        "0/(?1ㅅㅍㅛㅇ!ㅡㅜ:92ㅕㅂㅌ~+ㅏ\"ㅔ-ㅊㅈ×7ㄹ4ㄷㅗㅐ)@5;ㄴㄱㅣ,ㅓ'8.=ㅠㅋ63_ㅑㅎㅁ",
        "_ㅑ×.\"ㅗㅓ)!5ㅅㅏㅜㅔㅁ;ㅣ~ㅋ?+/ㅕ0ㅠ,ㅐㅂ87(ㅇ@ㅎㅍ:6'49ㅌㅡㄱ=ㅊㄴㄷㅈ1ㄹㅛ23-",
        "ㅛ.1ㅜ_9-'~\"ㅇㄹㅂㄷㅣㅕ0ㅍㅁ4!3ㅓ2ㅔ7,5ㅎㅅㅑ(=ㅈ×ㅐ/@;+?ㅠ)ㅡㄴ8ㅏㅋ:6ㄱㅊㅌㅗ",
        "ㅅ8ㅌ~:ㅕ;'ㅏ.ㅊㅜ?43×ㅐㅠ@_ㅓ6ㅑㄴㄹ9ㄱㅎ\"=/ㅁㅋㅂ5ㅔㅡ7)ㅈ0-ㅇ,12+ㅣ!ㅗㅍ(ㄷㅛ",
        "+×ㅏ/ㅠㅅ(ㅡㅍ)ㅔ,ㅓㄷㅇㅂㅜ1ㄴ2ㅁㅣㅐ!ㅊㅋ'ㅑㅗㅕ90?ㅈㅌ@3_ㅛ8ㄱ-=4ㅎ.ㄹ\"76~5:;"
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
