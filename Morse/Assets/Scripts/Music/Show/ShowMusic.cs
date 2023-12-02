using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowMusic : MonoBehaviour
{
    [SerializeField] private GameObject leftMusic;
    [SerializeField] private GameObject centerMusic;
    [SerializeField] private GameObject rightMusic;

    [SerializeField] private TextMeshProUGUI leftLanguage;
    [SerializeField] private TextMeshProUGUI centerLanguage;
    [SerializeField] private TextMeshProUGUI rightLanguage;

    private MusicInfo musicInfo;

    private int index;

    private void Start()
    {
        musicInfo = Singleton<MusicInfo>.instance;
        index = musicInfo.currentMusicIndex;
    }

    private void Update()
    {
        if (musicInfo.isChangeMusic) index = musicInfo.currentMusicIndex;

        if (musicInfo.isLoadScene || (musicInfo.isChangeMusic && !musicInfo.isMove)) ShowMusicInfo();
    }

    public void ShowMusicInfo()
    {
        MusicList musicList = musicInfo.musicList;

        if (index == 0)
        {
            int idx = musicInfo.musicList.datas.Count - 1;
            
            leftMusic.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = musicList.datas[idx].name;
            leftMusic.transform.GetChild(1).GetComponent<Image>().sprite = musicList.datas[idx].image;

            leftLanguage.text = musicList.datas[idx].language;
        }
        else
        {
            leftMusic.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = musicList.datas[index - 1].name;
            leftMusic.transform.GetChild(1).GetComponent<Image>().sprite = musicList.datas[index - 1].image;

            leftLanguage.text = musicList.datas[index - 1].language;
        }

        centerMusic.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = musicList.datas[index].name;
        centerMusic.transform.GetChild(1).GetComponent<Image>().sprite = musicList.datas[index].image;

        centerLanguage.text = musicList.datas[index].language;

        if (index == musicInfo.musicList.datas.Count - 1)
        {
            rightMusic.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = musicList.datas[0].name;
            rightMusic.transform.GetChild(1).GetComponent<Image>().sprite = musicList.datas[0].image;

            rightLanguage.text = musicList.datas[0].language;
        }
        else
        {
            rightMusic.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = musicList.datas[index + 1].name;
            rightMusic.transform.GetChild(1).GetComponent<Image>().sprite = musicList.datas[index + 1].image;

            rightLanguage.text = musicList.datas[index + 1].language;
        }

        musicInfo.isChangeMusic = false;
        musicInfo.isLoadScene = false;
    }
}
