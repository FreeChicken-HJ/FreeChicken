using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeadCount : MonoBehaviour
{
    public TextMeshProUGUI text;
    public static int count;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = "Count : " + count.ToString();
    }
}
