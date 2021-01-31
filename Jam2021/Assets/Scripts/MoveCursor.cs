using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCursor : MonoBehaviour
{
    public List<Collider2D> Cols;
    public Ghost[] GhostScripts;
    public CameraController Controller;
    public Transform Center;
    public float Radius = 1f;
    public Rigidbody2D RB;
    public float MoveSpeed = 10f;
    
    private Vector2 Movement;

    public Vector2 ExternalMovement;

    private SpriteRenderer SR;
    public SpriteRenderer InnerSprite;
    private Animator Anim;
    public Animator Anim2;
    public CameraController CC;
    public bool CanClick;
    private static readonly int IsHover = Animator.StringToHash("IsHover");


    // Start is called before the first frame update
    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
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
            if (Input.GetKeyDown(KeyCode.Return) && !MainUIScript.Instance.Readings.IsStart && MainUIScript.Instance.CurState != MainUIScript.States.Analyzing)
            {
                SoundManager.Instance.PlaySfx(0, .8f);
                CC.Zoom(RB.position);
                Cols[Cols.Count - 1].GetComponent<Cosmics>().SetValues();
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
            InnerSprite.color = Color.red;
            CanClick = true;
            Anim.SetBool(IsHover, true);
            Anim2.SetBool(IsHover, true);
            Cols.Add(other);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SpaceObj"))
        {
            Cols.Remove(other);
            if (Cols.Count == 0)
            {
                InnerSprite.color = Color.white;
                CanClick = false;

                Anim.SetBool(IsHover, false);
                Anim2.SetBool(IsHover, false);
                
            }
        }
    }

    public void StartHover()
    {
        //InnerSprite.enabled = false;
    }
    public void BackToIdle()
    {
        //InnerSprite.enabled = true;
    }
    
}
