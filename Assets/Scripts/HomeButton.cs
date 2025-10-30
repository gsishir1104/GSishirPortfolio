using UnityEngine;
using System.Collections;

public class HomeBtnManager : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Vector3 startPosition;
    public GameObject scanlines;

    [Header("All Panels")]
    public PanelFader[] panelFaders; // drag ALL panels that use PanelFader

    public void GoHome()
    {
        StopAllCoroutines();
        StartCoroutine(ResetPortfolio());
    }

    private IEnumerator ResetPortfolio()
    {
        // Fade out all visible panels instead of SetActive(false)
        foreach (PanelFader panel in panelFaders)
        {
            if (panel != null && panel.canvasGroup.alpha > 0)
                yield return StartCoroutine(panel.FadeOut());
        }

        // Turn off scanlines
        if (scanlines != null)
            scanlines.SetActive(false);

        // Move player to start
        if (player != null)
            player.position = startPosition;
    }
}
