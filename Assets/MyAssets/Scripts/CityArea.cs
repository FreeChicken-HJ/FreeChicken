using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityArea : MonoBehaviour
{
    public float destroyDisatance;
    public CitySceneSpawn citySceneSpawn;
    public CityScenePlayer playerTransform;
    public bool isSpawn;
    // Start is called before the first frame update
    public void Setup(CitySceneSpawn citySceneSpawn, CityScenePlayer playerTransform)
    {
        this.citySceneSpawn = citySceneSpawn;
        this.playerTransform = playerTransform;
    }
    void Start()
    {
        playerTransform = GameObject.Find("CityCharacter").GetComponent<CityScenePlayer>();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (playerTransform.transform.position.z - this.transform.position.z >= destroyDisatance && !citySceneSpawn.isStop)
        {
            //Destroy(car.gameObject);
            citySceneSpawn.SpawnArea();
            isSpawn = true;
            Destroy(gameObject);
        }
    }
}
