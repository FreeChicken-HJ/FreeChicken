using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseImage : MonoBehaviour
{
    
    public Texture2D MouseChick; 

    void Start()
    {
       
        Cursor.SetCursor(MouseChick, Vector2.zero, CursorMode.Auto);
    }
}
