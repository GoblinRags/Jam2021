using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUIScript : MonoBehaviour
{
    public static MainUIScript Instance;
    
    //public 
    public ReadingsUI Readings;
    public Animator PlanetAnimator;
    public TextMeshProUGUI TimerText;
    public GameObject Panel;
    public Image Planet;
    public Sprite[] PlanetSprites;
    private static readonly int IsAnalyze = Animator.StringToHash("IsAnalyze");
    public States CurState = States.Beginning;
    public Cosmics CurrentCosmic;

    public TextMeshProUGUI OutComeText;
    public enum States
    {
        Beginning,
        Analyzed,
        Analyzing,
        SecondAnalysis
    }
    void Awake()
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
        if (CurState == States.Analyzed)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CurState = States.SecondAnalysis;
                SecondAnalysis();
            }
        }
    }

    public void StartAnalyze(List<Cosmics.Element> elements, Cosmics obj)
    {
        CurrentCosmic = obj;
        Planet.color = Color.clear;
        //Planet.color = obj.CurType == Cosmics.Type.Planet ? Color.white : Color.clear;
        Panel.SetActive(true);
        PlanetAnimator.SetBool(IsAnalyze, true);
        Readings.StartAnalysis(elements);
        CurState = States.Analyzing;
    }

    public void SecondAnalysis()
    {
        SoundManager.Instance.PlaySfx(13, 1f);
        StartCoroutine(SecondAnalyze());
    }

    public IEnumerator SecondAnalyze()
    {
        CurState = States.Analyzing;
        PlanetAnimator.SetBool(IsAnalyze, true);
        Planet.color = CurrentCosmic.CurType == Cosmics.Type.Planet ? Color.white : Color.clear;
        yield return new WaitForSeconds(2f);

        StartCoroutine(SetOutCome());
        PlanetAnimator.SetBool(IsAnalyze, false);

    }

    public IEnumerator SetOutCome()
    {
        
        switch (CurrentCosmic.CurType)
        {
            case Cosmics.Type.Star:
                OutComeText.text = "Found Star";
                break;
            case Cosmics.Type.Planet:
                if (CurrentCosmic.IsHabitable)
                {
                    OutComeText.text = "Found Habitable Planet: +$100";
                    UpgradeManager.Instance.AddMoney(100);
                }
                else
                    OutComeText.text = "Found Inhabitable Planet";
                break;
            case Cosmics.Type.Asteroid:
                OutComeText.text = "Found Asteroid";
                break;
            
        }

        OutComeText.enabled = true;
        yield return new WaitForSeconds(.2f);
        OutComeText.enabled = false;
        yield return new WaitForSeconds(.1f);
        OutComeText.enabled = true;
        yield return new WaitForSeconds(.2f);
        OutComeText.enabled = false;
        yield return new WaitForSeconds(.1f);
        OutComeText.enabled = true;
        yield return new WaitForSeconds(3f);
        CurState = States.SecondAnalysis;
        yield return new WaitForSeconds(5f);
        OutComeText.enabled = false;
        
    }
}
