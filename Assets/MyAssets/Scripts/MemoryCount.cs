using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MemoryCount : MonoBehaviour
{
    public TextMeshProUGUI text;
    public static int memCount;
    public int TotalmemCount;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (memCount <= TotalmemCount)
        {
            text.text = memCount.ToString();
        }
    }
}
