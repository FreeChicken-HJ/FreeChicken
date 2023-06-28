using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBelt : MonoBehaviour
{
    FactoryPlayer player;
    
    public float Speed;
    void Start()
    {
        player = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer>();
        Speed = 0.5f;
    }
    // Start is called before the first frame update
    void Update()
    {
        Move();
    }
    void Move()
    {
        if (player.isSlide)
        {
            Speed = 0.5f;
            player.transform.Translate(Vector3.forward * Time.deltaTime * Speed,Space.World);
            
        }
        else if (!player.isSlide)
        {
            Speed = 0;
        }

    }
   
}
