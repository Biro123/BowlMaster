﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float launchSpeed = 350;

    private Rigidbody rigidBody;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        Launch(launchSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Launch(float launchSpeed)
    {
        rigidBody.velocity = new Vector3(0, 0, launchSpeed);
        audioSource.Play();        
    }
}