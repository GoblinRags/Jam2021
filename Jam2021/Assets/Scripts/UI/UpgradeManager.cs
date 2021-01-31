using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int Money = 0;
    public static UpgradeManager Instance;
    public static List<Upgrades> UpgradesArray = new List<Upgrades>();

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Check if can afford button cost
    public void CheckButtonsCost()
    {
        foreach (Upgrades upg in UpgradesArray)
        {
            upg.SetButton(Money >= upg.CurrentCost);
        }
    }
}
