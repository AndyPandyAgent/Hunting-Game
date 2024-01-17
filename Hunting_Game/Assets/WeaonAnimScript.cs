using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaonAnimScript : MonoBehaviour
{ 
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayAnim()
    {
        print("AnimPlay");
        animator.Play("Shoot");
    }
}
