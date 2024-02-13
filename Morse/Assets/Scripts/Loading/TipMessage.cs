using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TipMessage : MonoBehaviour
{
    private string[] tips =
    {
        "이 게임은 제작자가 두 번째로 만든 게임입니다.",
        "보정 만들다 머리 터질 뻔 했습니다. 살려줘!!!",
        "2 Mode는 에니그마를 사용하여 단어를 생성합니다.",
        "모든 플레이 데이터는 로컬에 저장됩니다. 잃어버리지 않게 주의하세요."
    };

    private int idx;

    private void Start()
    {
        StartCoroutine(ShowTip());
    }

    IEnumerator ShowTip()
    {
        idx = Random.Range(0, tips.Length);
        GetComponent<TMP_Text>().text = tips[idx];
        yield return new WaitForSeconds(4f);
        StartCoroutine(ShowTip());
    }
}
