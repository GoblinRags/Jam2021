using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void HoverButton()
    {
        //NOTE: for STEVE to change sfx
        SoundManager.Instance.PlaySfx(0, 1f);
    }

    public void PressButton()
    {
        //change sfx-STEVE - default button sfx->play unique sfx in Upgrades Script
        SoundManager.Instance.PlaySfx(4, 1f);
    }


    
}
