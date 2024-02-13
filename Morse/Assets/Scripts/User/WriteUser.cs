using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class WriteUser : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<TMP_InputField>().ActivateInputField();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Title.instance.isEndWriteUser = true;
            WriteUserData();
            StartCoroutine(User.instance.ReadUser());
            transform.parent.gameObject.SetActive(false);
        }
    }

    private void WriteUserData()
    {
        string path = User.instance.userPath;

        UserInfo infoData = new UserInfo();
        UserSetting settingData = new UserSetting();

        infoData.name = GetComponent<TMP_InputField>().text;
        infoData.level = 1;
        infoData.exp = 0;

        string info = JsonUtility.ToJson(infoData);
        File.WriteAllText(path + "/userInfo.Json", info);

        settingData.OneShortKey = KeyCode.F;
        settingData.OneLongKey = KeyCode.J;
        settingData.TwoFirstShortKey = KeyCode.D;
        settingData.TwoFirstLongKey = KeyCode.F;
        settingData.TwoSecondShortKey = KeyCode.J;
        settingData.TwoSecondLongKey = KeyCode.K;
        settingData.offset = 0;
        settingData.volum = 10;

        string setting = JsonUtility.ToJson(settingData);
        File.WriteAllText(path + "/userSetting.Json", setting);
    }
}
