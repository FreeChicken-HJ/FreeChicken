using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRock : MonoBehaviour
{
  
    Boss boss;
    public GameObject[] prefab;
    BoxCollider area;
    public int cnt = 20;
    GameObject go;
    // Start is called before the first frame update
    void Start()
    {
       boss = GameObject.Find("Boss").GetComponent<Boss>();
       area = GetComponent<BoxCollider>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.isRockFalling)
        {
           for(int i = 0; i < cnt; i++)
            {
                Spawn();
               
            }
           
        }
        boss.isRockFalling = false;
    }
    void Spawn()
    {
        Vector3 pos = GetRandomPos();
        int selection = Random.Range(0, prefab.Length);
        GameObject go = prefab[selection];
        GameObject instance = Instantiate(go, pos,Quaternion.identity);
        float random = Random.Range(3.5f, 5f);
        Destroy(instance, random);
    }
    
    Vector3 GetRandomPos()
    {
        Vector3 basePos = transform.position;
        Vector3 size = area.size;

        float posX = basePos.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePos.y + Random.Range(-size.y / 2f, size.y / 2f);
        float posZ = basePos.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);
        Vector3 pos = area.center + spawnPos;
        return pos;
    }
}
