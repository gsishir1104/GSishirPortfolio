using UnityEngine;

public class ButtonLinkOpener : MonoBehaviour
{
    // Opens the given link in the system’s default browser
    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }
}
