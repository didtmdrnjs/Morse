using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Record : MonoBehaviour
{
    private void Update()
    {
        string path = "file://" + Application.streamingAssetsPath + "/Music/" +
                MusicInfo.instance.datas[MusicInfo.instance.currentMusicIndex].name + "/" +
                (GameManager.instance.mode == EMode.OneWord ? "One" : "Two") +
                (GameManager.instance.difficulty == 0 ? "Easy" : "Hard") + ".json";
        transform.GetChild(1).GetComponentInChildren<TMP_Text>().text =
            !File.Exists(path) ?
            "No Record" :
            GetRecord(path);
    }

    private string GetRecord(string path)
    {
        RecordData data = MusicInfo.instance.records[MusicInfo.instance.currentMusicIndex][(int)GameManager.instance.mode, GameManager.instance.difficulty];
        if (data == null)
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<RecordData>(json);
            MusicInfo.instance.records[MusicInfo.instance.currentMusicIndex][(int)GameManager.instance.mode, GameManager.instance.difficulty] = data;
        }
        return
            "Score : " + data.score + "\n" +
            "<color=green>Perfect</color> : " + data.perfect + "\n" +
            "<color=#64FF00>Greate</color> : " + data.greate + "\n" +
            "<color=#FF6400>Good</color> : " + data.good + "\n" +
            "<color=red>Fail</color> : " + data.fail + "\n" +
            "Max Combo : " + data.maxCombo + "\n" +
            (data.isFullCombo ? "<color=yellow><b>Full Combo</b></color>\n" : "") +
            (data.isPerfect ? "<color=#00FFFF><b>Perfect</b></color>" : "");
    }
}
