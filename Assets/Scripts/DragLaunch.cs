using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour {

    private Ball ball;
    private Vector3 startPos;
    private Vector3 endPos;
    private float startTime;
    private float endTime;

	// Use this for initialization
	void Start () {
        ball = GetComponent<Ball>();
	}

    public void DragStart()
    { // Capture time & position of drag start
        startTime = Time.timeSinceLevelLoad;
        startPos = Input.mousePosition;
    }	

    public void DragEnd ()
    { // Launch the ball
        endTime = Time.timeSinceLevelLoad;
        endPos = Input.mousePosition;

        float swipeTime = endTime - startTime;
        float launchSpeedX = (endPos.x - startPos.x) / swipeTime;
        float launchSpeedZ = (endPos.y - startPos.y) / swipeTime;
        Vector3 launchVector = new Vector3(launchSpeedX, 0, launchSpeedZ);

        ball.Launch(launchVector);


    }

}
