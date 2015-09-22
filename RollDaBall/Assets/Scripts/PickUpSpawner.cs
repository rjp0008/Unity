using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour {

	private float deltaTime;
	private Rigidbody rb;

	public GameObject player;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        deltaTime += Time.deltaTime;
        if (deltaTime > 10)
        {
            UdpClient client = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2000); // endpoint where server is listening (testing localy)
            client.Connect(ep);

            var test = BitConverter.GetBytes(player.transform.position.x);
            var test2 = BitConverter.GetBytes(player.transform.position.z);

            var ba = new byte[sizeof(float) * 2];
            for (int i = 0; i < sizeof(float); i++)
            {
                ba[i] = test[i];
                ba[i + sizeof(float)] = test2[i];
            }

            client.Send(ba, ba.Length);
            ba = client.Receive(ref ep);

            transform.position = new Vector3(BitConverter.ToSingle(ba, 0), 5, BitConverter.ToSingle(ba, 4));


            deltaTime = 0;
        }
    }

	void FixedUpdate()
	{
		
	}
}
