using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{

    public GameObject Hazard;
    public GameObject Player;
    public int HazardCount;

    public float SpawnWait;
    public Vector3 SpawnValues;
    public float WaveWait;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            for (var i = 0; i < HazardCount; i++)
            {
                var vec = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
                var quat = Quaternion.identity;
                Instantiate(Hazard, vec, quat);
                yield return new WaitForSeconds(SpawnWait);
            }
            yield return new WaitForSeconds(WaveWait);
        }
    }

}
