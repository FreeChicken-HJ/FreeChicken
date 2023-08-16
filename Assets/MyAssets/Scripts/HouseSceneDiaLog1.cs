using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSceneDiaLog1 : MonoBehaviour
{
    public string[] sentences;
    public bool isCnt;

    void Update()
    {
        if (!isCnt)
        {
            HouseSceneTalkManager1.instance.OndiaLog(sentences);
        }
        isCnt = true;
    }
}
