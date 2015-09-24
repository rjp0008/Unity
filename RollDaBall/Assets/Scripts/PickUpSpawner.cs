using Assets.Scripts;
using System;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{

    private float deltaTime;
    private Rigidbody rb;
    private float timeToSpawnNext = 0;

    public GameObject player;
    public GameObject objectToSpawn;

    UDPConnection udp = new UDPConnection();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += Time.deltaTime;

        if (deltaTime > timeToSpawnNext) 
        {
            timeToSpawnNext = UnityEngine.Random.Range(.5f, 3f);
            transform.position = new Vector3(-player.transform.position.x, 2f, -player.transform.position.z);

            var newObject = Instantiate(objectToSpawn);
            newObject.GetComponent<Transform>().position = new Vector3(transform.position.x,.5f, transform.position.z);
            newObject.SetActive(true);


            deltaTime = 0;
        }
    }

    void FixedUpdate()
    {

    }
}
