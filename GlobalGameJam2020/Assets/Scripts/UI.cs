using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public float FadeDuration = 2f;
    public CanvasGroup Title;
    public CanvasGroup Credits;

    private bool gameStarted = false;

    public void Start()
    {
        Title.alpha = 1;
        Credits.alpha = 0;
    }
    public void Update()
    {
        if(Input.GetButton("Command") && !gameStarted)
        {
            gameStarted = true;
            StartCoroutine(Fade(Title, false));
        }
    }

    public void ShowCredits()
    {
        StartCoroutine(Fade(Credits, true));
    }


    private IEnumerator Fade(CanvasGroup canvas , bool show)
    {
        float step = Time.fixedDeltaTime  / FadeDuration;
        while (show ? canvas.alpha < 0.999f : canvas.alpha > 0.001f)
        {
            canvas.alpha += show ? step : -step;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }
}
