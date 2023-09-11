using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltingObj : MonoBehaviour
{
    public GameObject meltingObj; 
    public Color colColor = Color.red; 
    private Color originalColor; 
    private bool isCollision = false; 
    public float hideTime; 
    public float respawnTime; 
    private Vector3 originalPos; 

    void Start()
    {
        originalColor = meltingObj.GetComponent<Renderer>().material.color;
        originalPos = meltingObj.transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isCollision)
            {
                meltingObj.GetComponent<Renderer>().material.color = colColor;
                isCollision = true;

                Invoke("HideObject", hideTime);
            }
        }
    }

    void HideObject()
    {
        meltingObj.SetActive(false);
        Invoke("RespawnObject", respawnTime);
    }

    void RespawnObject()
    {
        meltingObj.GetComponent<Renderer>().material.color = originalColor;
        meltingObj.transform.position = originalPos; 
        meltingObj.SetActive(true);
        isCollision = false;
    }
}
