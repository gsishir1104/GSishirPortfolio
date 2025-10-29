using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public IEnumerator FadeIn(float duration = 0.5f)
    {
        canvasGroup.gameObject.SetActive(true);
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            canvasGroup.alpha = t / duration;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    public IEnumerator FadeOut(float duration = 0.5f)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            canvasGroup.alpha = 1 - t / duration;
            yield return null;
        }
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(false);
    }
}
