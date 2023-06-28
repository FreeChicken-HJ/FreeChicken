using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
   
    public float carSpeed;
   
    bool attackPlayer;
   
   
    // Start is called before the first frame update
    void Awake()
    {
        
        //area = GameObject.FindGameObjectWithTag("CityMap").GetComponent<CityArea>();
    }
    void Start()
    {

       
        
    }
    void Update()
    {
        Move();
        
    }
    // Update is called once per frame
    void Move()
    {
        if (!attackPlayer)
        {
            transform.Translate(Vector3.right * carSpeed * Time.deltaTime);

        }
    }
  /*  IEnumerator Move()
    {
        yield return new WaitForSeconds(.1f);
        

        if (!attackPlayer)
        {
            transform.Translate(Vector3.right * carSpeed * Time.deltaTime);
            
        }
        yield return new WaitForSeconds(2f);

       
        
        Instantiate(this.gameObject, curPosition, rotation);
        
        
        //yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }*/
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            attackPlayer = true;
            
            Destroy(this.gameObject,1f);
        }

         
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "TrainDestroy")
        {
            Destroy(this.gameObject);
        }    
    }
}
