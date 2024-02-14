using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BossBehaviour : MonoBehaviour
{
    private GameObject player;

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
    }

    private void Shoot()
    {
        var currentBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        currentBullet.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position) * shootForce, ForceMode.Impulse);
        Invoke("Shoot", cooldown);
    }
}
