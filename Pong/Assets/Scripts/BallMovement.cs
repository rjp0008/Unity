using UnityEngine;
using System.Collections;
using Microsoft.Win32;
using UnityEditor;

public class BallMovement : MonoBehaviour
{

    public float movementSpeed = .5f;

    private Rigidbody rb;
    private Transform tf;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        var initialVelocity = Random.insideUnitCircle*100;
        rb.AddForce(new Vector3(initialVelocity.x, 0, initialVelocity.y));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //if (Mathf.Abs(rb.velocity.x) < 1)
        //{
        //    var temp = rb.velocity;
        //    while (Mathf.Abs(temp.x) <1 )
        //    {
        //        temp.x *= temp.x*1.1f;
        //    }
        //    rb.AddForce(temp);
        //}
       }

  }
