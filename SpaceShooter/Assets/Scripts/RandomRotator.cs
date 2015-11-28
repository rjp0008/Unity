using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{

    public float Tumble;

    void Start()
    {
        var rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere*Tumble;
    }
}
