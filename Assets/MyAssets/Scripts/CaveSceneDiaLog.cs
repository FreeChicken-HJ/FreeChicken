using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSceneDiaLog : MonoBehaviour
{
    public string[] sentences;
    public bool isCnt;
 
    void Update()
    {
        if (!isCnt)
        {
            CaveSceneTalkManager.instance.OndiaLog(sentences);
        }
        isCnt = true;

    }
}
