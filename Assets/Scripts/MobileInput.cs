using UnityEngine;

public class MobileInput : MonoBehaviour
{
    [SerializeField] public bool leftPressed;
    [SerializeField] public bool rightPressed;
    [SerializeField] public bool jumpPressed;
    [SerializeField] public bool interactPressed;

    public void LeftDown() => leftPressed = true;
    public void LeftUp() => leftPressed = false;

    public void RightDown() => rightPressed = true;
    public void RightUp() => rightPressed = false;

    public void JumpDown() => jumpPressed = true;
    public void JumpUp() => jumpPressed = false;

    public void InteractDown()
    {
        interactPressed = true;
        Debug.Log("🔵 Interact button pressed");
    }

    public void InteractUp()
    {
        interactPressed = false;
        Debug.Log("⚪ Interact button released");
    }
}
