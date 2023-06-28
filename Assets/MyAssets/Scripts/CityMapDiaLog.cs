using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityMapDiaLog : MonoBehaviour
{
    
    public string[] sentences;
    public bool isCnt;
    void Update()
    {
        if (!isCnt)
        {
            CityMapUIManager.instance.OndiaLog(sentences);
        }
        isCnt = true;
         
    }
}
