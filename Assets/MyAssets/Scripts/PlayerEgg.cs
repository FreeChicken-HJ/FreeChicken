using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEgg : MonoBehaviour
{
    public bool isFire;
    Vector3 direction;
    [SerializeField] float speed = 1f;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        if(isFire)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }

    public void Fire(Vector3 dir)
    {
        direction = dir;
        isFire = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerEgg>() != null)
        {
            Destroy(gameObject);
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Note")
    //    {
    //        Debug.Log("´ê¾ÒÀ½");
    //    }
    //}

}
