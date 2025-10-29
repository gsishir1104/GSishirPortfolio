using UnityEngine;
using System.Collections;

public class CertificationsUIManager : MonoBehaviour
{
    [Header("Panels")]
    public PanelFader certificationsPanel;

    private void Start()
    {
        certificationsPanel.canvasGroup.alpha = 0;
        certificationsPanel.canvasGroup.gameObject.SetActive(false);
    }

    public void OpenCertifications()
    {
        certificationsPanel.canvasGroup.gameObject.SetActive(true);
        StartCoroutine(certificationsPanel.FadeIn());
    }

    public IEnumerator CloseCertifications()
    {
        if (certificationsPanel != null)
        {
            yield return StartCoroutine(certificationsPanel.FadeOut());
            certificationsPanel.canvasGroup.gameObject.SetActive(false);
        }
    }
}
