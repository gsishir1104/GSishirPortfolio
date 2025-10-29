using UnityEngine;
using System.Collections;

public class ComputerTerminal : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject aboutMePanel;
    public GameObject scanlines;

    [Header("Computer Settings")]
    public Animator computerAnimator;
    public float activationDelay = 1.5f; // Time before UI appears after animation

    private bool playerInRange = false;
    private bool panelOpen = false;
    private bool isActivating = false;

    void Start()
    {
        aboutMePanel.SetActive(false);
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
            // Close everything immediately
            aboutMePanel.SetActive(false);
            scanlines.SetActive(false);

            if (computerAnimator != null)
                computerAnimator.SetBool("IsActive", false);
        }
    }

    IEnumerator ActivateComputer()
    {
        isActivating = true;

        if (computerAnimator != null)
            computerAnimator.SetBool("IsActive", true);

        // Wait for the computer "turn on" animation to play
        yield return new WaitForSeconds(activationDelay);

        // Now show the UI and scanlines
        scanlines.SetActive(true);
        aboutMePanel.SetActive(true);

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
