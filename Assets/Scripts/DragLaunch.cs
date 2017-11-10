using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour {

    private Ball ball;
    private Vector3 startPos, endPos;
    private float startTime, endTime;
    
	// Use this for initialization
	void Start () {
        ball = GetComponent<Ball>();
	}

    public void MoveStart(float xNudge)
    {
        if (ball.inPlay) { return; }
        float newXPos = transform.position.x + xNudge;

        if (newXPos >= -50 && newXPos <= 50) {
            transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);
        }
    }

    public void DragStart()
    { // Capture time & position of drag start
        if (ball.inPlay) { return; }

        startTime = Time.timeSinceLevelLoad;
        startPos = Input.mousePosition;
    }	

    public void DragEnd ()
    { // Launch the ball
        if (ball.inPlay) { return; }

        endTime = Time.timeSinceLevelLoad;
        endPos = Input.mousePosition;

        float swipeTime = endTime - startTime;
        float launchSpeedX = (endPos.x - startPos.x) / swipeTime;
        float launchSpeedZ = (endPos.y - startPos.y) / swipeTime;
        Vector3 launchVector = new Vector3(launchSpeedX, 0, launchSpeedZ);

        ball.Launch(launchVector);
    }

}
