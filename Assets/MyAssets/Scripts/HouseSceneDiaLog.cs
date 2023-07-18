using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSceneDiaLog : MonoBehaviour
{
    public string[] sentences;
    public bool isCnt;

    void Update()
    {
        if (!isCnt)
        {
            HouseSceneTalkManager.instance.OndiaLog(sentences);
        }
        isCnt = true;
    }
}
