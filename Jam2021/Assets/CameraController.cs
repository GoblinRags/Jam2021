using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public Rigidbody2D RB;
    public float MoveSpeed = 10f;

    public Vector2 Movement;
    
    
    public float ZoomInSize = 1f;
    public float ZoomOutSize = 5f;
    public float ZoomTime = 2f;
    
    public enum ZoomState {In, Out}

    public Vector3 ZoomOutPos;
    public Vector3 ZoomInPos;
    public ZoomState CurState = ZoomState.Out;

    public bool IsLerping;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
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

        if (move.y != 0 || move.x != 0) {
            SoundManager.Instance.PlaySfx(2, .05f);
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
        RB.velocity = Movement * MoveSpeed;
        //RB.velocity = Movement * MoveSpeed + Controller.Movement * Controller.MoveSpeed;
        Vector2 newPos = Movement * (MoveSpeed * Time.fixedDeltaTime);
        newPos.x += RB.position.x;
        newPos.y += RB.position.y;
        RB.MovePosition(newPos);
    }

    public IEnumerator Lerp(ZoomState state, Vector3 zoomInPos)
    {
        float timer = 0f;
        Vector3 startPos = (state == ZoomState.In) ? ZoomOutPos : zoomInPos;
        Vector3 endPos = (state == ZoomState.Out) ? ZoomOutPos : zoomInPos;
        
        float startSize = (state == ZoomState.In) ? ZoomOutSize : ZoomInSize;
        float endSize = (state == ZoomState.Out) ? ZoomOutSize : ZoomInSize;

        float fxTick = 0f;

        IsLerping = true;
        while (timer < ZoomTime)
        {
            timer += Time.deltaTime;
            float t = timer / ZoomTime;
            float newSize = Mathf.Lerp(startSize, endSize, Mathf.SmoothStep(0f, 1f, t));
            Vector3 newPos = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, t));
            SetCamPos(newPos, newSize);

            fxTick += 1;

            if (fxTick % 30 == 0 && timer < ZoomTime*0.9f) {
                SoundManager.Instance.PlaySfx(1, .6f);
            }
            yield return null;
        }

        SoundManager.Instance.PlaySfx(4, .5f);
        SetCamPos(endPos, endSize);
        IsLerping = false;
    }

    public void SetCamPos(Vector3 pos, float size)
    {
        pos.z = -10;
        transform.position = pos;
        Camera.main.orthographicSize = size;
    }

    public void Zoom(Vector3 zoomInPos)
    {
        if (!IsLerping)
        {
            if (CurState == ZoomState.Out)
            {
                ZoomOutPos = transform.position;
                ZoomInPos = zoomInPos;
            }
            CurState = CurState == ZoomState.In ? ZoomState.Out : ZoomState.In;
            StartCoroutine(Lerp(CurState, zoomInPos));
        }
    }
}
