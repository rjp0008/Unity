using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int score;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        UpdateScore();
        winText.text = "";
    }

    void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");


        var movement = new Vector3(horizontal, 0,  vertical);
        
        rb.AddForce(movement*speed);
        rb.AddForce(rb.velocity * -0.1f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            score++;
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        countText.text = "Count: " + score.ToString();
        if(score >= 12)
        {
            winText.text = "YOU'VE WON!1";
        }
    }
}


