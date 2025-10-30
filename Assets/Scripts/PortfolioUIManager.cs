using UnityEngine;

public class PortfolioUIManager : MonoBehaviour
{
    public static PortfolioUIManager Instance;

    [Header("UI Panels")]
    public GameObject aboutMePanel;
    public GameObject projectsHubPanel;
    public GameObject phantomEscapePanel;
    public GameObject flappyBirdPanel;
    public GameObject certificationsPanel;
    public GameObject contactPanel;
    public GameObject instructionPanel;
    public GameObject scanlines;

    [Header("Intro Panels")]
    public GameObject introPopup1;
    public GameObject introPopup2;


    [Header("Player Settings")]
    public Transform player;
    public Vector3 startPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        HideAllPanels();

        // Start intro automatically
        if (introPopup1 != null)
        {
            introPopup1.SetActive(true);
        }
        else if (instructionPanel != null)
        {
            instructionPanel.SetActive(true);
        }
    }

    public void ShowPanel(GameObject panel)
    {
        HideAllPanels();
        if (panel != null)
        {
            panel.SetActive(true);
            if (scanlines != null)
                scanlines.SetActive(true);
        }
    }

    public void ContinueFromIntro1()
    {
        if (introPopup1 != null)
            introPopup1.SetActive(false);

        if (introPopup2 != null)
            introPopup2.SetActive(true);
    }

    public void ContinueFromIntro2()
    {
        if (introPopup2 != null)
            introPopup2.SetActive(false);

        if (instructionPanel != null)
            instructionPanel.SetActive(true);
    }


    public void HideAllPanels()
    {
        if (aboutMePanel) aboutMePanel.SetActive(false);
        if (projectsHubPanel) projectsHubPanel.SetActive(false);
        if (phantomEscapePanel) phantomEscapePanel.SetActive(false);
        if (flappyBirdPanel) flappyBirdPanel.SetActive(false);
        if (certificationsPanel) certificationsPanel.SetActive(false);
        if (contactPanel) contactPanel.SetActive(false);
        if (instructionPanel) instructionPanel.SetActive(false);
        if (scanlines) scanlines.SetActive(false);
        if (introPopup1) introPopup1.SetActive(false);
        if (introPopup2) introPopup2.SetActive(false);
    }

    public void GoHome()
    {
        HideAllPanels();

        // Move player back to starting point
        if (player != null)
            player.position = startPosition;

        // Reactivate instructions
        if (instructionPanel != null)
            instructionPanel.SetActive(true);
    }
}
