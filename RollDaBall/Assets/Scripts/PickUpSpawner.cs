using Assets.Scripts;
using System;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{

    private float deltaTime;
    private Rigidbody rb;

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
        if (false)
        {
            var test = BitConverter.GetBytes(player.transform.position.x);
            var test2 = BitConverter.GetBytes(player.transform.position.z);

            var ba = new byte[sizeof(float) * 2];
            for (int i = 0; i < sizeof(float); i++)
            {
                ba[i] = test[i];
                ba[i + sizeof(float)] = test2[i];
            }

            //ba = udp.SendBytes(ba);

            transform.position = new Vector3(-BitConverter.ToSingle(ba, 0), .5f, -BitConverter.ToSingle(ba, 4));

            var newObject = Instantiate(objectToSpawn);
            newObject.transform.position = transform.position;
            newObject.SetActive(true);


            deltaTime = 0;
        }
    }

    void FixedUpdate()
    {

    }
}
