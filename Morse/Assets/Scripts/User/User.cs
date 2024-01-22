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
        string path = instance.path;

        UserInfo infoData = new UserInfo();
        UserSetting settingData = new UserSetting();

        infoData = userInfo;

        string info = JsonUtility.ToJson(infoData);
        File.WriteAllText(path + "/userInfo.Json", info);

        settingData.OneShortKey = userSetting.OneShortKey;
        settingData.OneLongKey = userSetting.OneLongKey;
        settingData.TwoFirstShortKey = userSetting.TwoFirstShortKey;
        settingData.TwoFirstLongKey = userSetting.TwoFirstLongKey;
        settingData.TwoSecondShortKey = userSetting.TwoSecondShortKey;
        settingData.TwoSecondLongKey = userSetting.TwoSecondLongKey;
        settingData.offset = GameManager.instance.offset;

        string setting = JsonUtility.ToJson(settingData);
        File.WriteAllText(path + "/userSetting.Json", setting);
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

        userInfo = infoData;
        userSetting = settingData;

        GameManager.instance.offset = userSetting.offset;
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