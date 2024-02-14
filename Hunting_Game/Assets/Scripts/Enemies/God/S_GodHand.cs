using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GodHand : MonoBehaviour
{
    private GameObject player;
    public float giveSpeed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, giveSpeed * Time.deltaTime);
    }
}
