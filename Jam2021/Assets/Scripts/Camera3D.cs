using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3D : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Cam = GetComponent<Camera>();
    }

    private Camera Cam;
    private Rigidbody RB;
    public float ZoomSpeed;
    public float MoveSpeed;
    public float Zoom;
    public Vector2 Movement;
    // Update is called once per frame
    void Update()
    {
        Vector2 move = Vector3.zero;
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
        Movement = move.normalized;

        Zoom = Input.mouseScrollDelta.y;
    }

    void FixedUpdate()
    {
        Vector2 newPos = Movement * (MoveSpeed * Time.fixedDeltaTime);
        Vector3 rotation = transform.rotation.eulerAngles;
        
        float zoom = Zoom * ZoomSpeed * Time.fixedDeltaTime;
        Cam.fieldOfView -= zoom;
        rotation.x -= newPos.y;
        rotation.y += newPos.x;
        //
        RB.MoveRotation(Quaternion.Euler(rotation));
    }
}
