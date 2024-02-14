using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class MusicInfo : MonoBehaviour
{
    public static MusicInfo instance;

    private const string version_url = "https://script.google.com/macros/s/AKfycbxlE73a8Zy4xU3rwfoK8w8MDYjAAUHBuDJYHEwoNbYJrzEBqIMFMINjTkryXG0WXjwr/exec";
    private const string musicData_url = "https://script.google.com/macros/s/AKfycbzLyyMJidozrmtVTcKZQkCkU86cy3VXbuNasOZVfcAvh9vg7LOQflV-dqc8JeeKJ3Sr/exec";

    private string musicDirectory;
    private string version;

    public List<MusicData> datas = new();
    public List<RecordData[,]> records = new List<RecordData[,]>();
    public int currentMusicIndex;

    public bool isLoadScene;

    public float currentLoadElement;
    public float totalLoadElement;
    public bool isLoadTotal;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        musicDirectory = Application.dataPath + "/Music";

        if (!Directory.Exists(musicDirectory))
        {
            Directory.CreateDirectory(musicDirectory);
            StartCoroutine(DownLoadMusicData());
        }
        else StartCoroutine(ReadVersion());

        currentMusicIndex = 0;
        currentLoadElement = 0;
    }

    private void Update()
    {
        IndexCheck();
    }

    public void IndexCheck()
    {
        if (currentMusicIndex < 0) currentMusicIndex = datas.Count - 1;
        if (currentMusicIndex == datas.Count) currentMusicIndex = 0;
    }

    IEnumerator ReadVersion()
    {
        UnityWebRequest www = UnityWebRequest.Get(version_url);
        yield return www.SendWebRequest();

        version = File.ReadAllText(Application.dataPath + "/Json/version.txt");

        if (version != www.downloadHandler.text)
        {
            File.WriteAllText(Application.dataPath + "/Json/version.txt", www.downloadHandler.text);
            StartCoroutine(DownLoadMusicData());
        }
        else StartCoroutine(ReadJson());
    }

    IEnumerator DownLoadMusicData()
    {
        totalLoadElement = 3;
        UnityWebRequest musicData_www = UnityWebRequest.Get(musicData_url);
        yield return musicData_www.SendWebRequest();

        SetMusicData(musicData_www.downloadHandler.text);
    }

    IEnumerator GetTexture(int idx, string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success) StartCoroutine(GetTexture(idx, url));
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Debug.Log(DownloadHandlerTexture.GetContent(www));

            datas[idx].pngBytes = texture.EncodeToPNG();

            Rect rect = new Rect(0, 0, texture.width, texture.height);
            Sprite sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));

            datas[idx].image = sprite;

            if (idx == datas.Count - 1)
            {
                currentLoadElement++;
                isLoadTotal = true;

                yield return new WaitForSeconds(1);
                StartCoroutine(CheckImage());
            }
        }
    }

    IEnumerator CheckImage()
    {
        Debug.Log(datas[datas.Count - 1].image);
        if (datas[datas.Count - 1].image != null) StartCoroutine(SaveData());
        else yield return StartCoroutine(CheckImage());
    }

    IEnumerator SaveData()
    {
        yield return null;
        for (int i = 0; i < datas.Count; i++)
        {
            string path = musicDirectory + "/" + datas[i].name;
            Directory.CreateDirectory(path);

            MusicData data;
            data = new MusicData();

            data.id = datas[i].id;
            data.name = datas[i].name;
            data.language = datas[i].language;
            data.bpm =  datas[i].bpm;
            data.mapData = datas[i].mapData;

            string musicData = JsonUtility.ToJson(data);
            File.WriteAllText(path + "/MusicData.Json", musicData);
            File.WriteAllBytes(path + "/" + datas[i].name + ".png", datas[i].pngBytes);
        }
        currentLoadElement++;
        isLoadTotal = true;
    }

    IEnumerator ReadJson()
    {
        yield return null;
        string[] directorys = Directory.GetDirectories(musicDirectory);
        totalLoadElement = directorys.Length;
        
        for (int i = 0; i < directorys.Length; i++) 
        { 
            datas.Add(new MusicData());
            records.Add(new RecordData[2, 2]);
        }

        foreach (string path in directorys)
        {
            string fileName = path.Split('\\')[1];
            string dirPath = musicDirectory + "/" + fileName;

            string json = File.ReadAllText(dirPath + "/MusicData.Json");
            MusicData data = JsonUtility.FromJson<MusicData>(json);

            datas[data.id].id = data.id;
            datas[data.id].name = data.name;
            datas[data.id].language = data.language;
            datas[data.id].bpm = data.bpm;
            datas[data.id].mapData = data.mapData;
            datas[data.id].pngBytes = File.ReadAllBytes(dirPath + "/" + fileName + ".png");

            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(datas[data.id].pngBytes);
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            datas[data.id].image = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));

            currentLoadElement++;
            isLoadTotal = true;
        }
    }

    private void SetMusicData(string Data)
    {
        string[] data = Data.Split(",");
        for (int i = 0; i < data.Length; i++)
        {
            string[] info = data[i].Split(" ");
            MusicData musicData = new MusicData();
            musicData.id = Convert.ToInt32(info[0]);
            musicData.name = info[1];
            StartCoroutine(GetTexture(i, info[2]));
            musicData.language = info[3];
            musicData.bpm = Convert.ToInt32(info[4]);

            string map = info[5].Replace("*", ",");
            for (int j = 6; j < info.Length; j++) map += " " + info[j].Replace("*", ",");
            musicData.mapData = "   " + map + " ";

            datas.Add(musicData);
            records.Add(new RecordData[2, 2]);
        }
        currentLoadElement++;
        isLoadTotal = true;
    }
}

public class MusicData
{
    public int id;

    public string name;

    public Sprite image;

    public byte[] pngBytes;

    public string language;

    public int bpm;

    public string mapData;
}

public enum EMode
{
    OneWord,
    TwoWord
}