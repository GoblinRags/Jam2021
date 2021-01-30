using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float MoveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            move.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move.x += 1;
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            move.y += 1;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            move.y -= 1;
        }
        
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //Vector3 move = Vector3.zero;
        // += new Vector3(x, y).normalized * MoveSpeed;
        transform.position = transform.position + move.normalized * MoveSpeed * Time.deltaTime;
    }
}
