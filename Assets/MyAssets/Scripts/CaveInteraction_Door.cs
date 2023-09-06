using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CaveScenePlayer>();
        if (key != null)
        {
            key = GameObject.FindGameObjectWithTag("Key").GetComponent<CaveItem_Key>();
        }
    }

    void Update()
    {
        OpenDoor();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !player.hasKey)
        {
            donotOpenDoorText.SetActive(true);
            Invoke("CloseText", 2f);
            isOpen = true;
        }

        if(other.gameObject.tag.Equals("Player") && player.hasKey)
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
        if (other.gameObject.tag.Equals("Player") && !player.hasKey)
        {
            donotOpenDoorText.SetActive(false);
            isOpen = false;
        }

        if(other.gameObject.tag.Equals("Player") && player.hasKey)
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
            Invoke("Destroy_Daddy", 3f);
            OpenDoorText.SetActive(false);
            --player.keyCount;
            gameObject.SetActive(false);
            //Destroy(gameObject);
            Thx.gameObject.SetActive(true);
            Invoke("Last", 3f);
            Debug.Log("���� ������");
            
        }
        else if(Input.GetButtonDown("Interaction") && isOpen &&!player.hasKey)
        {
            donotOpenDoorText.SetActive(true);
            Invoke("DestroyOpenDoorText", 1.5f);
        }
    }

    void Destroy_Daddy()
    {
        daddy.gameObject.SetActive(false);
    }

    void Last()
    {
        Thx.gameObject.SetActive(false);
    }

    void DestroyOpenDoorText()
    {
        OpenDoorText.gameObject.SetActive(false);
    }
}
