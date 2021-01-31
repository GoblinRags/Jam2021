using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    enum States {Menu, Game, Shop}
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        newPos = Camera.main.ScreenToWorldPoint(newPos);
        newPos.z = 0f;
        transform.position = newPos;
    }
}
