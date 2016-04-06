using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public class EnemyController : MonoBehaviour
{
    public GameObject enemy;
    private List<Transform> enemyTransforms = new List<Transform>();


    private float secondsBeforeMovement = 1;
    private float secondsSinceMovement = 0;
	// Use this for initialization
	void Start ()
	{
       NewWave();
    }
	
	// Update is called once per frame
    void Update()
    {

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

    void NewWave()
    {
        for (int x = -5; x < 5; x+=2)
        {
            for (int y = 10; y > 7; y--)
            {
                Instantiate(enemy, new Vector3(x, y, 0),new Quaternion(0,0,90,10));
            }
        }
    }
}
