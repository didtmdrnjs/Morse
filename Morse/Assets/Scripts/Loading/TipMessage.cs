using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TipMessage : MonoBehaviour
{
    private string[] tips =
    {
        "�� ������ �����ڰ� �� ��°�� ���� �����Դϴ�.",
        "���� ����� �Ӹ� ���� �� �߽��ϴ�. �����!!!",
        "2 Mode�� ���ϱ׸��� ����Ͽ� �ܾ �����մϴ�.",
        "��� �÷��� �����ʹ� ���ÿ� ����˴ϴ�. �Ҿ������ �ʰ� �����ϼ���."
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
