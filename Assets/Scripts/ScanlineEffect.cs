using UnityEngine;
using UnityEngine.UI;

public class ScanlineEffect : MonoBehaviour
{
    public Material scanlineMaterial;
    public float speed = 0.1f;

    void Update()
    {
        if (scanlineMaterial != null)
        {
            float offset = Mathf.Repeat(Time.time * speed, 1f);
            scanlineMaterial.SetTextureOffset("_MainTex", new Vector2(0, offset));
        }
    }
}
