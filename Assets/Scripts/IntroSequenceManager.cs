using UnityEngine;

public class IntroSequenceManager : MonoBehaviour
{
    [Header("Intro Panels")]
    public GameObject introPanel1;
    public GameObject introPanel2;

    [Header("UI Elements")]
    public GameObject controlsOverlay; // New overlay that shows control instructions

    void Start()
    {
        introPanel1.SetActive(true);
        introPanel2.SetActive(false);
        controlsOverlay.SetActive(false);
    }

    public void OnContinuePressed()
    {
        // Move from first to second intro message
        introPanel1.SetActive(false);
        introPanel2.SetActive(true);
    }

    public void OnFinalContinuePressed()
    {
        // Show the controls tutorial after the second intro
        introPanel2.SetActive(false);
        controlsOverlay.SetActive(true);
    }
}
