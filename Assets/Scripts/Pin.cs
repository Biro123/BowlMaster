using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingThreshold = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public bool IsStanding()
    {
        // eulerAngles are how you see them in unity - am angle from each vertex
        Vector3 rotationInEuler = transform.rotation.eulerAngles;

        float tiltInX = Mathf.Abs(rotationInEuler.x); // abs ensures positive only
        float tiltInZ = Mathf.Abs(rotationInEuler.z);
        tiltInX = tiltInX - 270f;    // handle default X position of 270 

        if (tiltInX > 180) { tiltInX = 360f - tiltInX; }
        if (tiltInZ > 180) { tiltInZ = 360f - tiltInZ; }

        if (tiltInX <= standingThreshold && tiltInZ <= standingThreshold)
        {
            return true;
        }
        return false;
    }

}
