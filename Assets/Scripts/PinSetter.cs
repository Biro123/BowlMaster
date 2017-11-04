﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public Text standingDisplay;
    public int lastStandingCount = -1;  // default state
    public float distanceToRaise = 40f;

    private Ball ball;
    private bool ballEnteredBox = false;
    private float lastChangeTime;

	// Use this for initialization
	void Start () {
        ball = FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
        standingDisplay.text = CountStanding().ToString();

        if (ballEnteredBox)
        {
            CheckStanding();

        }
	}

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
        ball.Reset();
        standingDisplay.color = Color.green;
        lastStandingCount = -1; // indicates pins have settled
        ballEnteredBox = false;
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
        // Raise standing pins only by distanceToRaise
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
        Debug.Log("Renewing Pins");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ball>())
        {
            ballEnteredBox = true;
            standingDisplay.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Pin>())
        {
            Destroy(other.gameObject);
        }
    }


}
