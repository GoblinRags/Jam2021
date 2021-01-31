using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCursor : MonoBehaviour
{
    public Ghost[] GhostScripts;
    public CameraController Controller;
    public Transform Center;
    public float Radius = 1f;
    public Rigidbody2D RB;
    public float MoveSpeed = 10f;
    
    private Vector2 Movement;

    public Vector2 ExternalMovement;

    private SpriteRenderer SR;
    public CameraController CC;
    public bool CanClick;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        CC = CameraController.Instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (CanClick)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SoundManager.Instance.PlaySfx(0, 1);
                CC.Zoom(RB.position);
            }
        }
    
        Vector3 move = Vector3.zero;
        Movement = move.normalized;

        if (CC.CurState == CameraController.ZoomState.In || CC.IsLerping)
        {
            return;
        }
            
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move.x -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move.x += 1;
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            move.y += 1;
        }
        
        if (Input.GetKey(KeyCode.DownArrow))
        {
            move.y -= 1;
        }
        
        //float x = Input.GetAxis("Horizontal");
        //float y = Input.GetAxis("Vertical");
        
        //Vector3 move = Vector3.zero;
        // += new Vector3(x, y).normalized * MoveSpeed;
        //transform.position = transform.position + move.normalized * MoveSpeed * Time.deltaTime;
        Movement = move.normalized;

    }

    private void FixedUpdate()
    {
        //RB.velocity = Movement * MoveSpeed + Controller.Movement * Controller.MoveSpeed;
        Vector2 newPos = (Movement * MoveSpeed + Controller.Movement * Controller.MoveSpeed) * Time.fixedDeltaTime;
        newPos.x += RB.position.x;
        newPos.y += RB.position.y;
        
        Vector2 center = Center.position;
        float xPos = Mathf.Clamp(newPos.x, center.x - Radius, center.x + Radius);
        float yPos = Mathf.Clamp(newPos.y, center.y - Radius, center.y + Radius);
        
        RB.MovePosition(new Vector2(xPos, yPos));
        foreach (Ghost ghost in GhostScripts)
            ghost.Spawning = Movement != Vector2.zero;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.CompareTag("Cosmic"));
        if (other.CompareTag("SpaceObj"))
        {
            SR.color = Color.red;
            CanClick = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SpaceObj"))
        {
            SR.color = Color.white;
            CanClick = false;
        }
    }
    
    
}
