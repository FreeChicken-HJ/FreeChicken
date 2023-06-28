using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryHelpTxt : MonoBehaviour
{
    public GameObject txt;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
     
        if(other.gameObject.tag == "Player")
        {
            
            txt.SetActive(true);
            Debug.Log("Ãæµ¹");
        }
    }
}
