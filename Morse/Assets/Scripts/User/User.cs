using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class User : MonoBehaviour
{
    public static User instance;

    public UserInfo userInfo;
    public UserSetting userSetting;

    public string path = Application.dataPath + "/User";

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

        SetUser();
    }

    private void SetUser()
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Singleton<TitleManager>.instance.InputName.SetActive(true);
        }
        else
        {
            Singleton<TitleManager>.instance.isEndWriteUser = true;
            StartCoroutine(ReadUser());
        }
    }

    IEnumerator ReadUser()
    {
        yield return null;

        string infoFile = path + "/userInfo.Json"; 
        string settingFile = path + "/userSetting.Json"; 

        string info = File.ReadAllText(infoFile);
        string setting = File.ReadAllText(settingFile);

        UserInfo infoData = JsonUtility.FromJson<UserInfo>(info);
        UserSetting settingData = JsonUtility.FromJson<UserSetting>(setting);

        userInfo.name = infoData.name;
        userInfo.level = infoData.level;
        userInfo.exp = infoData.exp;

        userSetting.OneShortKey = settingData.OneShortKey;
        userSetting.OneLongKey = settingData.OneLongKey;
        userSetting.TwoFirstShortKey = settingData.TwoFirstShortKey;
        userSetting.TwoFirstLongKey = settingData.TwoFirstLongKey;
        userSetting.TwoSecondShortKey = settingData.TwoSecondShortKey;
        userSetting.TwoSecondLongKey = settingData.TwoSecondLongKey;
        userSetting.offset = settingData.offset;
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
}