using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSetter : MonoBehaviour {
    
    public GameObject pinSet;

    private Animator animator;
    private PinCounter pinCounter;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        pinCounter = FindObjectOfType<PinCounter>();
    }
	
    public void PerformAction (ActionMaster3.Action action)
    {
        if (action == ActionMaster3.Action.Tidy)
        {
            animator.SetTrigger("tidyTrigger");
        }
        else if (action == ActionMaster3.Action.Reset)
        {
            animator.SetTrigger("resetTrigger");
            pinCounter.PinCountReset();
        }
        else if (action == ActionMaster3.Action.EndTurn)
        {
            animator.SetTrigger("resetTrigger");
            pinCounter.PinCountReset();
        }
        else if (action == ActionMaster3.Action.EndGame)
        {
            throw new UnityException("Don't know how to handle endgame");
        }
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
