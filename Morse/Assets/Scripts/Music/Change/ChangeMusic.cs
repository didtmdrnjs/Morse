using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    private RectTransform rect;
    private float speed;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        speed = 5;
    }

    private void Update()
    {
        Change();
    }

    private void Change()
    {
        if (!Singleton<MusicInfo>.instance.isMove && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(Move(1));

            MusicInfo musicInfo = Singleton<MusicInfo>.instance;
            if (musicInfo.currentMusicIndex == 0) musicInfo.currentMusicIndex = musicInfo.musicList.datas.Count - 1;
            else musicInfo.currentMusicIndex--;

            musicInfo.isChangeMusic = true;
        }
        else if (!Singleton<MusicInfo>.instance.isMove && Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(Move(-1));

            MusicInfo musicInfo = Singleton<MusicInfo>.instance;
            if (musicInfo.currentMusicIndex == musicInfo.musicList.datas.Count - 1) musicInfo.currentMusicIndex = 0;
            else musicInfo.currentMusicIndex++;

            musicInfo.isChangeMusic = true;
        }
    }

    IEnumerator Move(int d)
    {
        Singleton<MusicInfo>.instance.isMove = true;
        rect.anchorMin += new Vector2(d * speed / 100f, 0);
        rect.anchorMax += new Vector2(d * speed / 100f, 0);
        if (Mathf.Abs(rect.anchorMin.x) < 1)
        {
            yield return null;
            StartCoroutine(Move(d));
        }
        else
        {
            Singleton<MusicInfo>.instance.isMove = false;
            yield return new WaitForSeconds(0.001f);
            rect.anchorMin = new Vector2(0, 0.085f);
            rect.anchorMax = new Vector2(1, 0.875f);
        }
    }
}
