using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Random = System.Random;

public class EnemyController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject barrier;
    public GameObject enemyShot;

    private List<Transform> enemyTransforms = new List<Transform>();


    private float secondsBeforeMovement = 1f;
    private float secondsSinceMovement = 0;

    private Vector3 currentHeading = Vector3.right;

    internal List<Transform> EnemyTransforms
    {
        get
        {
            return enemyTransforms.Where(x => x != null).ToList();
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

        foreach (var enemyLocation in EnemyTransforms.Where(enemyLocation => enemyLocation != null))
        {
            enemyLocation.position += currentHeading / 4;
        }
        ShootAtPlayer();

    }

    void NewWave()
    {
        enemyTransforms = new List<Transform>();
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
        secondsBeforeMovement = secondsBeforeMovement * .9f;
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
        return EnemyTransforms.Select(enemyTransform => enemyTransform.position.x);
    }

    private void SpawnBarriers()
    {
        for (float x = -4; x <= 4; x += 2.5f)
        {
            Instantiate(barrier, new Vector3(x, 1f, 0f), new Quaternion(0, 0, 0, 0));
        }
    }

    private void ShootAtPlayer()
    {
        var min = EnemyTransforms.Select(item => item.position.y).Min();
        var closestBaddies = EnemyTransforms.Where(item => item.position.y == min);
        Random rng = new Random();
        var firePos = closestBaddies.ElementAt((int)Math.Floor(rng.NextDouble()*closestBaddies.Count()));
        Instantiate(enemyShot, firePos.position, new Quaternion(0, 0, 0, 0));
    }
}
