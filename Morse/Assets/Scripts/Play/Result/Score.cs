using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;

    public int perfectCount;
    public int greateCount;
    public int goodCount;
    public int failCount;
    public int fullCount;

    public int maxCombo;
    public int combo;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) Destroy(gameObject);
    }

    public void SetCount()
    {
        fullCount = perfectCount + greateCount + goodCount + failCount;
    }

    public int GetScore()
    {
        float val = 1000000f / fullCount;
        float score = 0;
        score += perfectCount * val;
        score += greateCount * val * 0.65f;
        score += goodCount * val * 0.3f;
        return (int)score;
    }

    public float GetRate()
    {
        float val = (float)100 / fullCount;
        float rate = 0;
        rate += perfectCount * val;
        rate += greateCount * val * 0.7f;
        rate += goodCount * val * 0.45f;
        return rate;
    }
}
