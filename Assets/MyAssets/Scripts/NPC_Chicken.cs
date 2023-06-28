using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPC_Chicken : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI nearNPCText;
    [SerializeField] TextMeshProUGUI TalkNPCText;
    [SerializeField] Image ChatNPCImage;

    [SerializeField] Image imagetest;

    HouseScenePlayer player;

    public Camera Talk_AI_Chicken;

    bool isTalkNPC;

    void Start()
    {
        Talk_AI_Chicken.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScenePlayer>();

        
        //nearNPCText.gameObject.SetActive(false);
        //TalkNPCText.gameObject.SetActive(false);
        //ChatNPCImage.gameObject.SetActive(false);
        //imagetest.gameObject.SetActive(false);
    }

    void Update()
    {
        TalkNPC();
    }

    void TalkNPC()
    {
        if (Input.GetButtonDown("Interaction") && isTalkNPC)
        {
            //nearNPCText.gameObject.SetActive(false);

            Talk_AI_Chicken.enabled = true;
            

            imagetest.gameObject.SetActive(false);
            TalkNPCText.gameObject.SetActive(true);
            ChatNPCImage.gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("NPC¥ﬂø°∞‘ ∞°±Ó¿Ã ∞¨¥ﬂ");
            //nearNPCText.gameObject.SetActive(true);

            imagetest.gameObject.SetActive(true);

            isTalkNPC = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            //nearNPCText.gameObject.SetActive(false);
            imagetest.gameObject.SetActive(false);
            isTalkNPC=false;
        }
    }

    

    void Camera()
    {
        Talk_AI_Chicken.enabled = true;
    }

    public void notshowtext()
    {
        TalkNPCText.gameObject.SetActive(false);
        ChatNPCImage.gameObject.SetActive(false);
        Talk_AI_Chicken.enabled = false;
    }
}
