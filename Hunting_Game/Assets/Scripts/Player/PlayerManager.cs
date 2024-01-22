using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float hunger;
    public float hungerMultiplier;

    private void Awake()
    {
        hunger = 100;
    }

    private void Update()
    {
        hunger -= Time.deltaTime * hungerMultiplier;
    }
}
