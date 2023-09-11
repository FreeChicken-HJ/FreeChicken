using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CaveItem_DebuffPotion : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nearPotionItemText;
    [SerializeField] TextMeshProUGUI pickUpPotionItemText;

    bool isPickUp;
    public bool reversalPotion;


    void Start()
    {
        pickUpPotionItemText.gameObject.SetActive(false);
        nearPotionItemText.gameObject.SetActive(false);
    }
    void Update()
    {
        PickUp();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            reversalPotion = false;
            nearPotionItemText.gameObject.SetActive(true);
            isPickUp = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            reversalPotion = false;
            nearPotionItemText.gameObject.SetActive(false);
            isPickUp = false;
        }
    }

    void PickUp()
    {
        if (Input.GetButtonDown("Interaction") && isPickUp)
        {
            reversalPotion = true; 
            gameObject.SetActive(false);
            nearPotionItemText.gameObject.SetActive(false);
            pickUpPotionItemText.gameObject.SetActive(true);
            
            Invoke("notshowtext", 1.5f);
        }
    }
    void notshowtext()
    {
        pickUpPotionItemText.gameObject.SetActive(false);
    }
}
