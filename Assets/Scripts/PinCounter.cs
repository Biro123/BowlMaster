using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {

    public Text standingDisplay;
    public bool ballLeftBox = false;

    private GameManager gameManager;
    private int lastStandingCount = -1;  // default state
    private float lastChangeTime;
    private int lastSettledCount = 10;

    // Use this for initialization
    void Start () {
        gameManager = FindObjectOfType<GameManager>();        
    }
	
	// Update is called once per frame
	void Update () {
        standingDisplay.text = CountStanding().ToString();

        if (ballLeftBox)
        {
            standingDisplay.color = Color.red;
            CheckStanding();
        }
    }

    public void PinCountReset()
    {
        lastSettledCount = 10;
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
        gameManager.Bowl(pinFall);
        Debug.Log("going green");
        standingDisplay.color = Color.green;
        lastStandingCount = -1; // indicates pins have settled
        ballLeftBox = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            ballLeftBox = true;
        }
    }

}
