using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CaveItem_SmallPotion : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI InteractionSmallPotionText;

    bool isMovePotion;
    public bool isInteraction;

    Vector3 vec = new Vector3(100f, 2.6500001f, -100f);
    

    CaveScenePlayer player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CaveScenePlayer>();;
        InteractionSmallPotionText.gameObject.SetActive(false);
    }
    void Update()
    {
        MovePotion();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("포션에 가까이 갔닭");
            isInteraction = false;
            InteractionSmallPotionText.gameObject.SetActive(true);
            isMovePotion = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isInteraction = false;
            InteractionSmallPotionText.gameObject.SetActive(false);
            isMovePotion = false;
        }
    }
    void MovePotion() // 물약옮기기
    {
        if (Input.GetButtonDown("Interaction") && isMovePotion)
        {
            Debug.Log("물약밀기");
            isInteraction = true;

            this.transform.position = Vector3.MoveTowards(this.transform.position, vec, 0.1f);
            InteractionSmallPotionText.gameObject.SetActive(false);

            //Invoke("notshowtext", 1.5f);
        }
    }
    //void notshowtext()
    //{

    //}
}
