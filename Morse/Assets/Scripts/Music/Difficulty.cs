using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    public GameObject leftEasy;
    public GameObject leftHard;
    public GameObject centerEasy;
    public GameObject centerHard;
    public GameObject rightEasy;
    public GameObject rightHard;

    public GameObject leftDifficulty;
    public GameObject centerDifficulty;
    public GameObject rightDifficulty;

    public GameObject menu;

    private void Start()
    {
        if (GameManager.instance.difficulty == 1)
        {
            SetDifficultyHard(leftEasy, leftHard, leftDifficulty);
            SetDifficultyHard(centerEasy, centerHard, centerDifficulty);
            SetDifficultyHard(rightEasy, rightHard, rightDifficulty);
        }
        else
        {
            SetDifficultyEasy(leftEasy, leftHard, leftDifficulty);
            SetDifficultyEasy(centerEasy, centerHard, centerDifficulty);
            SetDifficultyEasy(rightEasy, rightHard, rightDifficulty);
        }
    }

    private void Update()
    {
        if (!menu.activeSelf) Change();
    }

    private void Change()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            GameManager.instance.difficulty = 0;
            SetDifficultyEasy(leftEasy, leftHard, leftDifficulty);
            SetDifficultyEasy(centerEasy, centerHard, centerDifficulty);
            SetDifficultyEasy(rightEasy, rightHard, rightDifficulty);
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            GameManager.instance.difficulty = 1;
            SetDifficultyHard(leftEasy, leftHard, leftDifficulty);
            SetDifficultyHard(centerEasy, centerHard, centerDifficulty);
            SetDifficultyHard(rightEasy, rightHard, rightDifficulty);
        }
    }

    private void SetDifficultyEasy(GameObject _easy, GameObject _hard, GameObject _difficulty)
    {
        _easy.GetComponent<Image>().color = new Color(213 / 255f, 255 / 255f, 172 / 255f, 1f);
        _easy.GetComponentInChildren<TMP_Text>().color = Color.green;
        _hard.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.1f);
        _hard.GetComponentInChildren<TMP_Text>().color = new Color(1f, 1f, 1f, 0.1f);
        _difficulty.GetComponent<TMP_Text>().text = GameManager.instance.difficulty == 0 ? "Easy" : "Hard";
    }

    private void SetDifficultyHard(GameObject _easy, GameObject _hard, GameObject _difficulty)
    {
        _easy.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.1f);
        _easy.GetComponentInChildren<TMP_Text>().color = new Color(1f, 1f, 1f, 0.1f);
        _hard.GetComponent<Image>().color = new Color(255 / 255f, 192 / 255f, 192 / 255f, 1);
        _hard.GetComponentInChildren<TMP_Text>().color = Color.red;
        _difficulty.GetComponent<TMP_Text>().text = GameManager.instance.difficulty == 0 ? "Easy" : "Hard";
    }
}