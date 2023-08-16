using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms;

public class PushButtonDoor_Cave : MonoBehaviour
{
    public GameObject Door1;
    public GameObject Door2;

    public GameObject Player;
    public bool doorSet;  // ¹® È®Á¤ ¿©ºÎ
    public bool isPush;
    public float upSpeed;
    public float downSpeed;

    //¾Æºü¶û Çùµ¿
    public GameObject Daddy;
    public bool goDaddy;
    public GameObject target;
   /* GameObject Pos1;
    GameObject Pos2;
    GameObject Pos3;*/
    public GameObject DaddyFinish;
    public bool isLast;

    Animator anim;
    public GameObject KissZone;
    void Start()
    {
        anim = Daddy.GetComponent<Animator>();
    }

    void Update()
    {
        if (isPush)
        {
            MoveDaddy();
            if (!doorSet)
            {
                
                Pushing();
                if (Door1.transform.position.y >= 5f)
                {
                    doorSet = true;
                    goDaddy = true;
                }
            }
        }

        if (!isPush && !doorSet)
        { 
            Door1.transform.Translate(Vector3.down * (Time.deltaTime / downSpeed));
            Door2.transform.Translate(Vector3.down * (Time.deltaTime / downSpeed));

            if (Door1.transform.position.y <= 3.384f)
            {
                doorSet = true;
                goDaddy = false;
               
            }
        }
        if (Daddy.transform.position.x >= target.transform.position.x && isLast && Player.transform.position.x >= target.transform.position.x)
        {
            isLast = false;
            DaddyFinish.SetActive(true);
            Daddy.SetActive(false);
            Invoke("ButtonFinish", 2f);
        }
        
    }
    
    void Pushing()
    {
       if(!doorSet)
        {
            Vector3 translation = new Vector3(0f, 1f, 0f) * Time.deltaTime * upSpeed;
            Door1.transform.Translate(translation);
            Door2.transform.Translate(translation);
            
        }
    }

    void MoveDaddy()
    {
        //goDaddy = true;
        //Vector3 move = new Vector3(0f, 0f, 1f) * Time.deltaTime * 3f;
        //Daddy.transform.Translate(move);
        if (goDaddy)
        {
            anim.SetBool("isRun", true);
            Daddy.transform.position = Vector3.MoveTowards(Daddy.transform.position, target.transform.position, Time.deltaTime * 2);
            

            
        }
    }

    void ButtonFinish()
    {
        DaddyFinish.SetActive(false);
        //Daddy.SetActive(true);
        //Daddy.transform.position = KissZone.transform.position;
        Daddy.transform.position = new Vector3(Daddy.transform.position.x + 12f, Daddy.transform.position.y, Daddy.transform.position.z-1.2f);
        anim.SetTrigger("Kiss");
        
    }
   
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("¹öÆ° ´­·µ»ï");
            isPush = true;
            doorSet = false;
            goDaddy = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("¹öÆ° ¶Ã»ï");
            isPush = false;
            doorSet = false;
            goDaddy = false;
            anim.SetBool("isRun", false);
        }
    }
}
