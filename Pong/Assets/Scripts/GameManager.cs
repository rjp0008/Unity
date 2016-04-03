using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text playerOneScore;
    public Text playerTwoScore;

    public GameObject ball;

    private GameObject currentBall;

    // Use this for initialization
    void Start()
    {
        StartRound();
    }

    // Update is called once per frame
    void Update()
    {
    }



    public void StartRound()
    {
        if (currentBall != null)
            Destroy(currentBall);
        currentBall = Instantiate(ball);
    }

    public void PlayerScored(float ballXPosition)
    {
        UpdateScore(ballXPosition > 0 ? playerOneScore : playerTwoScore);
    }

    private void UpdateScore(Text scoreText)
    {
        var currentScoreText = scoreText.text.Split(' ');
        var currentScore = Convert.ToInt32(currentScoreText[1]);
        currentScore += 10;
        scoreText.text = currentScoreText[0] + " " + currentScore;
    }
}
