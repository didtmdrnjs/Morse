using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class MusicInfo : MonoBehaviour
{
    public static MusicInfo instance;

    private const string musicList_url = "https://script.google.com/macros/s/AKfycbx43AuyPl3PKX9n-0GPbsLf69azaLjGxqZXg8WH-HQv2hkr3tlT_AP5zURieT12yP7B/exec";
    private const string musicData_url = "https://script.google.com/macros/s/AKfycbyy78Cui9ci2ZB5hUYmd2nG8ukhm033MFNO9OXtjlMA_C6eq9M1wF1Vlg44y1vLYvNV/exec";

    private string musicDirectory;

    public MusicList musicList;
    public int currentMusicIndex;

    public int difficulty;
    public Mode mode;

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
        musicList = new MusicList();

        if (!Directory.Exists(musicDirectory))
        {
            Directory.CreateDirectory(musicDirectory);
            StartCoroutine(DownLoadMusicData());
            totalLoadElement = 3;
        }
        else StartCoroutine(ReadJson());

        currentMusicIndex = 0;
        difficulty = 0;
        mode = Mode.OneWord;

        currentLoadElement = 0;
    }

    private void Update()
    {
        IndexCheck();
    }

    public void IndexCheck()
    {
        if (currentMusicIndex < 0) currentMusicIndex = musicList.datas.Count - 1;
        if (currentMusicIndex == musicList.datas.Count) currentMusicIndex = 0;
    }

    IEnumerator DownLoadMusicData()
    {
        UnityWebRequest musicList_www = UnityWebRequest.Get(musicList_url);
        yield return musicList_www.SendWebRequest();

        UnityWebRequest musicData_www = UnityWebRequest.Get(musicData_url);
        yield return musicData_www.SendWebRequest();

        SetMusicData(musicList_www.downloadHandler.text, musicData_www.downloadHandler.text);
    }

    IEnumerator GetTexture(int idx, string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        
        Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        Debug.Log(DownloadHandlerTexture.GetContent(www));

        musicList.datas[idx].pngBytes = texture.EncodeToPNG();

        Rect rect = new Rect(0, 0, texture.width, texture.height);
        Sprite sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));

        musicList.datas[idx].image = sprite;

        if (idx == musicList.datas.Count - 1)
        {
            currentLoadElement++;
            isLoadTotal = true;

            yield return new WaitForSeconds(1);

            StartCoroutine(SaveData());
        }
    }

    IEnumerator SaveData()
    {
        yield return null;
        for (int i = 0; i < musicList.datas.Count; i++)
        {
            string path = musicDirectory + "/" + musicList.datas[i].name;
            Directory.CreateDirectory(path);

            MusicData data;
            data = new MusicData();

            data.id = musicList.datas[i].id;
            data.name = musicList.datas[i].name;
            data.language = musicList.datas[i].language;
            data.bpm = musicList.datas[i].bpm;
            data.mapData = musicList.datas[i].mapData;

            string musicData = JsonUtility.ToJson(data);
            File.WriteAllText(path + "/MusicData.Json", musicData);
            File.WriteAllBytes(path + "/" + musicList.datas[i].name + ".png", musicList.datas[i].pngBytes);
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
            musicList.datas.Add(new MusicData());
        }

        foreach (string path in directorys)
        {
            string fileName = path.Split('\\')[1];
            string dirPath = musicDirectory + "/" + fileName;

            string json = File.ReadAllText(dirPath + "/MusicData.Json");
            MusicData data = JsonUtility.FromJson<MusicData>(json);

            musicList.datas[data.id].id = data.id;
            musicList.datas[data.id].name = data.name;
            musicList.datas[data.id].language = data.language;
            musicList.datas[data.id].bpm = data.bpm;
            musicList.datas[data.id].mapData = data.mapData;
            musicList.datas[data.id].pngBytes = File.ReadAllBytes(dirPath + "/" + fileName + ".png");

            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(musicList.datas[data.id].pngBytes);
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            musicList.datas[data.id].image = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));

            currentLoadElement++;
            isLoadTotal = true;
        }
    }

    private void SetMusicData(string List, string Data)
    {
        string[] list = List.Split(',');
        string[] data = Data.Split(",");

        for (int i = 0; i < list.Length; i++)
        {
            string[] LInfo = list[i].Split(" ");
            string[] DInfo = data[i].Split(" ");

            MusicData musicData = new MusicData();
            musicData.id = int.Parse(LInfo[0]);
            musicData.name = LInfo[1];
            StartCoroutine(GetTexture(i, LInfo[2]));
            musicData.language = DInfo[1];
            musicData.bpm = int.Parse(DInfo[2]);
            musicData.mapData = DInfo[3];
            musicList.datas.Add(musicData);
        }
        currentLoadElement++;
        isLoadTotal = true;
    }
}

public class MusicList
{
    public List<MusicData> datas;

    public MusicList() 
    {
        datas = new List<MusicData>();
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

public enum Mode
{
    OneWord,
    TwoWord
}