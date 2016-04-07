using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public class EnemyController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject barrier;

    private List<Transform> enemyTransforms = new List<Transform>();


    private float secondsBeforeMovement = 1f;
    private float secondsSinceMovement = 0;

    private Vector3 currentHeading = Vector3.right;

    internal List<Transform> EnemyTransforms
    {
        get
        {
            var temp = new List<Transform>();
            foreach (var enemyTransform in enemyTransforms)
            {
                if (enemyTransform != null)
                {
                    temp.Add(enemyTransform);
                }
            }
            enemyTransforms = temp;
            return enemyTransforms;
        }
        set { enemyTransforms = value; }
    }

    // Use this for initialization
    void Start()
    {
        NewWave();
        SpawnBarriers();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        secondsSinceMovement += Time.deltaTime;

        if (!(secondsSinceMovement > secondsBeforeMovement)) return;

        secondsSinceMovement = 0;

        if (FurthestRightLocation() > 5f || FurthestLeftLocation() < -5f)
        {
            DropDown();
        }

        foreach (var enemyLocation in enemyTransforms.ToList().Where(enemyLocation => enemyLocation != null))
        {
            enemyLocation.position += currentHeading / 4;
        }

    }

    void NewWave()
    {
        for (var x = -5; x < 5; x += 2)
        {
            for (var y = 10; y > 7; y--)
            {
                var enemyInstance = (GameObject)Instantiate(enemy, new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
                enemyTransforms.Add(enemyInstance.transform);
                enemyInstance.transform.Rotate(Vector3.forward, 90);
            }
        }
    }

    void DropDown()
    {
        foreach (var enemyLocation in enemyTransforms.ToList())
        {
            if (enemyLocation != null)
            {
                enemyLocation.position += Vector3.down / 4;
            }
        }

        currentHeading = currentHeading == Vector3.right ? Vector3.left : Vector3.right;
    }

    private float FurthestRightLocation()
    {
        return AllXPositions().Max();
    }

    private float FurthestLeftLocation()
    {
        return AllXPositions().Min();
    }

    private IEnumerable<float> AllXPositions()
    {
        return enemyTransforms.Where(x => x != null).Select(enemyTransform => enemyTransform.position.x);
    }

    private void SpawnBarriers()
    {
        for (float x = -4; x <= 4; x+=2.5f)
        {
            Instantiate(barrier, new Vector3(x, 1f, 0f), new Quaternion(0, 0, 0, 0));
        }
    }
}
