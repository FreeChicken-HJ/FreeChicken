using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
   
    public float carSpeed;
    Vector3 curPosition;
    Vector3 curRotation;
    Quaternion rotation;
    // Start is called before the first frame update
    void Awake()
    {
       
        curPosition = transform.position;
        curRotation = transform.localEulerAngles;
        rotation = Quaternion.Euler(curRotation);
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        yield return new WaitForSeconds(1.5f);
       
        transform.Translate(Vector3.forward * carSpeed *Time.deltaTime);
        yield return new WaitForSeconds(3f);
        Instantiate(this.gameObject,curPosition, rotation);
        Destroy(this.gameObject);
        yield return new WaitForSeconds(2f);
    }
}
