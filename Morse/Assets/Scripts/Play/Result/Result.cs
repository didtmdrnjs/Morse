using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;
using System.Collections;

public class Result : MonoBehaviour
{

    public TMP_Text ret;
    public TMP_Text musicName;
    public Image img;
    public Image rank;
    public List<Image> rankImgs;

    private MusicData data;
    private int score;
    private float rate;
    private int perfectCount;
    private int greateCount;
    private int goodCount;
    private int failCount;
    private int maxCombo;

    private void Start()
    {
        Init();
        img.sprite = data.image;
        musicName.text = data.name;
        ret.text = 
            "Mode : " + (GameManager.instance.mode == EMode.OneWord ? "One" : "Two") + "\n" +
            "Difficult : " + (GameManager.instance.difficulty == 0 ? "Easy" : "Hard") + "\n" + 
            "\n" + 
            "Score : " + score + "\n" + 
            "Max Combo : " + maxCombo + "\n" +
            "Rate : " + rate.ToString("F2") + "%\n" + 
            "\n" +
            "<color=green>Perfect</color> : " + perfectCount + "\n" +
            "<color=#64FF00>Greate</color> : " + greateCount + "\n" +
            "<color=#FF6400>Good</color> : " + goodCount + "\n" +
            "<color=red>Fail</color> : " + failCount;
        //SetRank();
        Destroy(Score.instance.gameObject);
        SaveRecord();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene("SelectMusic");
    }

    private void Init()
    {
        data = MusicInfo.instance.datas[MusicInfo.instance.currentMusicIndex];
        Score.instance.SetCount();
        score = Score.instance.GetScore();
        rate = Score.instance.GetRate();
        perfectCount = Score.instance.perfectCount;
        greateCount = Score.instance.greateCount;
        goodCount = Score.instance.goodCount;
        failCount = Score.instance.failCount;
        maxCombo = Score.instance.maxCombo;
    }

    private void SetRank()
    {
        if (rate == 100) rank = rankImgs[0]; // SSS
        else if (rate >= 98) rank = rankImgs[1]; // SS
        else if (rate >= 94) rank = rankImgs[2]; // S
        else if (rate >= 86) rank = rankImgs[3]; // A
        else if (rate >= 70) rank = rankImgs[4]; // B
        else if (rate >= 54) rank = rankImgs[5]; // C
        else if (rate >= 38) rank = rankImgs[6]; // D
        else rank = rankImgs[7]; // F
    }

    private void SaveRecord()
    {
        string path = Application.dataPath + "/Music/" + data.name + "/" + (GameManager.instance.mode == EMode.OneWord ? "One" : "Two") + (GameManager.instance.difficulty == 0 ? "Easy" : "Hard") + ".json";
        if (!File.Exists(path)) StartCoroutine(Save(path));
        else ReadScore(path);
    }

    IEnumerator Save(string path)
    {
        yield return null;
        
        RecordData data = new RecordData();
        data.score = score;
        data.perfect = perfectCount;
        data.greate = greateCount;
        data.good = goodCount;
        data.fail = failCount;
        data.maxCombo = maxCombo;
        data.isFullCombo = failCount == 0;
        data.isPerfect = failCount == 0 && goodCount == 0 && greateCount == 0;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    IEnumerator ReadScore(string path)
    {
        yield return null;
        string json = File.ReadAllText(path);
        RecordData data = JsonUtility.FromJson<RecordData>(json);
        if (data.score < score) Save(path);
    }
}

public class RecordData
{
    public int score;
    public int perfect;
    public int greate;
    public int good;
    public int fail;
    public int maxCombo;
    public bool isFullCombo;
    public bool isPerfect;
}