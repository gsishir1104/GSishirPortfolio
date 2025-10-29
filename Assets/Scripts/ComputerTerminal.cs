using UnityEngine;
using System.Collections;

public class ComputerTerminal : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject aboutMePanel;
    public GameObject scanlines;

    [Header("Projects Panel System")]
    public ProjectsUIManager projectsUIManager; // Assigned only for Projects Computer

    [Header("Certifications Panel System")]
    public CertificationsUIManager certificationsUIManager; // Assigned only for Certifications Computer

    [Header("Contact Panel System")]
    public ContactUIManager contactUIManager; // Assigned only for Contact Computer


    [Header("Computer Settings")]
    public Animator computerAnimator;
    public float activationDelay = 1.5f; // Time before UI appears after animation

    private bool playerInRange = false;
    private bool panelOpen = false;
    private bool isActivating = false;

    void Start()
    {
        if (aboutMePanel != null)
            aboutMePanel.SetActive(false);

        if (scanlines != null)
            scanlines.SetActive(false);

        if (computerAnimator != null)
            computerAnimator.SetBool("IsActive", false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isActivating)
        {
            ToggleComputer();
        }
    }

    void ToggleComputer()
    {
        panelOpen = !panelOpen;

        if (panelOpen)
        {
            StartCoroutine(ActivateComputer());
        }
        else
        {
            StartCoroutine(DeactivateComputer());
        }
    }

    IEnumerator ActivateComputer()
    {
        isActivating = true;

        if (computerAnimator != null)
            computerAnimator.SetBool("IsActive", true);

        yield return new WaitForSeconds(activationDelay);

        if (scanlines != null)
            scanlines.SetActive(true);

        // 👇 Decide which UI panel to show
        if (projectsUIManager != null)
        {
            Debug.Log("Opening Projects Hub...");
            projectsUIManager.OpenProjectsHub();
        }
        else if (certificationsUIManager != null)
        {
            Debug.Log("Opening Certifications Panel...");
            certificationsUIManager.OpenCertifications();
        }
        else if (contactUIManager != null)
        {
            Debug.Log("Opening Contact Me Panel...");
            contactUIManager.OpenContact();
        }
        else if (aboutMePanel != null)
        {
            aboutMePanel.SetActive(true);
        }

        isActivating = false;
    }

    IEnumerator DeactivateComputer()
    {
        isActivating = true;

        if (projectsUIManager != null)
        {
            Debug.Log("Closing Projects Hub...");
            yield return StartCoroutine(projectsUIManager.CloseProjectsHub());
        }
        else if (certificationsUIManager != null)
        {
            Debug.Log("Closing Certifications Panel...");
            yield return StartCoroutine(certificationsUIManager.CloseCertifications());
        }
        else if (contactUIManager != null)
        {
            Debug.Log("Closing Contact Me Panel...");
            yield return StartCoroutine(contactUIManager.CloseContact());
        }
        else if (aboutMePanel != null)
        {
            aboutMePanel.SetActive(false);
        }

        if (scanlines != null)
            scanlines.SetActive(false);

        if (computerAnimator != null)
            computerAnimator.SetBool("IsActive", false);

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
            if (panelOpen)
                ToggleComputer();
        }
    }
}
