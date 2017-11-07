using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public Text standingDisplay;
    public GameObject pinSet;
    public bool ballLeftBox = false;

    private Ball ball;
    private Animator animator;
    ActionMaster actionMaster;
    private int lastStandingCount = -1;  // default state
    private float lastChangeTime;
    private int lastSettledCount = 10;

	// Use this for initialization
	void Start () {
        actionMaster = new ActionMaster();
        ball = FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        standingDisplay.text = CountStanding().ToString();

        if (ballLeftBox)
        {
            CheckStanding();
            standingDisplay.color = Color.red;
        }
	}

    // Update count of standing pins and determine when settled.
    private void CheckStanding()
    {
        int currentStanding = CountStanding();

        if (currentStanding != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;
            return;
        }

        float settleTime = 3f;   // time to wait to consider pins settled
        if (Time.time - lastChangeTime > settleTime)
        {
            PinsHaveSettled();
        }
    }

    private void PinsHaveSettled()
    {
        int pinFall = lastSettledCount - CountStanding();
        lastSettledCount = lastSettledCount - pinFall;
        ActionMaster.Action actionResult = actionMaster.Bowl(pinFall);

        if (actionResult == ActionMaster.Action.Tidy)
        {
            animator.SetTrigger("tidyTrigger");
        }
        else if (actionResult == ActionMaster.Action.Reset)
        {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        }
        else if (actionResult == ActionMaster.Action.EndTurn)
        {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        }
        else if (actionResult == ActionMaster.Action.EndGame)
        {
            throw new UnityException("Don't know how to handle endgame");
        }

        
        ball.Reset();
        standingDisplay.color = Color.green;
        lastStandingCount = -1; // indicates pins have settled
        ballLeftBox = false;
    }

    public int CountStanding()
    {
        int standingCount = 0;
        Pin[] Pins = FindObjectsOfType<Pin>();

        foreach (Pin currentPin in Pins)
        {
            if (currentPin.IsStanding())
            {
                standingCount++;
            }
        }
        return standingCount;
    }

    public void RaisePins()
    {
        Pin[] Pins = FindObjectsOfType<Pin>();
        foreach (Pin currentPin in Pins)
        {
            currentPin.RaiseIfStanding();
        }
    } 

    public void LowerPins()
    {
        Pin[] Pins = FindObjectsOfType<Pin>();
        foreach (Pin currentPin in Pins)
        {
            currentPin.Lower();
        }
    }

    public void RenewPins()
    {
        Instantiate(pinSet, new Vector3(0, 0, 1829), Quaternion.identity );
    }

}
