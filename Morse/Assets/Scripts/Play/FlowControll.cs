using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowControll : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Singleton<PlayManager>.instance.isFadeOut)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
