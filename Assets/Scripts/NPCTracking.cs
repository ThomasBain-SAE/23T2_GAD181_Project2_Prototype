using UnityEngine;
using UnityEngine.AI;

public class NPCTracking : MonoBehaviour
{
    public Transform player;  // Assign the player GameObject to this field in the Unity Inspector
    public float lightDetectionDistance = 5.0f;
    public LayerMask lightSourceLayer; // Set this in the Inspector to the layer where your light sources are.

    private NavMeshAgent agent;
    private bool inLightArea = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction from the NPC to the player
            Vector3 directionToPlayer = player.position - transform.position;

            // Check for light sources in the vicinity
            Collider[] lightSources = Physics.OverlapSphere(transform.position, lightDetectionDistance, lightSourceLayer);

            // If there are light sources nearby, avoid them
            if (lightSources.Length > 0)
            {
                inLightArea = true;
                if (agent.isOnNavMesh) // Check if the agent is on the NavMesh
                {
                    agent.SetDestination(transform.position); // Stop the agent's movement
                }
            }
            else if (!inLightArea)
            {
                // No light sources nearby and not currently in a light area, so move toward the player
                if (agent.isOnNavMesh) // Check if the agent is on the NavMesh
                {
                    agent.SetDestination(player.position);
                }
            }
        }
    }
}
