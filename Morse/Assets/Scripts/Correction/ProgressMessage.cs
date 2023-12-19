using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressMessage : MonoBehaviour
{
    private string[] messages; 

    private TextMeshProUGUI messageField;

    private bool isMessageEnd;

    private void Start()
    {
        messages = new string[] {
            "안녕하세요 " + Singleton<User>.instance.userInfo.name + "님",
            "지금부터 보정을 시작하도록 하겠습니다",
            "들리는 소리에 맞춰서 스페이스바를 눌러주세요",
            "엔터를 누르면 시작합니다",
            "엔터를 눌러주세요",
            ""
        };
        messageField = GetComponent<TextMeshProUGUI>();
        StartCoroutine(GuideMessage());
    }

    private void Update()
    {
        if (isMessageEnd && Input.GetKeyDown(KeyCode.Return) && !Singleton<CMetronome>.instance.isEndGuide) Singleton<CMetronome>.instance.isEndGuide = true;
    }

    IEnumerator GuideMessage()
    {
        messageField.text = messages[0];
        yield return new WaitForSeconds(1.5f);
        messageField.text = messages[1];
        yield return new WaitForSeconds(2.5f);
        messageField.text = messages[2];
        yield return new WaitForSeconds(2.5f);
        messageField.text = messages[3];
        yield return new WaitForSeconds(1.5f);
        messageField.text = messages[4];
        isMessageEnd = true;
    }
}
