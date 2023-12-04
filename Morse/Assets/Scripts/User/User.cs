using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : Singleton<User>
{
    public UserInfo userInfo;
    public UserSetting userSetting;

    private void Start()
    {
        userInfo = new UserInfo();
        userSetting = new UserSetting();
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

    public AudioClip hitSound;
}