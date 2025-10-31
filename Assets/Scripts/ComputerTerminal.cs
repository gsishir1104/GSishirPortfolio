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
    private Collider2D triggerCollider;

    void Awake()
    {
        triggerCollider = GetComponent<Collider2D>();
    }

    void Start()
    {
        if (aboutMePanel != null)
            aboutMePanel.SetActive(false);

        if (scanlines != null)
            scanlines.SetActive(false);

        // Keep prompt visible initially
        if (interactionPrompt != null)
            interactionPrompt.SetActive(true);

        if (computerAnimator != null)
            computerAnimator.SetBool("IsActive", false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isActivating)
        {
            Debug.Log("🟢 E key pressed near computer: toggling...");
            ToggleComputer();
        }
    }

    public void ToggleComputer()
    {
        // Prevent multiple clicks or re-entry during transitions
        if (isActivating)
            return;

        panelOpen = !panelOpen;

        if (panelOpen)
            StartCoroutine(ActivateComputer());
        else
            StartCoroutine(DeactivateComputerRoutine());
    }

    IEnumerator ActivateComputer()
    {
        isActivating = true;

        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);

        if (computerAnimator != null)
            computerAnimator.SetBool("IsActive", true);

        yield return new WaitForSeconds(activationDelay);

        if (scanlines != null)
            scanlines.SetActive(true);

        // Open correct panel
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

    IEnumerator DeactivateComputerRoutine()
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

        if (scanlines != null)
            scanlines.SetActive(false);

        if (computerAnimator != null)
            computerAnimator.SetBool("IsActive", false);

        if (interactionPrompt != null)
            interactionPrompt.SetActive(true);

        // Safety: small cooldown before allowing reactivation
        yield return new WaitForSeconds(0.5f);
        isActivating = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (interactionPrompt != null && !panelOpen)
                interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            // Don’t toggle immediately — run coroutine safely via Canvas (still active)
            if (panelOpen)
                StartCoroutine(SafeCloseAfterExit());
        }
    }

    IEnumerator SafeCloseAfterExit()
    {
        // Wait one frame to ensure the GameObject stays active for the coroutine start
        yield return null;

        // Then safely close
        if (gameObject.activeInHierarchy)
            yield return StartCoroutine(DeactivateComputerRoutine());
    }
}
