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

    [Header("Attacking")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float range = 4000;

    [Header("Attraction")]
    public GameObject attractor;

    [Header("Run")]
    public float runLength;

    [Header("States")]
    public float hearRange, closeRange;
    public bool playerInHearRange, playerInCloseRange;

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
        if(player.GetComponent<PlayerMovment>().state == PlayerMovment.MovementState.sprinting)
        {
            RunAway();
        }

    }

    private void ClosePlayer()
    {
        if (player.GetComponent<PlayerMovment>().state == PlayerMovment.MovementState.walking)
        {
            RunAway();
        }
    }

    private void RunAway()
    {
        Vector3 runTo = transform.position + ((transform.position - player.position) * runLength);
        agent.SetDestination(runTo);
    }

    /*private void Shoot()
    {
        RaycastHit hit;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        shootLight.gameObject.SetActive(true);
        Invoke("TurnOffLight", 0.05f);

        Instantiate(smoke, new Vector3(attackPoint.position.x, attackPoint.position.y, attackPoint.position.z), transform.rotation);

        Vector3 directionWithRandom = directionToPlayer + new Vector3(Random.Range(randomAmount, randomAmount), Random.Range(randomAmount, randomAmount), Random.Range(randomAmount, randomAmount));

        if (Physics.Raycast(attackPoint.transform.position, directionToPlayer, out hit, range))
        {

            PlayerManager playerManager = hit.transform.GetComponent<PlayerManager>();


            if (playerManager != null)
            {
                //playerManager.TakeDamage(damageAmount);
            }

        }


        Debug.DrawRay(transform.position, transform.forward, Color.yellow);
    }*/

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
    }

    /*private void TurnOffLight()
    {
        shootLight.gameObject.SetActive(false);
    }*/
}
