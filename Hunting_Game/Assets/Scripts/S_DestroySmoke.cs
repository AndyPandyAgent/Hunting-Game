using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DestroySmoke : MonoBehaviour
{
    private void Awake()
    {
        Invoke("DestroySelf", 3);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
