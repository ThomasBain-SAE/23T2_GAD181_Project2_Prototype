using UnityEngine;

public class NPCAvoidLight : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Rigidbody rb;
    private bool inLightArea = false;

    public Transform player;  // Assign the player GameObject to this field in the Unity Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction from the NPC to the player
            Vector3 directionToPlayer = player.position - transform.position;

            if (!inLightArea)
            {
                // Move the NPC toward the player
                rb.velocity = directionToPlayer.normalized * moveSpeed;
            }
            else
            {
                // Avoid light sources by stopping the NPC's movement
                rb.velocity = Vector3.zero;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightSource"))
        {
            inLightArea = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightSource"))
        {
            inLightArea = false;
        }
    }
}

