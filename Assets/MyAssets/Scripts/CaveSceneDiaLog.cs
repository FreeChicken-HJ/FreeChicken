using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSceneDiaLog : MonoBehaviour
{
    public string[] sentences;
    public bool isCnt;
 
    // Update is called once per frame
    void Update()
    {
        if (!isCnt)
        {
            CaveSceneTalkManager.instance.OndiaLog(sentences);
        }
        isCnt = true;

    }
}
