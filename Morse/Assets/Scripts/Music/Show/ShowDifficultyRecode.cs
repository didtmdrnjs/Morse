using UnityEngine;

public class ShowDifficultyRecode : MonoBehaviour
{
    private void Update()
    {
        ShowRecode();
    }

    private void ShowRecode()
    {
        if (MusicInfo.instance.difficulty == 0) {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
