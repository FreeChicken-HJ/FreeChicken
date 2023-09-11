using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveInteraction_Door : MonoBehaviour
{
    public GameObject OpenDoorText;
    public GameObject donotOpenDoorText;
    bool isOpen;

    CaveScenePlayer player;
    CaveItem_Key key;

    public AudioSource OpenDoorClear;
  
    public GameObject Thx;
    public GameObject daddy;
    public GameObject Target;
    public GameObject Door;
    Animator dadAnim;
    
    public bool isEnd;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CaveScenePlayer>();
        dadAnim = daddy.GetComponent<Animator>();
        if (key != null)
        {
            key = GameObject.FindGameObjectWithTag("Key").GetComponent<CaveItem_Key>();
        }
    }

    void Update()
    {
        OpenDoor();
        if (isEnd)
        {
            dadAnim.SetBool("isWalk", true);
            daddy.transform.position = Vector3.MoveTowards(daddy.transform.position, Target.transform.position, Time.deltaTime * 3f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !player.hasKey &&!isEnd)
        {
            donotOpenDoorText.SetActive(true);
            Invoke("CloseText", 2f);
            isOpen = true;
        }

        if(other.gameObject.CompareTag("Player") && player.hasKey && !isEnd)
        {
            isOpen = true;
            OpenDoorText.SetActive(true);
        }
    }
    void CloseText()
    {
        donotOpenDoorText.SetActive(false);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !player.hasKey && isOpen)
        {
            donotOpenDoorText.SetActive(false);
            isOpen = false;
        }

        if(other.gameObject.CompareTag("Player") && player.hasKey && isOpen)
        {
            isOpen = false;
            OpenDoorText.SetActive(false);
        }
    }

    void OpenDoor()
    {
        if (Input.GetButtonDown("Interaction") && isOpen && player.hasKey)
        {
            OpenDoorClear.Play();
            Invoke("SetEnd", 3f);
            
            OpenDoorText.SetActive(false);
            --player.keyCount;

            Door.SetActive(false);
            
            Thx.gameObject.SetActive(true);
            Invoke("Last", 6f);
            
        }
        else if(Input.GetButtonDown("Interaction") && isOpen &&!player.hasKey)
        {
            donotOpenDoorText.SetActive(true);
            Invoke("DestroyOpenDoorText", 1.5f);
        }
    }

    void SetEnd()
    {
        isEnd = true;
    }

    void Last()
    {
        gameObject.SetActive(false);
        daddy.gameObject.SetActive(false);
        Thx.gameObject.SetActive(false);
    }

    void DestroyOpenDoorText()
    {
        OpenDoorText.gameObject.SetActive(false);
    }
}
