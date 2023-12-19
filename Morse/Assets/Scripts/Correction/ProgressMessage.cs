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
            "�ȳ��ϼ��� " + Singleton<User>.instance.userInfo.name + "��",
            "���ݺ��� ������ �����ϵ��� �ϰڽ��ϴ�",
            "�鸮�� �Ҹ��� ���缭 �����̽��ٸ� �����ּ���",
            "���͸� ������ �����մϴ�",
            "���͸� �����ּ���",
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
