using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class FreezeTime : MonoBehaviour {
	
	// Handle freezing
	private bool freezeTime;
	public float movementBuffer;
	public float detectInterval;
	public AudioSource youMovedSound;

	// Hand positions
	private GameObject leftController;
	private GameObject rightController;
	private Vector3 lastLeftPos;
	private Vector3 lastRightPos;

	private LaserGopherBehavior laserGopherBehavior;

	// otherwise you are always detected for the first frame
	private bool firstFrameOfFreeze = true;

    // PP Damage Effect
    public float damageDuration;
    public PostProcessingProfile damagePPP;
    public AnimationCurve damageAC;
    public Color vignetteColor = new Vector4(0.1F, 0, 0, 1);

    private void Start ()
	{
		freezeTime = false;
		StartCoroutine(DetectMovement());
	}

	public void BeginFreezeTime(LaserGopherBehavior laserGopherBehavior)
	{
		// this is a problem if there are multiple gophers
		this.laserGopherBehavior = laserGopherBehavior;
		Debug.Log("begin freeze time");
		FetchHands();
		firstFrameOfFreeze = true;
		freezeTime = true;
	}

	public void EndFreezeTime()
	{
		Debug.Log("end freeze time");
		freezeTime = false;
	}

	private IEnumerator DetectMovement()
	{
		while (true)
		{
			if (freezeTime)
			{
				var newLeftPos = leftController.transform.position;
				var newRightPos = rightController.transform.position;
				if (firstFrameOfFreeze)
				{
					firstFrameOfFreeze = false;
				}
				else if (MovedTooMuch(newLeftPos, newRightPos))
				{
					Debug.Log("You moved!");
					laserGopherBehavior.AlertOn();
					youMovedSound.Play();
                    StartCoroutine(DamagePulse(vignetteColor));
                }
				else
				{
					laserGopherBehavior.AlertOff();
				}
				lastLeftPos = newLeftPos;
				lastRightPos = newRightPos;
			}
			yield return new WaitForSeconds(detectInterval);
		}
	}

	private bool MovedTooMuch(Vector3 newLeftPos, Vector3 newRightPos)
	{
		Debug.Log("Left hand movement: " + Vector3.Distance(lastLeftPos, newLeftPos));
		if (Vector3.Distance(lastLeftPos, newLeftPos)/detectInterval > movementBuffer)
		{
			return true;
		}
		return Vector3.Distance(lastRightPos, newRightPos)/detectInterval > movementBuffer;
	}

	private void FetchHands()
	{
		if (leftController != null && rightController != null) return;
		leftController = PlayerHelper.GetLeftHand();
		rightController = PlayerHelper.GetRightHand();
	}

    public IEnumerator DamagePulse(Color screenColor)
    {
        var vignetteValue = damagePPP.vignette.settings;
        vignetteValue.intensity = 0.5f;
        vignetteValue.color = screenColor;
        damagePPP.vignette.settings = vignetteValue;

        float journey = 0f;
        float oscillate = 0f;
        bool increasing = true;

        while (journey < damageDuration)
        {
            journey = journey + Time.deltaTime;
            if (journey < damageDuration*0.5f)
                oscillate += Time.deltaTime;
            else
                oscillate -= Time.deltaTime;

            float percent = Mathf.Abs(Mathf.Clamp01(oscillate / (damageDuration * 0.5f)));
            float curvePercent = damageAC.Evaluate(percent);
            //Debug.Log(curvePercent);
            vignetteValue.intensity = Mathf.Lerp(0.5f, 0.9f, curvePercent);
            damagePPP.vignette.settings = vignetteValue;

            yield return new WaitForSeconds(0.01f);
        }
    }
}
