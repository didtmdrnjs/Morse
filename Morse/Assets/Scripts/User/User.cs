using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class User : MonoBehaviour
{
    public static User instance;

    public UserInfo userInfo;
    public UserSetting userSetting;
    public LevelData levels;

    public string userPath = "file://" + Application.streamingAssetsPath + "/User";
    public string lavelDataPath = "file://" + Application.streamingAssetsPath + "/Json/LevelData.json";

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) Destroy(gameObject);
        
        userInfo = new UserInfo();
        userSetting = new UserSetting();
        levels = new LevelData();

        SetUser();
    }

    private void SetUser()
    {
        StartCoroutine(ReadLevelData());
        if (!Directory.Exists(userPath))
        {
            Directory.CreateDirectory(userPath);
            Title.instance.InputName.SetActive(true);
        }
        else
        {
            Title.instance.isEndWriteUser = true;
            StartCoroutine(ReadUser());
        }
    }

    public void WriteUserData()
    {
        string path = instance.userPath;

        string info = JsonUtility.ToJson(userInfo);
        File.WriteAllText(path + "/userInfo.Json", info);

        string setting = JsonUtility.ToJson(userSetting);
        File.WriteAllText(path + "/userSetting.Json", setting);
    }

    public IEnumerator ReadUser()
    {
        yield return null;
        
        string infoFile = userPath + "/userInfo.Json"; 
        string settingFile = userPath + "/userSetting.Json"; 

        string info = File.ReadAllText(infoFile);
        string setting = File.ReadAllText(settingFile);

        userInfo = JsonUtility.FromJson<UserInfo>(info);
        userSetting = JsonUtility.FromJson<UserSetting>(setting);
    }

    public IEnumerator ReadLevelData()
    {
        yield return null;

        string data = File.ReadAllText(lavelDataPath);
        levels = JsonUtility.FromJson<LevelData>(data);
    }

    public void SetLevel(int x)
    {
        userInfo.exp += (8 - x) * 2 * (userInfo.level / 2f) * (GameManager.instance.difficulty == 0 ? 1 : 2) * (GameManager.instance.mode == EMode.OneWord ? 1 : 5);
        while (userInfo.exp >= levels.datas[userInfo.level].maxExp && userInfo.level < 50)
        {
            userInfo.level++;
            userInfo.exp -= levels.datas[userInfo.level].maxExp;
        }

        WriteUserData();
    }
}

public class UserInfo
{
    public string name;
    public int level;
    public float exp;
}

public class UserSetting
{
    public KeyCode OneShortKey;
    public KeyCode OneLongKey;

    public KeyCode TwoFirstShortKey;
    public KeyCode TwoFirstLongKey;
    
    public KeyCode TwoSecondShortKey;
    public KeyCode TwoSecondLongKey;

    public float offset;
    public int volum;
}

public class LevelData
{
    public List<LevelInfo> datas;
}

[System.Serializable]
public class LevelInfo
{
    public int level;
    public int maxExp;
}