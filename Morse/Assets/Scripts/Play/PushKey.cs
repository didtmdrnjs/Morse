using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PushKey : MonoBehaviour
{
    private float pushTime;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            
        }
    }
}
