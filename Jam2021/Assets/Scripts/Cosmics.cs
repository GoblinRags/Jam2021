using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cosmics : MonoBehaviour
{
    public List<Element> Elements = new List<Element>();
    public enum Type {Unknown, Asteroid, Star, Planet}

    public Type CurType = Type.Unknown;
    public bool IsHabitable;

    public bool SettedVariables;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValues()
    {
        if (SettedVariables || MainUIScript.Instance.Readings.IsStart)
            return;

        SettedVariables = true;
        
        if (CurType != Type.Star)
        {
            float rand = Random.Range(0f, 1f);

            if (rand <= .65f)
            {
                CurType = Type.Planet;
            }
            else if (rand <= .75f)
            {
                CurType = Type.Star;
            }
            else
                CurType = Type.Asteroid;
            
        }


        switch (CurType)
        {
            
            case Type.Star:
                //Hydrogen
                Element ele1 = new Element();
                ele1.ElementType = Element.Types.Hydrogen;
                ele1.Amount = Random.Range(50, 75);
                Elements.Add(ele1);
                
                //Oxygen
                Element ele2 = new Element();
                ele2.ElementType = Element.Types.Helium;
                ele2.Amount = Random.Range(10, 25);
                Elements.Add(ele2);
                break;
            case Type.Asteroid:
                Element a1 = new Element();
                a1.ElementType = Element.Types.Hydrogen;
                a1.Amount = Random.Range(5, 10);
                Elements.Add(a1);
                Element a2 = new Element();
                a2.ElementType = Element.Types.Oxygen;
                a2.Amount = Random.Range(5, 10);
                Elements.Add(a2);
                Element a3 = new Element();
                a3.ElementType = Element.Types.Carbon;
                a3.Amount = Random.Range(50, 60);
                Elements.Add(a3);
                break;  
            case Type.Planet:
                float randi = Random.Range(0f, 1f);
                if (randi <= .15f)
                {
                    Element ele3 = new Element();
                    ele3.ElementType = Element.Types.Hydrogen;
                    ele3.Amount = Random.Range(50, 75);
                    Elements.Add(ele3);
                
                    //Oxygen
                    Element ele4 = new Element();
                    ele4.ElementType = Element.Types.Helium;
                    ele4.Amount = Random.Range(10, 25);
                    Elements.Add(ele4);
                }
                else if (randi <= .3f)
                {
                    Element ele3 = new Element();
                    ele3.ElementType = Element.Types.Carbon;
                    ele3.Amount = Random.Range(50, 85);
                    Elements.Add(ele3);
                
                    //Oxygen
                    Element ele4 = new Element();
                    ele4.ElementType = Element.Types.Water;
                    ele4.Amount = Random.Range(2, 15);
                    Elements.Add(ele4);
                }
                else
                {
                    Element ele3 = new Element();
                    ele3.ElementType = Element.Types.Oxygen;
                    ele3.Amount = Random.Range(15, 25);
                    Elements.Add(ele3);
                
                    //Oxygen
                    Element ele4 = new Element();
                    ele4.ElementType = Element.Types.Water;
                    ele4.Amount = Random.Range(15, 30);
                    Elements.Add(ele4);
                    
                    Element ele5 = new Element();
                    ele5.ElementType = Element.Types.Carbon;
                    ele5.Amount = Random.Range(5, 10);
                    Elements.Add(ele5);
                    IsHabitable = true;
                }
                

                break;
        }
        MainUIScript.Instance.StartAnalyze(Elements, this);
    }

    public struct Element
    {
        public enum Types {Oxygen, Hydrogen, Helium, Carbon, Water}
        public static string[] Abbreviation = {"O", "H", "He", "C", "H20"};
        public Types ElementType;
        public int Amount;
    }
}
