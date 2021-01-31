using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGhost : Ghost
{
    public SpriteRenderer SR;
    
    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    protected override void ObjLessVisible()
    {
        Color newColor = Color.Lerp(Color.white, Color.clear, LifeTimer / Lifetime);
        //Color color = SR.color;
        //color.a -= (255f / Lifetime) * Time.deltaTime;
        SR.color = newColor;
    }
    
    
}
