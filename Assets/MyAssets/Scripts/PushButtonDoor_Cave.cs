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
    Vector3 target = new Vector3(140f, -1.47000027f, 45.8227806f);
    public GameObject DaddyFinish;

    Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isPush && !doorSet)
        {
            Pushing();
            MoveDaddy();

            if (Door1.transform.position.y >= 5f)
            {
                doorSet = true;
                goDaddy= true;
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
        goDaddy = true;
        //Vector3 move = new Vector3(0f, 0f, 1f) * Time.deltaTime * 3f;
        //Daddy.transform.Translate(move);
        Daddy.transform.position = Vector3.MoveTowards(Daddy.transform.position, target, 0.02f);
        //anim.SetBool("isRun", true);

        if (Daddy.transform.position == target)
        {
            DaddyFinish.SetActive(true);
            Invoke("ButtonFinish", 2f);
        }
    }

    void ButtonFinish()
    {
        DaddyFinish.SetActive(false);
        Daddy.SetActive(false);
    }

    void StopDaddy()
    {
        anim.SetBool("isRun", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("¹öÆ° ´­·µ»ï");
            isPush = true;
            doorSet = false;
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
        }
    }
}
