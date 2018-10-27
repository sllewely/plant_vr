using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {
    public Text tutorialText;
    private string[] tutorialDisplays = new string[] {
        "Welcome to Nom Nom Garden!\nYou are now a carnivorous plant.\nPull trigger to continue.",
        "Squeeze and release the triggers \non the front of each controller \nto close and open your hands.",
        "Now try picking up the fly to your right.",
        "Eat the fly! \nBring it to your face and \ndon't let go until it's gone.",
        "Try catching a fly from the air and eating it",
        "When you eat a fly, your food-o-meter will increase.\nFill the meter to win, and don't let it run out!\nFill the food meter now by catching and eating flies.",
        "Watch out for the Herbicidal Gopher!\nWhen he pops up, freeze so he doesn't notice you.\nIf he sprays you three times, you lose!",
        "You're ready! Pull both triggers to start the game."
    };
    private int tutorialStage = 0;

    public GameObject leftHand;
    public GameObject rightHand;

    private SteamVR_TrackedObject leftTrackedObject;
    private SteamVR_TrackedObject rightTrackedObject;

    private SteamVR_Controller.Device leftDevice;
    private SteamVR_Controller.Device rightDevice;

    private AudioSource nextChime;

    public GameObject GameManager;
    private GameState gameState;
    public GameObject tutorialFly;
    private Vector3 flyTransformPos;
    public GameObject waspSpawner;
    public GameObject sprinkler;

    public float sprinklerWaitTime = 10f;
    private float sprinklerTimeLeft;

    private bool ateFly;

    //Stage 1 advancement criteria
    private bool[] stage1triggers = new bool[] { false, false, false, false };

    // Use this for initialization
    void Start () {
        tutorialText.text = tutorialDisplays[tutorialStage];
        nextChime = GetComponent<AudioSource>();
        FetchHands();
        leftTrackedObject = leftHand.GetComponent<SteamVR_TrackedObject>();
        rightTrackedObject = rightHand.GetComponent<SteamVR_TrackedObject>();
        gameState = GameManager.GetComponent<GameState>();

        Debug.Log("Start Tutorial");
        flyTransformPos = tutorialFly.transform.position;

        sprinklerTimeLeft = sprinklerWaitTime;

    }

    // Update is called once per frame
    void Update () {
        leftDevice = SteamVR_Controller.Input((int)leftTrackedObject.index);
        rightDevice = SteamVR_Controller.Input((int)rightTrackedObject.index);

        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("MASTER_scene", LoadSceneMode.Single);
        }

        switch (tutorialStage)
        {
            case 0: // Stage 0: Start
                if (anyTrigger())
                {
                    advanceStage();
                }
                break;
            case 1: // Stage 1: Use both triggers
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
                tutorialFly.SetActive(true);
                ateFly = false;
                advanceStage();
                break;
            case 2: // Stage 2: Pick up fly
                if (leftDevice.GetPress(SteamVR_Controller.ButtonMask.Trigger)
                    && (leftHand.GetComponent<Grab>().GetHeldObject() == tutorialFly)
                    || rightDevice.GetPress(SteamVR_Controller.ButtonMask.Trigger)
                    && (rightHand.GetComponent<Grab>().GetHeldObject() == tutorialFly))
                    advanceStage();
                break;
            case 3: // Stage 3: Eat the fly
                //TODO: "on" style event instead?
                if (ateFly) {
                    ateFly = false;
                    waspSpawner.SetActive(true);
                    advanceStage();
                }
                //TODO: don't check every update -- only check when letting go?
                if (leftHand.GetComponent<Grab>().GetHeldObject() != tutorialFly
                    && rightHand.GetComponent<Grab>().GetHeldObject() != tutorialFly)
                    tutorialFly.transform.position = flyTransformPos;
                break;
            case 4: // Stage 4: Eat a moving fly
                if (ateFly)
                {
                    ateFly = false;
                    gameState.drainDelayEasy = 3; // Start drain
                    gameState.SetEasyDifficulty();
                    advanceStage();
                }
                break;
            case 5: // Stage 5: Fill the meter
                if (gameState.GetScore() < 3)
                {
                    gameState.SetScore(3);
                } else if (gameState.GetScore() >= gameState.targetScoreEasy)
                {
                    sprinkler.SetActive(true);
                    advanceStage();
                }
                break;
            case 6: // Stage 6: Freeze
                if (sprinklerTimeLeft <= 0)
                    advanceStage();
                else
                    sprinklerTimeLeft -= Time.deltaTime;
                break;
            case 7: // Stage 7: Squeeze both triggers to start game
                if(leftDevice.GetPress(SteamVR_Controller.ButtonMask.Trigger)
                    && rightDevice.GetPress(SteamVR_Controller.ButtonMask.Trigger))
                    SceneManager.LoadScene("MASTER_scene", LoadSceneMode.Single);
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

    public void eat()
    {
        ateFly = true;
    }


}
