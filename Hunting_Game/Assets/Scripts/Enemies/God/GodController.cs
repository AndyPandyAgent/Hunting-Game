using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodController : MonoBehaviour
{
    public GameObject hand;

    public void GiveSkill()
    {
        Instantiate(hand, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
            GiveSkill();
    }
}
