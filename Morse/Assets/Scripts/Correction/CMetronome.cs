using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMetronome : Singleton<CMetronome>
{
    public bool isEndGuide;

    private float time;

    private void Update()
    {
        if (isEndGuide) time += Time.deltaTime;

        metronome();
    }

    private void metronome()
    {
        if (time == 1)
        {

        }
    }
}
