using UnityEngine;
using System.Collections;

public class ContactUIManager : MonoBehaviour
{
    [Header("Panels")]
    public PanelFader contactPanel;

    private void Start()
    {
        contactPanel.canvasGroup.alpha = 0;
        contactPanel.canvasGroup.gameObject.SetActive(false);
    }

    public void OpenContact()
    {
        contactPanel.canvasGroup.gameObject.SetActive(true);
        StartCoroutine(contactPanel.FadeIn());
    }

    public IEnumerator CloseContact()
    {
        if (contactPanel != null)
        {
            yield return StartCoroutine(contactPanel.FadeOut());
            contactPanel.canvasGroup.gameObject.SetActive(false);
        }
    }
}
