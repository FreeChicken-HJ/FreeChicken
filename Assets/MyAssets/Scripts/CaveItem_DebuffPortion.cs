using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaveItem_DebuffPortion : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nearPortionItemText;
    [SerializeField] TextMeshProUGUI pickUpPortionItemText;

    bool isPickUp;
    public bool reversalPortion;

    CaveScenePlayer player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CaveScenePlayer>();
        pickUpPortionItemText.gameObject.SetActive(false);
        nearPortionItemText.gameObject.SetActive(false);
    }
    void Update()
    {
        PickUp();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Æ÷¼Ç¿¡ °¡±îÀÌ °¬´ß");
            reversalPortion = false;
            nearPortionItemText.gameObject.SetActive(true);
            isPickUp = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            reversalPortion = false;
            nearPortionItemText.gameObject.SetActive(false);
            isPickUp = false;
        }
    }
    void PickUp()
    {
        if (Input.GetButtonDown("Interaction") && isPickUp)
        {
            Debug.Log("Æ÷¼ÇÀ» ¾ò¾ú´ß");
            reversalPortion = true; 
            gameObject.SetActive(false);
            nearPortionItemText.gameObject.SetActive(false);
            pickUpPortionItemText.gameObject.SetActive(true);
            
            Invoke("notshowtext", 1.5f);
        }
    }
    void notshowtext()
    {
        pickUpPortionItemText.gameObject.SetActive(false);
    }
}
