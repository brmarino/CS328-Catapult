﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resetter : MonoBehaviour {

    public Rigidbody2D projectile;
    public float resetSpeed = 0.025f;

    private float resetSpeedSqr;
    private SpringJoint2D spring;

	// Use this for initialization
	void Start () {

        resetSpeedSqr = resetSpeed * resetSpeed;
        spring = projectile.GetComponent<SpringJoint2D>();

	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown (KeyCode.R))
        {
            Reset();
        }

        if(spring == null && projectile.velocity.sqrMagnitude < resetSpeedSqr)
        {

            Invoke("Reset", 2);
        }

	}

    void OnTriggerExit2D(Collider2D obj)
    {
        string name = obj.gameObject.name;

        if (name == "PigHead")
        {
            Invoke("Reset", 2);
        }
    }

    void Reset()
    {
        SceneManager.LoadScene("Level1");
    }
}