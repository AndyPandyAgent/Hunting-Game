using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class S_WorldStateManager : MonoBehaviour
{
    public PlayerCam playerCam;
    private GunScript gunScript;
    public Light light;
    public Camera fpsCam;
    public GameObject camHolder;
    public Transform player;

    private Color ogColor;
    public GameObject god;
    public Transform startPos;
    public Transform endPos;

    public bool chaosCurrentState = false;
    public bool chaosState;
    public bool normalState;
    public bool startBoss;
    public bool isInPos;
    public float animTime = 4;
    public int cost = 1;
    public Material chaosMat;
    private Material normalMat;

    public float timer;
    [HideInInspector]public float startTimer;

    [Header("CamShake")]
    [SerializeField]private float shake;
    [SerializeField] private float shakeAmount;
    [SerializeField] private float decreseAmount;
    [SerializeField] private float freq = 25;

    [Header("Spawner")]
    public GameObject enemy;
    public Bounds bounds;
    public float spawnRange;
    public LayerMask groundLayer;
    public float spawnFrequency;
    public int enemiesToSpawn;
    public GameObject latestEnemy;
    private List<GameObject> spawnedEnemies;

    private bool hasStartedShooting;

    private void Awake()
    {
        normalState = true;
        ogColor = light.color;
        gunScript = GameObject.FindGameObjectWithTag("Rifle").GetComponent<GunScript>();
        startTimer = timer;
        hasStartedShooting = false;

        normalMat = RenderSettings.skybox;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            chaosState = true;

        if(Input.GetKeyDown(KeyCode.T))
            chaosState = !chaosState;
        if (chaosState)
        {
            ChaosState();
        }

        else if (normalState)
        {
            NormalState();
        }
        else if (startBoss)
        {
            BossState();
            StartCoroutine(Spawner(enemiesToSpawn, spawnFrequency));
            startBoss = false;
        }

        if(shake > 0)
        {

            camHolder.transform.localPosition = Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreseAmount;

            fpsCam.transform.LookAt(camHolder.transform.position);

            shake -= Time.deltaTime * decreseAmount;
        }
        else
        {
            // Ensure the camera returns to its original rotation when shaking stops
            shake = 0f; // Ensure shakeTimer doesn't go negative
        }

        if(Vector3.Distance(god.transform.position, endPos.transform.position) < 5)
        {
            isInPos = true;
        }
        else
        {
            isInPos = false;
        }

        bounds.center = player.transform.position;

        if (spawnedEnemies.Count >= enemiesToSpawn && !hasStartedShooting)
        {
            GameObject.FindGameObjectWithTag("God").GetComponent<S_BossBehaviour>().startShoot = true;
            hasStartedShooting = true;
        }
    }

    private void ChaosState()
    {
        if(shake <= 0 && !chaosCurrentState)
            shake = 6;
        chaosCurrentState = true;
        light.color = Color.red;
        RenderSettings.skybox = chaosMat;
        god.transform.position = Vector3.Lerp(god.transform.position, endPos.position, animTime * Time.deltaTime);
        playerCam.enabled = false;
        gunScript.enabled = false;
        fpsCam.transform.LookAt(god.transform);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void NormalState()
    {
        chaosCurrentState = false;
        light.color = ogColor;
        RenderSettings.skybox = normalMat;
        god.transform.position = Vector3.Lerp(god.transform.position, startPos.position, animTime * Time.deltaTime);
        playerCam.enabled = true;
        gunScript.enabled = true;
    }

    private void BossState()
    {
        chaosState = false;
        light.color = Color.red;
        god.transform.position = Vector3.Lerp(god.transform.position, endPos.position, animTime * Time.deltaTime);
        playerCam.enabled = true;
        gunScript.enabled = true;
        if (isInPos)
        {
            god.GetComponent<S_BossBehaviour>().startSpawn = true;
        }
    }

    public bool IsInsideBounds(Vector3 pos)
    {
        return bounds.Contains(pos);
    }

    public void Spawn()
    {
        Vector3 randomOffset = Random.insideUnitCircle * spawnRange;
        Vector3 randomPos = player.transform.position + randomOffset + new Vector3(0,1000,0);


        RaycastHit hit;
        if (Physics.Raycast(randomPos, Vector3.down, out hit, Mathf.Infinity, groundLayer) || Physics.Raycast(randomPos, Vector3.down *-1, out hit, Mathf.Infinity, groundLayer))
        {
            randomPos = hit.point;
        }
        GameObject enem = Instantiate(enemy, randomPos, Quaternion.identity);
        spawnedEnemies.Add(enem);
    }

    public void WinState()
    {
        print("You win");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(bounds.center, bounds.size);
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }

    IEnumerator Spawner(int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            Spawn();
            yield return new WaitForSeconds(delay);
        }
    }
}
