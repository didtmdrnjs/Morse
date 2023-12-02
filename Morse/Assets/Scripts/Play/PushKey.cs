using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PushKey : MonoBehaviour
{
    [SerializeField] private AudioClip shortSound;
    [SerializeField] private AudioClip LongSound;

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
            pushTime = Singleton<PlayManager>.instance.time;
            audioSource.clip = shortSound;
            audioSource.time = 0.013f;
            audioSource.Play();
            Debug.Log("Short");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            pushTime = Singleton<PlayManager>.instance.time;
            audioSource.clip = LongSound;
            audioSource.time = 0.0472f;
            audioSource.Play();
            Debug.Log("Long");
        }
    }
}
