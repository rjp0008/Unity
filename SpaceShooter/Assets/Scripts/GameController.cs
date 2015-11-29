using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{

    public GameObject Hazard;
    public GameObject Player;
    public int HazardCount;

    public float SpawnWait;
    public Vector3 SpawnValues;
    public float WaveWait;

    public Text scoreText;
    public int score;

    void Start()
    {
        StartCoroutine(SpawnWaves());
        score = 0;
        UpdateScore();
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

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

}
