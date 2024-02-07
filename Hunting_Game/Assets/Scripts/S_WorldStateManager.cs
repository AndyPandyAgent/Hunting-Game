using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class S_WorldStateManager : MonoBehaviour
{
    public PlayerCam playerCam;
    private GunScript gunScript;
    public Light light;
    public Camera fpsCam;

    private Color ogColor;
    public GameObject god;
    public Transform startPos;
    public Transform endPos;

    public bool chaosState;
    public float animTime = 2;
    public int cost = 1;

    private void Awake()
    {
        ogColor = light.color;
        gunScript = GameObject.FindGameObjectWithTag("Rifle").GetComponent<GunScript>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
            chaosState = !chaosState;
        if (!chaosState)
        {
            NormalState();
        }
        else
        {
            ChaosState();
        }
    }

    private void ChaosState()
    {
        light.color = Color.red;
        god.transform.position = Vector3.Lerp(god.transform.position, endPos.position, animTime * Time.deltaTime);
        playerCam.enabled = false;
        gunScript.enabled = false;
        fpsCam.transform.LookAt(god.transform);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void NormalState()
    {
        light.color = ogColor;
        god.transform.position = Vector3.Lerp(god.transform.position, startPos.position, animTime * Time.deltaTime);
        playerCam.enabled = true;
        gunScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
