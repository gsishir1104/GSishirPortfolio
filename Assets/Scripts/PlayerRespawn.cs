using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Transform respawnPoint;   // The starting point
    public float fallThreshold = -10f; // Y position below which player respawns
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // If no respawn point is set, use current position
        if (respawnPoint == null)
        {
            GameObject start = GameObject.Find("PlayerSpawnPoint");
            if (start != null)
                respawnPoint = start.transform;
            else
                respawnPoint = transform;
        }
    }

    void Update()
    {
        // If player falls below the threshold
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // Reset player position and velocity
        transform.position = respawnPoint.position;
        rb.linearVelocity = Vector2.zero;

        Debug.Log("Player respawned at starting point!");
    }
}
