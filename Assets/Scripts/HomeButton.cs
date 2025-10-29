using UnityEngine;

public class HomeButton : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform spawnPoint;
    public GameObject uiCanvas; // Optional - disable UI when going home
    public Camera mainCamera;   // Optional - reset camera position if needed

    public void GoHome()
    {
        if (player != null && spawnPoint != null)
        {
            player.position = spawnPoint.position;
            player.rotation = spawnPoint.rotation;
        }

        if (uiCanvas != null)
            uiCanvas.SetActive(false);

        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(player.position.x, player.position.y, mainCamera.transform.position.z);
        }

        Debug.Log("Returned to home position!");
    }
}
