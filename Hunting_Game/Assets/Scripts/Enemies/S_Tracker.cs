using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_Tracker : MonoBehaviour
{
    public GameObject pointer;
    public GameObject owner;

    private void Awake()
    {
        pointer = GameObject.FindGameObjectWithTag("Pointer");
        owner = transform.parent.GameObject();
        Invoke("AbortChild", 0.001f);
    }

    private void AbortChild()
    {
        transform.parent = null;
    }

    private void OnTriggerEnter(Collider other)
    {

        pointer.GetComponent<S_Pointer>().GetTarget(owner);
    }
}
