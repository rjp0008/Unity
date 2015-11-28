using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{

    public GameObject Hazard;
    public GameObject Player;

    public Vector3 spawnValues;

    void Start()
    {
        SpawnWaves();
    }

    private void SpawnWaves()
    {
        var vec = new Vector3(Random.Range(-spawnValues.x,spawnValues.x),spawnValues.y,spawnValues.z);
        var quat = Quaternion.identity;
        Instantiate(Hazard, vec, quat);
    }
}
