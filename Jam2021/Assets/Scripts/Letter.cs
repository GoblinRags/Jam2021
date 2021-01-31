using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public enum Phases {FadeIn, Start, CanClick, Fadeout, Done}

    public Phases CurPhase = Phases.FadeIn;

    private bool CanPush;

    public float PushTime = .1f;
    public float PushTimer = 0f;
    public Vector2 PushIntervals = new Vector2(.15f, .3f);

    public float PushAmount = .2f;
    public float FinalLetterPos = 4f;

    public Transform LetterPos;

    public Transform Machine;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartScene());
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurPhase)
        {
            case Phases.FadeIn:

                break;
            case Phases.Start:
                PushTimer += Time.deltaTime;

                if (PushTimer >= PushTime)
                {
                    SoundManager.Instance.PlaySfx(3, 1f);
                    Vector3 newPos = LetterPos.transform.position;
                    newPos.y = PushAmount + LetterPos.transform.position.y;
                    newPos.y = Mathf.Min(newPos.y, FinalLetterPos);
                    LetterPos.transform.position = newPos;
                    PushTimer = 0f;
                    PushTime = Random.Range(PushIntervals.x, PushIntervals.y);
                    if (newPos.y >= FinalLetterPos)
                    {
                        CurPhase = Phases.CanClick;
                    }
                }
                break;
            case Phases.CanClick:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    CurPhase = Phases.Fadeout;
                    FindObjectOfType<FadeScenes>().FadeIn("Sandbox1");
                }
                break;
            case Phases.Fadeout:
                Vector3 pos = Vector3.zero;
                pos.y = PushAmount * 6f * Time.deltaTime + LetterPos.transform.position.y;
                LetterPos.transform.position = pos;
                Machine.transform.position -= new Vector3(0f, PushAmount / 2f * Time.deltaTime, 0f);
                break;

        }
    }
    
    public IEnumerator StartScene()
    {
        FadeScenes fadeScene = FindObjectOfType<FadeScenes>();
        yield return fadeScene.StartCoroutine(fadeScene.Fade(FadeScenes.FadeDir.Out));
        yield return new WaitForSecondsRealtime(1f);
        CurPhase = Phases.Start;
    }
}
