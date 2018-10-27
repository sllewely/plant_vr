using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {
    public Text tutorialText;
    private string[] tutorialDisplays = new string[] {
        "Welcome to Nom Nom Garden!\nYou are now a carnivorous plant.\nPull trigger to continue.",
        "Squeeze and release the triggers \non the front of each controller \nto close and open your hands.",
        "Now try picking up the fly.",
        "Eat the fly! \nBring it to your face and \ndon't let go until it's gone.",
        "Try catching a fly from the air and eating it",
        "When you eat a fly, your food-o-meter will increase.\nFill the meter to win, and don't let it run out!\nFill the food meter now by catching and eating flies.",
        "Watch out for the Herbicidal Gopher!\nWhen he pops up, freeze so he doesn't notice you.\nIf he sprays you three times, you lose!",
        "You're ready! Press any button to start the game."
    };
    private int tutorialStage = 0;

    public GameObject leftHand;
    public GameObject rightHand;

    private SteamVR_TrackedObject leftTrackedObject;
    private SteamVR_TrackedObject rightTrackedObject;

    private SteamVR_Controller.Device leftDevice;
    private SteamVR_Controller.Device rightDevice;

    private AudioSource nextChime;

    public GameObject tutorialFly;
    private Transform flyTransform;

    //Stage 1 advancement criteria
    private bool[] stage1triggers = new bool[] { false, false, false, false };

    // Use this for initialization
    void Start () {
        tutorialText.text = tutorialDisplays[tutorialStage];
        nextChime = GetComponent<AudioSource>();
        FetchHands();
        leftTrackedObject = leftHand.GetComponent<SteamVR_TrackedObject>();
        rightTrackedObject = rightHand.GetComponent<SteamVR_TrackedObject>();

        Debug.Log("Start Tutorial");
        flyTransform = tutorialFly.transform;

    }

    // Update is called once per frame
    void Update () {
        leftDevice = SteamVR_Controller.Input((int)leftTrackedObject.index);
        rightDevice = SteamVR_Controller.Input((int)rightTrackedObject.index);

        switch (tutorialStage)
        {
            case 0:
                if (anyTrigger())
                {
                    advanceStage();
                }
                break;
            case 1:
                if (leftDevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                    stage1triggers[0] = true;
                if (rightDevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                    stage1triggers[1] = true;
                if (leftDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                    stage1triggers[2] = true;
                if (rightDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                    stage1triggers[3] = true;
                foreach (bool b in stage1triggers)
                    if (!b)
                        return;
                advanceStage();
                break;
            case 2:
                if (leftDevice.GetPress(SteamVR_Controller.ButtonMask.Trigger) 
                    && (leftHand.GetComponent<Grab>().GetHeldObject() == tutorialFly)
                    || rightDevice.GetPress(SteamVR_Controller.ButtonMask.Trigger) 
                    && (rightHand.GetComponent<Grab>().GetHeldObject() == tutorialFly))
                    advanceStage();
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            default:
                Debug.Log("Invalid tutorial stage");
                break;
        }
    }

    private void advanceStage()
    {
        tutorialStage += 1;
        tutorialText.text = tutorialDisplays[tutorialStage];
        Debug.Log("Tutorial stage " + tutorialStage);
        nextChime.Play();
    }

    private void FetchHands()
    {
        if (leftHand != null && rightHand != null) return;
        leftHand = PlayerHelper.GetLeftHand();
        rightHand = PlayerHelper.GetRightHand();
    }

    private bool anyTrigger()
    {
        return (
            leftDevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) ||
            rightDevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)
            );
    }
}
