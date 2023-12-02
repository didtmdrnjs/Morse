using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    private int count;

    private void OnEnable()
    {
        count = 3;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        if (count == 0)
        {
            Singleton<PlayManager>.instance.isCountdown = true;
            gameObject.SetActive(false);
        }

        gameObject.GetComponent<TextMeshProUGUI>().text = count.ToString();
        
        yield return new WaitForSeconds(1);

        count--;
        StartCoroutine(Countdown());
    }
}
