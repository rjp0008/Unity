using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public GameObject bolt;

    private Rigidbody rb;
    private float speed = 2f;

    private float timeBetweenShots = 1f;
    private float timeSinceLastShot;
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
        Move();
        Shoot();
    }

    private void Shoot()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot > timeBetweenShots)
        {
            float input = Input.GetAxis("Vertical");
            if (input != 0)
            {
                timeSinceLastShot = 0;
                Instantiate(bolt,rb.position+new Vector3(0,.2f,0),rb.rotation);
            }
        }
    }

    private void Move()
    {
        float input = Input.GetAxis("Horizontal");
        rb.transform.position = rb.transform.position + new Vector3( input * Time.deltaTime * speed, 0, 0);
    }
    
}
