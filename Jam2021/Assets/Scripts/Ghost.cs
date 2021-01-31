using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    public Transform Holder;
    public float GhostTimer = 0f;
    public float LifeTimer = 0f;
    public float GhostTimerInterval = .5f;
    public bool GetRidOfScript;
    public bool Spawning = true;
    public bool IsFading = false;
    
    public float Lifetime = .2f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawning)
        {
            if (GhostTimer >= GhostTimerInterval)
            {
                GhostTimer = 0f;
                GameObject obj = Instantiate(this.gameObject, transform.position, transform.rotation, Holder);
                Ghost ghost = obj.GetComponent<Ghost>();
                
                if (GetRidOfScript)
                    ExtraFunctions(obj);
                ghost.IsFading = true;
                ghost.Spawning = false;

                SoundManager.Instance.PlaySfx(3, .25f);
            }
            else
                GhostTimer += Time.deltaTime;
        }
        else if (IsFading)
        {
            if (LifeTimer >= Lifetime)
                Destroy(gameObject);
            else
                ObjLessVisible();
            
            LifeTimer += Time.deltaTime;
        }
            
    }

    protected virtual void ObjLessVisible()
    {
        
    }

    public virtual void ExtraFunctions(GameObject obj)
    {
        if (obj.TryGetComponent(out MouseControl control))
        {
            control.enabled = false;
        }
    }
}
