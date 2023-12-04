using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoFade : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);
    }

    private void Start()
    {
        Cursor.visible = false;
        StartCoroutine(IFadeIn());
    }

    IEnumerator IFadeIn()
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            yield return new WaitForSeconds(0.0125f);
            gameObject.GetComponent<Image>().color = new Color(255, 255, 255, i);
        }
    
        StartCoroutine(NextScene());
    }
    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(1.5f);
        
        SceneManager.LoadScene("Loading");
    }
}
