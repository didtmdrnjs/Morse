using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField] private GameObject leftMusic;
    [SerializeField] private GameObject centerMusic;
    [SerializeField] private GameObject rightMusic;

    [SerializeField] private TextMeshProUGUI leftLanguage;
    [SerializeField] private TextMeshProUGUI centerLanguage;
    [SerializeField] private TextMeshProUGUI rightLanguage;

    private bool isMove;
    private bool isChangeMusic;
    private int index;

    private RectTransform rect;
    private float speed;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        speed = 5;
    }

    private void Start()
    {
        index = MusicInfo.instance.currentMusicIndex;
    }

    private void Update()
    {
        Change();
        if (isChangeMusic) index = MusicInfo.instance.currentMusicIndex;
        if (MusicInfo.instance.isLoadScene || (isChangeMusic && !isMove)) ShowMusicInfo();
    }

    private void Change()
    {
        if (!isMove && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(Move(1));
            MusicInfo.instance.currentMusicIndex--;
            isChangeMusic = true;
        }
        else if (!isMove && Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(Move(-1));
            MusicInfo.instance.currentMusicIndex++;
            isChangeMusic = true;
        }
    }

    IEnumerator Move(int d)
    {
        isMove = true;
        rect.anchorMin += new Vector2(d * speed / 100f, 0);
        rect.anchorMax += new Vector2(d * speed / 100f, 0);
        if (Mathf.Abs(rect.anchorMin.x) < 1)
        {
            yield return null;
            StartCoroutine(Move(d));
        }
        else
        {
            isMove = false;
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(1, 1);
        }
    }

    public void ShowMusicInfo()
    {
        List<MusicData> datas = MusicInfo.instance.datas;
        int idx = 0;

        idx = index == 0 ? datas.Count - 1 : index - 1;
        leftMusic.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = datas[idx].name;
        leftMusic.transform.GetChild(1).GetComponent<Image>().sprite = datas[idx].image;
        leftLanguage.text = datas[idx].language;

        centerMusic.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = datas[index].name;
        centerMusic.transform.GetChild(1).GetComponent<Image>().sprite = datas[index].image;
        centerLanguage.text = datas[index].language;

        idx = index == datas.Count - 1 ? 0 : index + 1;
        rightMusic.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = datas[idx].name;
        rightMusic.transform.GetChild(1).GetComponent<Image>().sprite = datas[idx].image;
        rightLanguage.text = datas[idx].language;

        isChangeMusic = false;
        MusicInfo.instance.isLoadScene = false;
    }
}
