using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FollowParent : MonoBehaviour
{
   

    CaveScenePlayer player;
    Animator animator;
    public Vector3 Offset;
        void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CaveScenePlayer>();
        animator = GetComponent<Animator>();

       
    }
    void Update()
    {
        if (player.isFinal == true)
        {
           
            
            transform.position = player.transform.position + Offset;
            
            animator.SetBool("isRun", true);
            
            
        }
    }

  
}
