using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    private float speed = 2f;
	// Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        float input = Input.GetAxis("Horizontal");
        rb.transform.position = rb.transform.position + new Vector3(-1*input*Time.deltaTime*speed, 0, 0);
    }
}
