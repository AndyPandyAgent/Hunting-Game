using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BossBehaviour : MonoBehaviour
{
    private GameObject player;

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


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
            startShoot = true;


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
            GameObject.FindGameObjectWithTag("WorldManager").GetComponent<S_WorldStateManager>().WinState();
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
