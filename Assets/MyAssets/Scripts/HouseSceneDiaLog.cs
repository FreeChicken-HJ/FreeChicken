using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSceneDiaLog : MonoBehaviour
{
    public string[] sentences;
    public bool isCnt;
    //private void OnMouseDown()
    //{
    //    if(HouseSceneTalkManager.instance.dialoguegroup.alpha==0)
    //        HouseSceneTalkManager.instance.OndiaLog(sentences);
    //}

    void Update()
    {
        if (!isCnt)
        {
            HouseSceneTalkManager.instance.OndiaLog(sentences);
        }
        isCnt = true;
    }
}
