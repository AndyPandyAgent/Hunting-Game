using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehivour : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public Transform enemy;

    public LayerMask groundMask, playerMask;


    [Header("Patrolling")]
    public Vector3 walkPoint;
    public float waitTime;
    bool walkPointSet;
    public float walkPointRange;
    public float patrolSpeed;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float range = 4000;

    [Header("Attraction")]
    public GameObject attractor;

    [Header("Run")]
    public float runLength;
    public float runSpeed;

    [Header("States")]
    public float hearRange, closeRange, runawayTime;
    public bool playerInHearRange, playerInCloseRange, isRunningAway;
    public Vector3 center;
    public Vector3 size;

    [Header("Health")]
    public float health = 100;
    public float maxHealth = 100;
    public float damageAmount = 20;
    public float score;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform attackPoint;
    public float shootForce = 300;
    public float upwardForce = 3;
    public float randomAmount = 0f;



    private void Update()
    {
        attractor = GameObject.FindGameObjectWithTag("Attractor");

        playerInHearRange = Physics.CheckSphere(transform.position, hearRange, playerMask);
        playerInCloseRange = Physics.CheckSphere(transform.position, closeRange, playerMask);

        if (!playerInHearRange && !playerInCloseRange) Patroling();
        if (playerInHearRange && !playerInCloseRange) HearPlayer();
        if (playerInHearRange && playerInCloseRange) ClosePlayer();

        if (attractor != null)
            if (attractor.GetComponent<Attractor>().inRange)
                agent.SetDestination(attractor.transform.position);

        if (isRunningAway)
            RunAway();

    }
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearhWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        agent.speed = patrolSpeed;
    }

    private void SearhWalkPoint()
    {
        float randonZ = Random.Range(-walkPointRange, walkPointRange);
        float randonX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randonX, transform.position.y, transform.position.z + randonZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
            walkPointSet = true;
    }

    private void HearPlayer()
    {
        isRunningAway = true;
    }

    private void ClosePlayer()
    {
        isRunningAway = true;
    }

    private void RunAway()
    {

        Vector3 areaCenter = transform.position + ((transform.position - player.position) * runLength);
        Vector3 randomPoint = GetRandomNavMeshPointWithinArea(areaCenter, size, groundMask);

        agent.SetDestination(randomPoint);
        Invoke("StopRunning", runawayTime);
        agent.speed = agent.speed = runSpeed;
    }
    private void StopRunning()
    {
        isRunningAway = false;
    }

    Vector3 GetRandomNavMeshPointWithinArea(Vector3 areaCenter, Vector3 areaSize, LayerMask layerMask)
    {
        NavMeshHit hit;
        Vector3 randomPoint = areaCenter + new Vector3(Random.Range(-areaSize.x / 2f, areaSize.x / 2f), 0f, Random.Range(-areaSize.z / 2f, areaSize.z / 2f));

        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, layerMask))
        {
            return hit.position;
        }

        // If no valid point found, return center of the area
        return areaCenter;
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, hearRange);
        Gizmos.DrawWireSphere(transform.position, closeRange);
    }

    public void TakeDamage(float damage)
    {
        print("TakeDamage");
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().score += score;
        Destroy(gameObject);

        if (GameObject.FindGameObjectWithTag("Pointer"))
        {
            S_Pointer pointer = GameObject.FindGameObjectWithTag("Pointer").GetComponent<S_Pointer>();
            if (pointer != null)
            {
                if (pointer.targetObj = gameObject)
                {

                    pointer.targetObj = null;
                    pointer.isTracking = false;
                }
            }
        }



    }
}
