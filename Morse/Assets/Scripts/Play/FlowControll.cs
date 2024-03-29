using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowControll : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
    }

    private void Update()
    {
        if (PlayManager.instance.isFadeOut) transform.GetChild(1).gameObject.SetActive(true);
    }
}
