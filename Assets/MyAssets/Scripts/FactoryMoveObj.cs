using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryMoveObj : MonoBehaviour
{
    public float Speed;
    // Start is called before the first frame update


    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Slide")
        {
            
            this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * Speed, Space.World);
        }
        if (other.tag == "TurnPointR")
        {
            this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * Speed, Space.World);
        }
        if(other.tag == "TurnPointL")
        {
            this.gameObject.transform.Translate(Vector3.left * Time.deltaTime * Speed, Space.World);
        }
        if(other.tag == "TurnPointD")
        {
            this.gameObject.transform.Translate(Vector3.back * Time.deltaTime * Speed, Space.World);

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
