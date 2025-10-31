using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScaler : MonoBehaviour
{
    public float targetAspect = 16f / 9f; // Default aspect ratio
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        UpdateViewport();
    }

    void UpdateViewport()
    {
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            // Letterbox (horizontal bars)
            cam.rect = new Rect(0, (1f - scaleHeight) / 2f, 1f, scaleHeight);
        }
        else
        {
            // Pillarbox (vertical bars)
            float scaleWidth = 1f / scaleHeight;
            cam.rect = new Rect((1f - scaleWidth) / 2f, 0, scaleWidth, 1f);
        }
    }

    void OnPreCull()
    {
        GL.Clear(true, true, Color.black);
    }
}
