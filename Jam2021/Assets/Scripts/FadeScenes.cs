using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScenes : MonoBehaviour
{

    public RawImage FadeOutImage;

    public float FadeInTime; //how long to fade completely
    public float FadeOutTime = 7f;
    public enum FadeDir {In, Out}
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Fade(FadeDir.In));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public IEnumerator Fade(FadeDir dir)
    {
        float timer = 0f;
        float alpha = (dir == FadeDir.Out)? 1 : 0;
        float alphaEnd = (dir == FadeDir.In) ? 1 : 0;
        FadeOutImage.enabled = true;
        if (dir == FadeDir.In)
        {
            while (timer < FadeInTime)
            {
                timer += Time.deltaTime;
                float t = timer / FadeInTime;
                float newAlpha = Mathf.Lerp(alpha, alphaEnd, Mathf.SmoothStep(0f, 1f, t));
                SetAlpha(newAlpha);
                yield return null;
            }
        }
        else
        {
            while (timer < FadeOutTime)
            {
                timer += Time.deltaTime;
                float t = timer / FadeOutTime;
                float newAlpha = Mathf.Lerp(alpha, alphaEnd, Mathf.SmoothStep(0f, 1f, t));
                SetAlpha(newAlpha);
                yield return null;
            }
        }
        FadeOutImage.enabled = dir == FadeDir.In;
    }

    private void SetAlpha(float alpha)
    {
        Color color = Color.black;
        color.a = alpha;
        FadeOutImage.color = color;
    }
    
    public IEnumerator FadeAndLoadScene(FadeDir dir, string sceneToLoad) 
    {
        yield return Fade(dir);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void FadeIn(string scene)
    {
        StartCoroutine(FadeAndLoadScene(FadeDir.In, scene));
    }
}
