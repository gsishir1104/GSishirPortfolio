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
        // Hide all panels at start
        projectsHubPanel.canvasGroup.alpha = 0;
        projectsHubPanel.canvasGroup.gameObject.SetActive(false);

        phantomEscapePanel.canvasGroup.alpha = 0;
        phantomEscapePanel.canvasGroup.gameObject.SetActive(false);

        flappyBirdPanel.canvasGroup.alpha = 0;
        flappyBirdPanel.canvasGroup.gameObject.SetActive(false);
    }

    public void OpenProjectsHub()
    {
        StartCoroutine(projectsHubPanel.FadeIn());
    }

    public IEnumerator CloseProjectsHub()
    {
        if (projectsHubPanel != null)
        {
            yield return StartCoroutine(projectsHubPanel.FadeOut());
            projectsHubPanel.canvasGroup.gameObject.SetActive(false);
        }
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
