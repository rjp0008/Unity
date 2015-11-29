using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DestroyByContact : MonoBehaviour
{
    public GameObject Explosion;
    public GameObject PlayerExplosion;
    public int scoreValue;
    private GameController gController;


    public void Start()
    {
        var gameObject = GameObject.FindGameObjectWithTag("GameController");
        gController = gameObject.GetComponent<GameController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        //Don't destroy boundary
        if (other.tag == "Boundary") return;

        //Destroy both these objects
        Destroy(other.gameObject);
        Destroy(gameObject);
        var rb = gameObject.GetComponent<Rigidbody>();

        //if object is player do something special
        if (other.tag == "Player")
        {
            var otherRb = other.gameObject.GetComponent<Rigidbody>();
            Instantiate(PlayerExplosion, otherRb.position, otherRb.rotation);
        }


        //Make the explosion
        Instantiate(Explosion, rb.position, rb.rotation);

        gController.AddScore(scoreValue);
    }
}
