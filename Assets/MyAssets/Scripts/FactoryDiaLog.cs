using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryDiaLog : MonoBehaviour
{
    
    public string[] sentences;
    public bool isCnt;
    void Update()
    {
        if (!isCnt)
        {
            FactoryUIManager.instance.OndiaLog(sentences);
        }
        isCnt = true;
         
    }
}
