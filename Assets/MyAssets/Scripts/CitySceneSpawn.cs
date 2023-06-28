using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySceneSpawn : MonoBehaviour
{
    public GameObject[] areaPrefabs;
    public GameObject lastMap;
    public float zDistance;
    int areaIndex = 0;
    int spawnAreaCntStart = 2;
    public CityScenePlayer playerTransform;

    //public int cityCnt;

    public bool isStop;
    public bool isFinish;
    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < spawnAreaCntStart; i++)
        {
            if(i == 0)
            {
                SpawnArea(false);
            }
            else
            {
                SpawnArea();
            }
        }
    }
    void Start()
    {
        playerTransform = GameObject.Find("CityCharacter").GetComponent<CityScenePlayer>();
    }
    private void Update()
    {
        if (areaIndex == 3 && !isFinish)
        {
            isStop = true;
            SpawnLastMap();
            //Debug.Log("∞‘¿” ≥°");
        }
    }
    public void SpawnArea(bool isRandom = true)
    {
        GameObject clone = null;
        if (isRandom == false)
        {
            clone = Instantiate(areaPrefabs[0]);
           /* cityCnt++;
            Debug.Log(cityCnt);*/
        }
        else
        {
            int ranRange = Random.Range(0, areaPrefabs.Length);
            clone = Instantiate(areaPrefabs[ranRange]);
           /* cityCnt++;
            Debug.Log(cityCnt);*/
        }
        
        clone.transform.position = new Vector3(0,0, areaIndex * zDistance);
        clone.GetComponent<CityArea>().Setup(this,playerTransform);
        areaIndex++;
        Debug.Log(areaIndex);

    }
    void SpawnLastMap()
    {
        isFinish = true;
        GameObject clone = null;
        clone = Instantiate(lastMap);
        clone.transform.position = new Vector3(0, 0, areaIndex * zDistance);
        //clone.GetComponent<CityArea>().Setup(this, playerTransform);

    }
}
