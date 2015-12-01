using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{

    public GameObject[] Hazards;
    public GameObject Player;
    public int HazardCount;

    public float SpawnWait;
    public Vector3 SpawnValues;
    public float WaveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameoverText;

    private int score;
    private bool restart;
    private bool gameOver;

    void Start()
    {
        StartCoroutine(SpawnWaves());
        SetupNewGame();
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKey(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    void SetupNewGame()
    {
        gameOver = false;
        gameoverText.text = "";

        score = 0;
        UpdateScore();

        restartText.text = "";
    }


    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            for (var i = 0; i < HazardCount; i++)
            {
                var vec = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
                var quat = Quaternion.identity;
                Instantiate(Hazards[Random.Range(0,Hazards.Length)], vec, quat);
                yield return new WaitForSeconds(SpawnWait);
            }
            yield return new WaitForSeconds(WaveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for restart.";
                restart = true;
                break;
            }
        }
    }

    public void EndGame()
    {
        gameOver = true;
        gameoverText.text = "Game Over!";
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
