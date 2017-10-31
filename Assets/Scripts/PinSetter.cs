using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public Text standingDisplay;

    private bool ballEnteredBox = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        standingDisplay.text = CountStanding().ToString();
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

    public int CountStanding()
    {
        int standingCount = 0;
        Pin[] Pins = FindObjectsOfType<Pin>();

        foreach (Pin currentPin in Pins)
        {
            if ( currentPin.IsStanding() )
            {
                standingCount++;
            }
        }
        return standingCount;
    }

}
