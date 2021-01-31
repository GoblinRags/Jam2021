using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Image ClickButton;
    public TextMeshProUGUI ClickButtonText;
    public TextMeshProUGUI MoneyText;
    public int Money = 0;
    public static UpgradeManager Instance;
    public static List<Upgrades> UpgradesArray = new List<Upgrades>();
    public bool IsBlinking;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckButtonsCost();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Check if can afford button cost
    public void CheckButtonsCost()
    {
        if (Money < 50f)
        {
            IsBlinking = true;
            StartCoroutine(Blink());
        }
            
        MoneyText.text = Money.ToString();
        foreach (Upgrades upg in UpgradesArray)
        {
            upg.SetButton(Money >= upg.CurrentCost);
        }
    }

    public void AddMoney(int add)
    {
        Money += add;
        MoneyText.text = Money.ToString();
    }
    
    public IEnumerator Blink()
    {
        bool flip = false;
        while (IsBlinking)
        {
            ClickButton.color = flip == false ?Color.yellow : Color.white;
            ClickButtonText.color = flip == false ?Color.yellow : Color.white;
            flip = !flip;    
            yield return new WaitForSecondsRealtime(.5f);
        }
    }

    public void ContinuePress()
    {
        IsBlinking = false;
        //next scene
    }
    
}
