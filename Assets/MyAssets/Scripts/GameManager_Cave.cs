using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_Cave : MonoBehaviour
{
    public int totalKeyCount;
    public TextMeshProUGUI playerCountText;

    public void GetKey(int count)
    {
        playerCountText.text = count.ToString();
    }

}
