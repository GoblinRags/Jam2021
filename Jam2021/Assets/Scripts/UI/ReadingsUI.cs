using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadingsUI : MonoBehaviour
{
    public bool IsStart;

    public float AnalyzeTime = 2f;

    public TextMeshProUGUI[] ElementNames;

    public TextMeshProUGUI[] ElementAmounts;

    private static readonly int IsAnalyze = Animator.StringToHash("IsAnalyze");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnalysis(List<Cosmics.Element> elements)
    {
        StartCoroutine(StartAnalyze(elements));
    }
    
    void StartParamaters(List<Cosmics.Element> elements)
    {
        IsStart = true;
    }

    public IEnumerator StartAnalyze(List<Cosmics.Element> elements)
    {
        SoundManager.Instance.PlaySfx(13, 1f);
        
        ResetVariables();
        SetNames(elements);
        float timer = 0f;
        float miniTimer = 0f;
        float miniInterval = .1f;
        IsStart = true;
        while (timer < AnalyzeTime)
        {
            timer += Time.deltaTime;
            miniTimer += Time.deltaTime;

            if (miniTimer >= miniInterval)
            {
                for (int i = 0; i < elements.Count; i++)
                {
                    ElementAmounts[i].text = Random.Range(0, 100) + "%";
                }
                miniTimer = 0f;
            }
            
            yield return null;
        }
        for (int i = 0; i < elements.Count; i++)
        {
            ElementAmounts[i].text = elements[i].Amount + "%";
        }
        IsStart = false;

        MainUIScript.Instance.CurState = MainUIScript.States.Analyzed;
        MainUIScript.Instance.PlanetAnimator.SetBool(IsAnalyze, false);
    }

    public void ResetVariables()
    {
        foreach (TextMeshProUGUI text in ElementNames)
        {
            text.text = "";
        }

        foreach (TextMeshProUGUI text in ElementAmounts)
        {
            text.text = "";
        }
    }

    public void SetNames(List<Cosmics.Element> elements)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            ElementNames[i].text = Cosmics.Element.Abbreviation[(int)elements[i].ElementType];
        }
    }
    
}
