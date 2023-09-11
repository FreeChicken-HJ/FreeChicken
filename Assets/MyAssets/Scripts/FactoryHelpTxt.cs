using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryHelpTxt : MonoBehaviour
{
    public GameObject txt;
    private void OnTriggerEnter(Collider other)
    {   
        if(other.gameObject.tag == "Player")
        {            
            txt.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            txt.SetActive(false);
        }
    }
}
