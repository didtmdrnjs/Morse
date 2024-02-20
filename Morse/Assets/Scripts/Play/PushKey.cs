using TMPro;
using UnityEngine;

public class PushKey : MonoBehaviour
{
    public TMP_Text one;
    public TMP_Text two;

    private float bpm;
    private string oneModeCode;
    private string twoModeCode;

    private void Start()
    {
        bpm = PlayManager.instance.bpm;
        PlayManager.instance.onOneFail += () =>
        {
            one.text = "<color=red>fail</color>";
            Score.instance.failCount++;
        };
        PlayManager.instance.onTwoFail += () =>
        {
            two.text = "<color=red>fail</color>";
            Score.instance.failCount++;
        };
    }

    private void Update()
    {
        oneModeCode = CreateWord.instance.Code.text;
        twoModeCode = ShowEnigma.instance.Code.text;
        if (GameManager.instance.mode == EMode.OneWord) OneMode();
        else TwoMode();
    }

    private void OneMode()
    {
        if (PlayManager.instance.isOneInput) return;

        if (Input.GetKeyDown(User.instance.userSetting.OneShortKey) && PlayManager.instance.currentCode == '，') OneVerdict('，');
        if (Input.GetKeyDown(User.instance.userSetting.OneLongKey) && PlayManager.instance.currentCode == '！') OneVerdict('！');
    }

    private void TwoMode()
    {
        if (!PlayManager.instance.isOneInput)
        {
            if (Input.GetKeyDown(User.instance.userSetting.TwoFirstShortKey) && PlayManager.instance.currentCode == '，') OneVerdict('，');
            if (Input.GetKeyDown(User.instance.userSetting.TwoFirstLongKey) && PlayManager.instance.currentCode == '！') OneVerdict('！');
        }
        if (!PlayManager.instance.isTwoInput)
        {
            if (Input.GetKeyDown(User.instance.userSetting.TwoSecondShortKey) && PlayManager.instance.curEnigmaCode == '，') TwoVerdict('，');
            if (Input.GetKeyDown(User.instance.userSetting.TwoSecondLongKey) && PlayManager.instance.curEnigmaCode == '！') TwoVerdict('！');
        }
    }

    private void OneVerdict(char code)
    {
        PlayManager.instance.isOneInput = true;
        Score.instance.maxCombo = Mathf.Max(Score.instance.maxCombo, ++Score.instance.combo);
        float inputTime = PlayManager.instance.offsetTime;
        float verdict = inputTime >= (60 / bpm / 2) ? (60 / bpm) - inputTime : inputTime;

        string[] codes = oneModeCode.Split(' ');
        string color = "";
        if (verdict <= (60 / bpm / 7))
        {
            one.text = "<color=green>perfect</color>";
            Score.instance.perfectCount++;
            color = "#00FF00";
        }
        else if (verdict <= (60 / bpm / 4))
        {
            one.text = "<color=#64FF00>greate</color>";
            Score.instance.greateCount++;
            color = "#64FF00";
        }
        else if (verdict <= (60 / bpm / 2))
        {
            one.text = "<color=#FF6400>good</color>";
            Score.instance.goodCount++;
            color = "#FF6400";
        }

        SetMorseCode(codes, color, code);
    }

    private void SetMorseCode(string[] codes, string color, char code)
    {
        if (!CreateWord.instance.isEasy) return;

        codes[PlayManager.instance.offsetMorseIdx] = "<color=" + color + ">" + code + "</color>";
        string morseCode = codes[0];
        for (int i = 1; i < codes.Length; i++)
        {
            morseCode += " " + codes[i];
        }
        CreateWord.instance.Code.text = morseCode;
    }

    private void TwoVerdict(char code)
    {
        PlayManager.instance.isTwoInput = true;
        Score.instance.maxCombo = Mathf.Max(Score.instance.maxCombo, ++Score.instance.combo);
        float inputTime = PlayManager.instance.offsetTime;
        float verdict = inputTime >= (60 / bpm / 2) ? (60 / bpm) - inputTime : inputTime;

        string[] codes = twoModeCode.Split(' ');
        string color = "";
        if (verdict <= (60 / bpm / 7))
        {
            two.text = "<color=green>perfect</color>";
            Score.instance.perfectCount++;
            color = "#00FF00";
        }
        else if (verdict <= (60 / bpm / 4))
        {
            two.text = "<color=#64FF00>greate</color>";
            Score.instance.greateCount++;
            color = "#64FF00";
        }
        else if (verdict <= (60 / bpm / 2))
        {
            two.text = "<color=#FF6400>good</color>";
            Score.instance.goodCount++;
            color = "#FF6400";
        }

        SetEnigmaCode(codes, color, code);
    }

    private void SetEnigmaCode(string[] codes, string color, char code)
    {
        if (!ShowEnigma.instance.isEasy) return;

        codes[PlayManager.instance.offsetEnigmaIdx] = "<color=" + color + ">" + code + "</color>";
        string morseCode = codes[0];
        for (int i = 1; i < codes.Length; i++)
        {
            morseCode += " " + codes[i];
        }
        ShowEnigma.instance.Code.text = morseCode;
    }
}
