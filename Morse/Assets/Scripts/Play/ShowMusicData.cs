using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowMusicData : MonoBehaviour
{
    private void Start()
    {
        MusicData data = MusicInfo.instance.datas[MusicInfo.instance.currentMusicIndex];

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = data.name;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "#" + data.bpm + "BPM";
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameManager.instance.mode == EMode.OneWord ? "#OneWord" : "#TwoWord";
        transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = GameManager.instance.difficulty == 0 ? "#Easy" : "#Hard";
        transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "#" + data.language;
        
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2);

        for (float i = 1; i > 0; i -= 0.01f)
        {
            yield return new WaitForSeconds(0.01f);

            gameObject.GetComponent<Image>().color = new Color(255, 255, 255, i);
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, i);
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, i);
            transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, i);
            transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, i);
            transform.GetChild(4).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, i);
        }

        yield return new WaitForSeconds(0.5f);

        PlayManager.instance.isFadeOut = true;
        gameObject.SetActive(false);
    }
}
