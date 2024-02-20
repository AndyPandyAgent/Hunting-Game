using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class S_BossBehaviour : MonoBehaviour
{
    private GameObject player;
    private GameObject worldState;

    [Header("Health")]
    public float health;
    private bool hasDied = false;
    public GameObject healthObject;
    public Transform[] spawnPoints;
    private GameObject currentPoint;

    [Header("Shooting")]
    public GameObject bullet;
    [SerializeField]private float shootForce;
    [SerializeField]private float cooldown;
    [HideInInspector]public bool startShoot;
    [HideInInspector] public bool startSpawn;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        worldState = GameObject.FindGameObjectWithTag("WorldManager");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
            startShoot = true;


        if (startSpawn)
        {
            //worldState.GetComponent<S_WorldStateManager>().Spawn();
        }

        if (startShoot)
        {
            Shoot();
            startShoot = false;
        }

        if(currentPoint == null)
        {
            SpawnHealth();
        }

        if(health <= 0 && !hasDied)
        {
            worldState.GetComponent<S_WorldStateManager>().WinState();
            hasDied = true;
        }
    }

    private void Shoot()
    {
        var currentBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        currentBullet.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position) * shootForce, ForceMode.Impulse);
        Invoke("Shoot", cooldown);
    }

    private void SpawnHealth()
    {
        var spawnPoint = Instantiate(healthObject, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
        spawnPoint.transform.SetParent(transform);
        currentPoint = spawnPoint;
    }

    public void KillPoint()
    {
        Destroy(currentPoint);
        health--;
    }
}
