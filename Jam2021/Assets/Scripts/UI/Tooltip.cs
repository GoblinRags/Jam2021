using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public static Tooltip Instance;
    
    public Camera Cam;
    public TextMeshProUGUI TooltipText;
    public RectTransform ParentRect;
    public RectTransform RT;
    public float PaddingSize = 10f;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            Instance = this;
        }
        else if (Instance == null)
        {
            Instance = this;
        }
        //ShowTooltip("HiHIHIHIHI");
        ParentRect = transform.parent.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowTooltip("Kewkwkewk haha");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(ParentRect, Input.mousePosition, Cam, out localPos);
        localPos.x += RT.sizeDelta.x;
        transform.localPosition = localPos;
    }

    private void ShowTooltip(string text)
    {
        gameObject.SetActive(true);
        TooltipText.text = text;
        Vector2 bgSize = Vector2.zero;
        bgSize.x = TooltipText.preferredWidth + PaddingSize * 2f;
        bgSize.y = TooltipText.preferredHeight+ PaddingSize * 2f;
        RT.sizeDelta = bgSize;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
        
    }

    public static void ShowToolTipStatic(string text)
    {
        Instance.ShowTooltip(text);
    }

    public static void HideToolTipStatic()
    {
        Instance.HideTooltip();
    }
    
}
