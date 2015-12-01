using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour
{

    public float scrollSpeed;
    public float tileSizedZ;

    private Vector3 startPosition;

	// Use this for initialization
	void Start ()
	{
	    startPosition = GetComponent<Transform>().position;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float newPosition = Mathf.Repeat(Time.time * scrollSpeed,tileSizedZ);
	    var transform = GetComponent<Transform>();
	    transform.position = startPosition + Vector3.forward * newPosition;
	}
}
