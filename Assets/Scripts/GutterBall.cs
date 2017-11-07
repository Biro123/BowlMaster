using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GutterBall : MonoBehaviour {

    PinSetter pinSetter;
	// Use this for initialization
	void Start () {
        pinSetter = FindObjectOfType<PinSetter>();
	}
	
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            pinSetter.ballLeftBox = true;
        }
    }
}
