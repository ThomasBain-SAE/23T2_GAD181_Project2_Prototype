using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    public GameObject projectile;
    public GameObject monsterScavengerPrefab;

    //Patroling

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("PlayerObject").transform;

        // Instantiate the prefab and store the reference to the newly created object
        //GameObject monsterObject = Instantiate(monsterScavengerPrefab, transform.position, Quaternion.identity);

        // Get the NavMeshAgent from the instantiated object
        agent = GetComponent<NavMeshAgent>();

        // You can destroy the prefab instance if you don't need it anymore
        // Destroy(monsterObject);
    }


    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Check if the enemy has reached the walk point
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        int maxAttempts = 5; // Maximum attempts to find a valid walk point
        int attempts = 0;

        do
        {
            // Calculate a random point within the specified range
            float randomX = Random.Range(-walkPointRange, walkPointRange);
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomY = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);

            // Check if the random point is on the NavMesh
            if (NavMesh.SamplePosition(walkPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                // Check if there is a valid path to the walk point
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(transform.position, walkPoint, NavMesh.AllAreas, path))
                {
                    if (path.status == NavMeshPathStatus.PathComplete)
                    {
                        walkPoint = hit.position;
                        walkPointSet = true;
                        break; // Valid walk point found, exit the loop
                    }
                }
            }

            attempts++;
        } while (attempts < maxAttempts);

        // If the maximum attempts are reached and a valid point is still not found, reset the walkPointSet flag
        if (attempts >= maxAttempts)
        {
            walkPointSet = false;
        }
    }





    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Add Attack code here: (WHATEVER IT IS MAKE SPOOKY)
           // Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
           // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
           // rb.AddForce(transform.forward * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), .5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
