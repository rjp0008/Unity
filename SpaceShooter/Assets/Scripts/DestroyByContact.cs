﻿using UnityEngine;
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
        if (gameObject != null)
        {
            gController = gameObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Could not find gamecontroller script.");
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        //Don't destroy boundary
        if (other.tag == "Boundary" || other.tag == "Enemy") return;

        //Destroy both these objects
        Destroy(other.gameObject);
        Destroy(gameObject);
        var rb = gameObject.GetComponent<Rigidbody>();

        //if object is player do something special
        if (other.tag == "Player")
        {
            gController.EndGame();
            var otherRb = other.gameObject.GetComponent<Rigidbody>();
            Instantiate(PlayerExplosion, otherRb.position, otherRb.rotation);
        }

        if (Explosion != null)
        {
            //Make the explosion
            Instantiate(Explosion, rb.position, rb.rotation);
        }

        gController.AddScore(scoreValue);
    }
}
