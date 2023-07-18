using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryNPCAnim : MonoBehaviour
{
    public Animator animator;
    FactoryPlayer player;
   
   
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isTalk)
        {
            animator.SetBool("isTalk",true);
            
        }
        else if (!player.isTalk)
        {
            animator.SetBool("isTalk",false);
        }
    }

}
