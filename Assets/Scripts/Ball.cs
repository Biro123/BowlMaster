using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Vector3 launchVelocity;
    public bool inPlay = false;

    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private Vector3 startPosition;
    

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        rigidBody.useGravity = false;
        startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Launch(Vector3 Velocity)
    {
        inPlay = true;
        rigidBody.useGravity = true;
        rigidBody.velocity = Velocity;
        audioSource.Play();        
    }

    public void Reset()
    {
        inPlay = false;
        transform.position = startPosition;
        rigidBody.velocity = new Vector3(0, 0, 0);
        rigidBody.angularVelocity = new Vector3(0, 0, 0);
        rigidBody.useGravity = false;
    }
}
