using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAttackSpace : MonoBehaviour
{

    //public GameObject prefab;
    //public GameObject dropRock;
    public GameObject dropFire;
    BoxCollider area;
    public int cnt = 3;
    //GameObject go;

    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {

        area = GetComponent<BoxCollider>();

    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < cnt; i++)
        {
            Spawn();

        }
    }




    void Spawn()
    {
        pos = GetRandomPos();

        /*GameObject go = prefab;
        GameObject instance = Instantiate(go, pos, Quaternion.identity);*/
        
        StartCoroutine(SpawnRock(pos));
        /*Destroy(instance, 3f);*/

    }
    IEnumerator SpawnRock(Vector3 pos)
    {

        yield return new WaitForSeconds(1f);

        GameObject fire = dropFire;
        GameObject ins = Instantiate(fire, pos, Quaternion.identity);



        //break;
    }


    Vector3 GetRandomPos()
    {
        Vector3 basePos = transform.position;
        Vector3 size = area.size;

        float posX = basePos.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posZ = basePos.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, -1.2f, posZ);
        Vector3 pos = area.center + spawnPos;
        return pos;
    }
}
