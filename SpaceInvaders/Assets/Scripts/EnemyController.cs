using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    public GameObject enemy;
    private List<Transform> enemyTransforms = new List<Transform>();

    private float secondsBeforeMovement = 1;
    private float secondsSinceMovement = 0;
	// Use this for initialization
	void Start ()
	{
        var enemies = GameObject.FindGameObjectsWithTag("Baddie");
	    foreach (var enemy in enemies)
	    {
	        enemyTransforms.Add(enemy.GetComponent<Transform>());
	    }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        secondsSinceMovement += Time.deltaTime;
        if (secondsSinceMovement > secondsBeforeMovement)
        {
            secondsSinceMovement = 0;
            foreach (var enemyLocation in enemyTransforms)
            {
                enemyLocation.position += Vector3.right; 

            }
        }
    }
}
