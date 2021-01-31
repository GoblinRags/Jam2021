using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public UpgradeUI UpgUIScript;
    public int[] Costs = {50, 75, 100};
    public enum UpgTypes {Lens, Magnifier, Electrical, Gears, BatteryBoost, Parallax, Sas}
    public UpgTypes UpgType;
    public int UpgLevel;
    public int CurrentCost;
    
    private bool IsMaxed;

    AudioSource audioSource;
    private void Awake()
    {
        UpgradeManager.UpgradesArray.Add(this);
        CurrentCost = Costs[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = SoundManager.Instance.GetComponent<AudioSource>();
        
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
            case UpgTypes.Magnifier:
                //play sfx
                SoundManager.Instance.PlaySfx(12, .6f);
                break;
            case UpgTypes.Lens:
                //play sfx
                SoundManager.Instance.PlaySfx(6, 1f);
                break;
            case UpgTypes.Electrical:
                //play sfx
                SoundManager.Instance.PlaySfx(7, 1.3f);
                break;
            case UpgTypes.Gears:
                //play sfxs
                SoundManager.Instance.PlaySfx(8, .8f);
                break;
            case UpgTypes.Sas:
                //play sfx
                SoundManager.Instance.PlaySfx(11, .6f);
                break;
            case UpgTypes.BatteryBoost:
                //play sfx
                SoundManager.Instance.PlaySfx(9, .8f);
                break;
            case UpgTypes.Parallax:
                //play sfx
                SoundManager.Instance.PlaySfx(10, .8f);
                break;
        }
        UpgLevel += 1;
        UpgradeManager.Instance.Money -= CurrentCost;
        SetText();
        if (UpgLevel >= Costs.Length)
            IsMaxed = true;
        else
        {
            CurrentCost = Costs[UpgLevel];
            Tooltip.ShowToolTipStatic("Cost: $" + CurrentCost);
        }
            
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
    
    
    public void HoverButton()
    {
        ////NOTE: for STEVE to change sfx
        //if (UpgUIScript.BuyButton.IsInteractable())
        //    SoundManager.Instance.PlaySfx(5, 1f);
        //draw tooltip
        Tooltip.ShowToolTipStatic("Cost: $" + CurrentCost);

    }

    public void PressButton()
    {
        //change sfx-STEVE - default button sfx->play unique sfx in Upgrades Script
        SoundManager.Instance.PlaySfx(4, 1f);
    }
    
    public void ExitButton()
    {
        //hide tooltip
        Tooltip.HideToolTipStatic();
    }
}
