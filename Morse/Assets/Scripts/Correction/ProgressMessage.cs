using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressMessage : MonoBehaviour
{
    private string[] messages;
    private string[] againMessage = {
        "�Է¿� �ϰ����� �����ϴ�",
        "������ �ٽ� �����մϴ�",
        ""
    };

    private TextMeshProUGUI messageField;

    private bool isMessageEnd;

    private void Start()
    {
        GameManager.instance.isPlayMusic = true;
        messages = new string[] {
            "�ȳ��ϼ��� " + User.instance.userInfo.name + "��",
            "���ݺ��� ������ �����ϵ��� �ϰڽ��ϴ�",
            "�鸮�� �Ҹ��� ���缭 �����̽��� �����ּ���",
            "���͸� ������ �����մϴ�",
            "���͸� �����ּ���",
            ""
        };
        messageField = GetComponent<TextMeshProUGUI>();
        StartCoroutine(GuideMessage());
    }

    private void Update()
    {
        if (isMessageEnd && Input.GetKeyDown(KeyCode.Return) && !Singleton<CMetronome>.instance.isEndGuide)
        {
            messageField.text = messages[5];
            Singleton<CMetronome>.instance.isEndGuide = true;
            isMessageEnd = false;
        }
        
        if (Singleton<CMetronome>.instance.isAgain) StartCoroutine(AgainMessage());
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

    IEnumerator AgainMessage()
    {
        messageField.text = messages[0];
        yield return new WaitForSeconds(1f);
        messageField.text = messages[1];
        yield return new WaitForSeconds(1f);
        messageField.text = messages[2];
        Singleton<CMetronome>.instance.isAgain = false;
    }
}
