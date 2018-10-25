using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class DamagePP : MonoBehaviour
{

    // Use this for initialization
    public float duration;
    public PostProcessingProfile ppp;
    public AnimationCurve ac;
    public Color vignetteColor = new Vector4(0.1F, 0, 0, 1);

    void Start()
    {
        startVignettePulse();
    }
    public void startVignettePulse()
    {
        StartCoroutine(VignettePulse(vignetteColor));
    }

    public IEnumerator VignettePulse(Color screenColor)
    {
        //Change vignette
        var vignetteValue = ppp.vignette.settings;
        vignetteValue.intensity = 0.5f;
        vignetteValue.color = screenColor;
        ppp.vignette.settings = vignetteValue;

        float journey = 0f;
        bool increasing = true;

        while (journey>=0)
        {

            if (increasing)
            {
                journey = journey + Time.deltaTime;
                if (journey >= duration)
                    increasing = false;
            }
            else
            {
                journey = journey - Time.deltaTime;
                if (journey <= 0)
                    increasing = true;
            }

            float percent = Mathf.Abs(Mathf.Clamp01(journey / duration));
            float curvePercent = ac.Evaluate(percent);
            //Debug.Log(curvePercent);
            vignetteValue.intensity = Mathf.Lerp(0.5f, 0.9f, curvePercent);
            ppp.vignette.settings = vignetteValue;

            yield return new WaitForSeconds(0.01f);
        }
    }
}