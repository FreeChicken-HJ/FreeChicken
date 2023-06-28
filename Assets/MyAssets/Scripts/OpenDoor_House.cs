using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenDoor_House : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI opendoorText;
    [SerializeField] TextMeshProUGUI neardoorText;
    bool isOpen;

    HouseScenePlayer player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScenePlayer>();
        opendoorText.gameObject.SetActive(false);
        neardoorText.gameObject.SetActive(false);
    }
    void Update()
    {
        OpenDoor();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("πÆø° ∞°±Ó¿Ã ∞¨¥ﬂ");
            neardoorText.gameObject.SetActive(true);
            isOpen = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            neardoorText.gameObject.SetActive(false);
            isOpen = false;
        }
    }

    void OpenDoor()
    {
        if (Input.GetButtonDown("Interaction") && isOpen)
        {
            gameObject.SetActive(false);
            neardoorText.gameObject.SetActive(false);
            opendoorText.gameObject.SetActive(true);
            Invoke("notshowtext", 1.5f);

        }
    }

    void notshowtext()
    {
        opendoorText.gameObject.SetActive(false);
    }
}
