using UnityEngine;
using System.Collections;
using Microsoft.Win32;
using UnityEditor;

public class BallMovement : MonoBehaviour
{

    public float movementSpeed = .5f;

    private Rigidbody rb;
    private Transform tf;

    private float timeBetweenSpeedups = 5f;
    private float timeSinceLastSpeedup;
    private float speedupFactor = 1.1f;

    private GameManager manager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        var x = Random.Range(0, 2) == 0 ? -1 : 1;
        var y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector3(x, 0, y);
        manager = FindObjectOfType<GameManager>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        timeSinceLastSpeedup += Time.deltaTime;
        if (timeSinceLastSpeedup > timeBetweenSpeedups)
        {
            timeSinceLastSpeedup = 0;
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z) * speedupFactor;
        }
    }
    
    

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            manager.PlayerScored(rb.transform.position.x);
            manager.StartRound();
        }
    }
}
