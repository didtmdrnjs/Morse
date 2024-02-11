using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressMessage : MonoBehaviour
{
    private string[] messages;
    private string[] againMessage = {
        "입력에 일관성이 없습니다",
        "보정을 다시 시작합니다",
        "들리는 소리에 맞춰서 스페이스를 눌러주세요"
    };

    private TextMeshProUGUI messageField;
    private bool isMessageEnd;

    private void Start()
    {
        GameManager.instance.isPlayMusic = true;
        messages = new string[] {
            "안녕하세요 " + User.instance.userInfo.name + "님",
            "지금부터 보정을 시작하도록 하겠습니다",
            "들리는 소리에 맞춰서 스페이스를 눌러주세요",
        };
        messageField = GetComponent<TextMeshProUGUI>();
        StartCoroutine(GuideMessage());

        CMetronome.instance.onCorrectionEnd += () =>
        {
            messageField.text = "오프셋이 " + (int)(GameManager.instance.offset * 1000) + "ms로 조정됩니다.";
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
