using UnityEngine;
using System.Collections;

public class ComputerTerminal : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject aboutMePanel;
    public GameObject scanlines;
    public GameObject interactionPrompt; // 👈 Assign your “Press E” image here

    [Header("Projects Panel System")]
    public ProjectsUIManager projectsUIManager;

    [Header("Certifications Panel System")]
    public CertificationsUIManager certificationsUIManager;

    [Header("Contact Panel System")]
    public ContactUIManager contactUIManager;

    [Header("Computer Settings")]
    public Animator computerAnimator;
    public float activationDelay = 1.5f;

    private bool playerInRange = false;
    private bool panelOpen = false;
    private bool isActivating = false;

    void Start()
    {
        if (aboutMePanel != null)
            aboutMePanel.SetActive(false);

        if (scanlines != null)
            scanlines.SetActive(false);

        // 👇 Keep the prompt always visible at start
        if (interactionPrompt != null)
            interactionPrompt.SetActive(true);

        if (computerAnimator != null)
            computerAnimator.SetBool("IsActive", false);
    }

    void Update()
    {
        // Only allow interaction when player is close
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isActivating)
        {
            ToggleComputer();
        }
    }

    void ToggleComputer()
    {
        panelOpen = !panelOpen;

        if (panelOpen)
            StartCoroutine(ActivateComputer());
        else
            StartCoroutine(DeactivateComputer());
    }

    IEnumerator ActivateComputer()
    {
        isActivating = true;

        // Hide prompt when opening
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);

        if (computerAnimator != null)
            computerAnimator.SetBool("IsActive", true);

        yield return new WaitForSeconds(activationDelay);

        if (scanlines != null)
            scanlines.SetActive(true);

        // Open the correct panel
        if (projectsUIManager != null)
            projectsUIManager.OpenProjectsHub();
        else if (certificationsUIManager != null)
            certificationsUIManager.OpenCertifications();
        else if (contactUIManager != null)
            contactUIManager.OpenContact();
        else if (aboutMePanel != null)
            aboutMePanel.SetActive(true);

        isActivating = false;
    }

    IEnumerator DeactivateComputer()
    {
        isActivating = true;

        // Close correct panel
        if (projectsUIManager != null)
            yield return StartCoroutine(projectsUIManager.CloseProjectsHub());
        else if (certificationsUIManager != null)
            yield return StartCoroutine(certificationsUIManager.CloseCertifications());
        else if (contactUIManager != null)
            yield return StartCoroutine(contactUIManager.CloseContact());
        else if (aboutMePanel != null)
            aboutMePanel.SetActive(false);

        // Turn off visuals
        if (scanlines != null)
            scanlines.SetActive(false);

        if (computerAnimator != null)
            computerAnimator.SetBool("IsActive", false);

        // 👇 Show the prompt again after closing
        if (interactionPrompt != null)
            interactionPrompt.SetActive(true);

        isActivating = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            // If player walks away while panel open, close it automatically
            if (panelOpen)
                ToggleComputer();
        }
    }
}
