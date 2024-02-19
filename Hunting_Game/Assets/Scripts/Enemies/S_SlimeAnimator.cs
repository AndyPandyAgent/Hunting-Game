using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_SlimeAnimator : MonoBehaviour
{
    public GameObject slime;
    private bool isSmall;
    public bool isDead;

    private void Awake()
    {
        isDead = false;
        ChangeSmall();
    }

    private void Update()
    {
        if (!isDead)
        {
            float targetScale = isSmall ? 0.5f : 1f;
            slime.transform.localScale = Vector3.Lerp(slime.transform.localScale, new Vector3(1, targetScale, 1), Time.deltaTime * (1 / 1));
        }
        if (isDead)
        {
            slime.transform.localScale = Vector3.Lerp(slime.transform.localScale, new Vector3(3, 0.1f, 3), Time.deltaTime * (1 / 1));
        }

        if (slime.transform.localScale.z >= 2.8f)
        {
            if(GetComponent<S_AnimalBehaviour>())
                GetComponent<S_AnimalBehaviour>().Die();
            else if(GetComponent<S_EnemyBehaviour>())
                GetComponent<S_EnemyBehaviour>().Die();
        }
    }

    private void ChangeSmall()
    {
        isSmall = !isSmall;
        Invoke("ChangeSmall", 1);
    }
}
