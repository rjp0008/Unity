using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour
{

    public float startingSpeed = 1;

    private Rigidbody rb;
    private Transform tf;
    
	// Use this for initialization
	void Start ()
	{
	    startingSpeed = ++startingSpeed;
	    rb = GetComponent<Rigidbody>();
        rb.AddForce(Random.insideUnitSphere);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        while (rb.velocity.z + rb.velocity.x < startingSpeed)
        {
            rb.AddForce(rb.velocity*2);
        }

    }
}
