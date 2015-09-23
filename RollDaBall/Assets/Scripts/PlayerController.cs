using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Assets.Scripts;
using System.Net.Sockets;
using System.Net;

public class PlayerController : MonoBehaviour
{
    private float deltaTime;
    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int score;


    private int id;
    private int packetNumber = 0;


    UdpClient client = new UdpClient();
    IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2000); // endpoint where server is listening (testing localy)


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        UpdateScore();
        winText.text = "";
        id = UnityEngine.Random.Range(int.MinValue, int.MaxValue);

        client.Connect(ep);
    }

    void FixedUpdate()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime > 1)
        {
            var data = DataToBytes();
            client.Send(data, data.Length);
            data = client.Receive(ref ep);
            deltaTime = 0;
        }
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");


        var movement = new Vector3(horizontal, 0, vertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            score++;
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        countText.text = "Count: " + score.ToString();
        if (score >= 12)
        {
            //winText.text = "YOU'VE WON!1";
        }
    }

    public byte[] DataToBytes()
    {
        var output = new byte[26];

        var xPosBytes = BitConverter.GetBytes(rb.transform.position.x);
        var yPosBytes = BitConverter.GetBytes(rb.transform.position.z);

        var xSpeedBytes = BitConverter.GetBytes(rb.velocity.x);
        var ySpeedBytes = BitConverter.GetBytes(rb.velocity.z);

        packetNumber++;
        var idBytes = BitConverter.GetBytes(id);
        var packetNumberBytes = BitConverter.GetBytes(packetNumber);

        for (int i = 0; i < 4; i++)
        {
            output[i] = xPosBytes[i];
            output[i + 4] = yPosBytes[i];
            output[i + 8] = xSpeedBytes[i];
            output[i + 12] = ySpeedBytes[i];
            output[i + 16] = idBytes[i];
            output[i + 20] = packetNumberBytes[i];
        }

        output[24] = 0x1;

        byte sum = 0;
        foreach (byte aByte in output)
        {
            sum += aByte;
        }

        output[25] = ((byte)~sum);

        return output;
    }
}


