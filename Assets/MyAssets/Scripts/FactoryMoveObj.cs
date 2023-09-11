using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryMoveObj : MonoBehaviour
{
    public float Speed;
 
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Slide"))
        {
            
            this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * Speed, Space.World);
        }
        if (other.CompareTag("TurnPointR"))
        {
            this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * Speed, Space.World);
        }
        if(other.CompareTag("TurnPointL"))
        {
            this.gameObject.transform.Translate(Vector3.left * Time.deltaTime * Speed, Space.World);
        }
        if(other.CompareTag("TurnPointD"))
        {
            this.gameObject.transform.Translate(Vector3.back * Time.deltaTime * Speed, Space.World);

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("PickUpPoc"))
        {
            Destroy(this.gameObject);
        }
    }
}
