using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimator : MonoBehaviour
{

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //animator.Play("Metarig|metarig Action");
            if (animator.gameObject.GetComponent<Animator>().enabled)
                animator.gameObject.GetComponent<Animator>().enabled = false;
            else
                animator.gameObject.GetComponent<Animator>().enabled = true;
        }
    }
}
