using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    BoxCollider boxCollider;
    Animator animator;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("doOpen");
            animator.SetBool("isOpen", true);
        }
    }
}
