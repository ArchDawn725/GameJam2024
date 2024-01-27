using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ImageFlasher : MonoBehaviour
{

    public Image target;
    public Color startColor = Color.white;
    public float fadeTime = 1f;

    void Start()
    {
        FlashAndFade();
    }

    public void FlashAndFade()
    {
        if(target == null)
        {
            Debug.LogError("No target image set for ImageFlasher");
            return;
        }
        target.color = startColor;
        StartCoroutine(FadeImage());
    }

    IEnumerator FadeImage()
    {
        float elapsedTime = 0f;
        while(elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            target.color = Color.Lerp(Color.white, Color.clear, elapsedTime / fadeTime);
            yield return null;
        }
    }
}
