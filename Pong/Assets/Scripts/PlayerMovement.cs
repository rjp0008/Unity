﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float MovementSpeed = 6f;

    private Rigidbody _rigidbody;
    private string movementAxis;
    private int playerNumber = 1;

	// Use this for initialization
	void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody>();
        movementAxis = "Vertical" + playerNumber;
    }
	
	// Update is called once per frame
	void Update ()
	{

	}

    void FixedUpdate()
    {
        Vector3 movement = transform.forward * Input.GetAxis(movementAxis) * MovementSpeed * Time.deltaTime;
        _rigidbody.MovePosition(movement + _rigidbody.position);

    }
}