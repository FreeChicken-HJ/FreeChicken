using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScene_SavePoint : MonoBehaviour
{
    HouseScenePlayer player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScenePlayer>();
    }

    public void SavePoint1()
    {
        player.transform.position = new Vector3(5.01999998f, 1.32000005f, 18.3449993f);
    }
    public void DelaySavePoint1()
    {
        Invoke("SavePoint1", 2f);
    }

    public void SavePoint2()
    {
        player.transform.position = new Vector3(40.1230011f, 0.428000003f, 16.7889996f);
    }

    public void DelaySavePoint2()
    {
        Invoke("SavePoint2", 2f);
    }

    public void SavePoint3()
    {
        player.transform.position = new Vector3(75.8610001f, 8.06000042f, 16.1520004f);
    }    

    public void DelaySavePoint3()
    {
        Invoke("SavePoint3", 2f);
    }
}
