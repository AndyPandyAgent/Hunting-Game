using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float hunger;
    public float score;
    public float hungerMultiplier;

    private void Awake()
    {
        score = 0;
        hunger = 100;
    }

    private void Update()
    {
        hunger -= Time.deltaTime * hungerMultiplier;
    }

    public void Suicide()
    {
        print("KILL YOURESeLF");
    }
}
