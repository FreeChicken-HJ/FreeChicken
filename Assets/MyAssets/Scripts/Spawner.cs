using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject target;
    public Transform spqwnPos;
    // Start is called before the first frame update
    void Start()
    {
        // 오브젝트 생성
        Instantiate(target, spqwnPos.position, spqwnPos.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(0, 0, -1) * moveSpeed * Time.deltaTime;
    }
}
