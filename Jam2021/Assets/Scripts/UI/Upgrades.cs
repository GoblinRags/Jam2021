using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public UpgradeUI UpgUIScript;
    public int[] Costs = {50, 75, 100};
    public enum UpgTypes {Lens, Electrical, Gears, BatteryBoost, Parallax, Sas}
    public UpgTypes UpgType;
    public int UpgLevel;
    public int CurrentCost;
    
    private bool IsMaxed;
    private void Awake()
    {
        UpgradeManager.UpgradesArray.Add(this);
        CurrentCost = Costs[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Upgrade()
    {
        //NOTE: For steve to add sfx
        switch (UpgType)
        {
            case UpgTypes.Lens:
                //play lens sfx
                break;
            case UpgTypes.Electrical:
                //play sfx -etc
                break;
        }
        UpgLevel += 1;
        UpgradeManager.Instance.Money -= CurrentCost;
        SetText();
        if (UpgLevel >= Costs.Length)
            IsMaxed = true;
        else
            CurrentCost = Costs[UpgLevel];
        UpgradeManager.Instance.CheckButtonsCost();
    }

    void SetText()
    {
        //int level = UpgLevel;
        //if (!IsGadget)
        //    level += 1;
        UpgUIScript.UpgradeLevelText.text = UpgLevel.ToString();
    }

    public void SetButton(bool enoughMoney)
    {
        UpgUIScript.BuyButton.interactable = enoughMoney && !IsMaxed;
    }
}
