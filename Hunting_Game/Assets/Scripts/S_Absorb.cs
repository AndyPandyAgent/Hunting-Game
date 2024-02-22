using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Absorb : MonoBehaviour
{
    public List<GameObject> hearts;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (hearts != null)
        {
            foreach(var heart in hearts)
            {
                heart.transform.position = Vector3.Lerp(heart.transform.position, player.transform.position, 0.5f * Time.deltaTime);
            }
        }
    }

    public void GetHeart(GameObject heart)
    {
        hearts.Add(heart);
    }
}
