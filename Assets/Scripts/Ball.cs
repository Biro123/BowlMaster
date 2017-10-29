using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Vector3 launchVelocity;
    public bool inPlay = false;

    private Rigidbody rigidBody;
    private AudioSource audioSource;
    

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        rigidBody.useGravity = false;

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
}
