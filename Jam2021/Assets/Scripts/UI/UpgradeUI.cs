using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Button BuyButton;
    public TextMeshProUGUI UpgradeLevelText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void PressButton()
    {
        //change sfx-STEVE - default button sfx->play unique sfx in Upgrades Script
        // IM NOT PLAYING CLICK SOUND HERE, EACH UPGRADE HAS A UNIQUE SFX
        SoundManager.Instance.PlaySfx(0, 1f);
    }
}
