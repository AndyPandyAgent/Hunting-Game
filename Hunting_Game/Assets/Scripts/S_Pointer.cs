using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Pointer : MonoBehaviour
{
    public GameObject targetObj;
    [HideInInspector]public bool isTracking;

    private void Awake()
    {
        isTracking = false;
    }
    private void Update()
    {
        if(targetObj != null)
            transform.LookAt(targetObj.transform.position);

        if (!isTracking)
        {
            targetObj = GameObject.FindGameObjectWithTag("AttackPoint");
        }

    }

    public void GetTarget(GameObject owner)
    {
        targetObj = owner;
        isTracking = true;
    }
}
