using UnityEngine;
using System.Collections;

public class Spotlight : MonoBehaviour
{

    public GameObject ball;

    private Light lighting;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    lighting = GetComponent<Light>();
	}



    public void SetBall(GameObject ball)
    {
        this.ball = ball;
    }
}
