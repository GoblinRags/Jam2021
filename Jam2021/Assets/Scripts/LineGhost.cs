using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGhost : Ghost
{
    public LineRenderer LR;
    
    private void Awake()
    {
        LR = GetComponent<LineRenderer>();
    }

    protected override void ObjLessVisible()
    {
        float alpha = LR.colorGradient.alphaKeys[0].alpha;
        
        float newAlpha = Mathf.Lerp(.5f, 0f, LifeTimer / Lifetime);
        foreach (GradientAlphaKey key in LR.colorGradient.alphaKeys)
        {
            var gradientAlphaKey = key;
            gradientAlphaKey.alpha = newAlpha;
        }
    }


}
