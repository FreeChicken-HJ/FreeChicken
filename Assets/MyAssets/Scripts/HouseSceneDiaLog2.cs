using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSceneDiaLog2 : MonoBehaviour
{
    public string[] sentences;
    public bool isCnt;

    void Update()
    {
        if (!isCnt)
        {
            HouseSceneTalkManager2.instance.OndiaLog(sentences);
        }
        isCnt = true;
    }
}
