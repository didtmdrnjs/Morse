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
        "�鸮�� �Ҹ��� ���缭 �����̽��� �����ּ���"
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
        };
        messageField = GetComponent<TextMeshProUGUI>();
        StartCoroutine(GuideMessage());

        CMetronome.instance.onCorrectionEnd += () =>
        {
            messageField.text = "�������� " + (int)(GameManager.instance.offset * 1000) + "ms�� �����˴ϴ�.";
        };
    }

    private void Update()
    {
        if (isMessageEnd)
        {
            CMetronome.instance.isEndGuide = true;
            isMessageEnd = false;
        }
        
        if (CMetronome.instance.isAgain) StartCoroutine(AgainMessage());
    }

    IEnumerator GuideMessage()
    {
        messageField.text = messages[0];
        yield return new WaitForSeconds(1.5f);
        messageField.text = messages[1];
        yield return new WaitForSeconds(2.5f);
        messageField.text = messages[2];
        yield return new WaitForSeconds(2f);
        isMessageEnd = true;
    }

    IEnumerator AgainMessage()
    {
        messageField.text = messages[0];
        yield return new WaitForSeconds(1f);
        messageField.text = messages[1];
        yield return new WaitForSeconds(1f);
        messageField.text = messages[2];
        CMetronome.instance.isAgain = false;
    }
}
