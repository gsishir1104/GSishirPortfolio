using UnityEngine;
using System.Collections;

public class ProjectsUIManager : MonoBehaviour
{
    [Header("Panels")]
    public PanelFader projectsHubPanel;
    public PanelFader phantomEscapePanel;
    public PanelFader flappyBirdPanel;

    private void Start()
    {
        StartCoroutine(projectsHubPanel.FadeIn());
        phantomEscapePanel.canvasGroup.alpha = 0;
        flappyBirdPanel.canvasGroup.alpha = 0;
    }

    public void ShowHub()
    {
        StartCoroutine(SwitchPanel(projectsHubPanel));
    }

    public void ShowPhantomEscape()
    {
        Debug.Log("Phantom Escape button clicked!");
        StartCoroutine(SwitchPanel(phantomEscapePanel));

    }

    public void ShowFlappyBird()
    {
        Debug.Log("Flappy Bird button clicked!");
        StartCoroutine(SwitchPanel(flappyBirdPanel));
    }

    private IEnumerator SwitchPanel(PanelFader target)
    {
        // Fade out all first
        if (projectsHubPanel.canvasGroup.alpha > 0)
            yield return StartCoroutine(projectsHubPanel.FadeOut());
        if (phantomEscapePanel.canvasGroup.alpha > 0)
            yield return StartCoroutine(phantomEscapePanel.FadeOut());
        if (flappyBirdPanel.canvasGroup.alpha > 0)
            yield return StartCoroutine(flappyBirdPanel.FadeOut());

        // Then fade in the target
        yield return StartCoroutine(target.FadeIn());
    }
}
