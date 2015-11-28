using System;
using UnityEngine;

[Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour
{

    public float Speed;
    public Boundary Boundary;
    public float tilt;

    public void FixedUpdate()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var movementVector = new Vector3(moveHorizontal, 0f, moveVertical);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = movementVector*Speed;
        rb.position = new Vector3
            (Mathf.Clamp(rb.position.x, Boundary.xMin, Boundary.xMax), 0.0f,
                Mathf.Clamp(rb.position.z, Boundary.yMin, Boundary.yMax)
            );

        rb.rotation = Quaternion.Euler(0f,0f,rb.velocity.x*-tilt);
    }
}
