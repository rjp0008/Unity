using UnityEngine;
using System.Collections;

public class LaserBolt : MonoBehaviour
{
    
    public float boltSpeed;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
	rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if (Mathf.Abs(rb.position.y) > 15)
        {
            Destroy(gameObject);
        }
        else
        {
            rb.transform.position += new Vector3(0,boltSpeed * Time.deltaTime,0);
        }
    }
}
