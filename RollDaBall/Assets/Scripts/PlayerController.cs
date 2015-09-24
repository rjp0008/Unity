using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Assets.Scripts;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    private float deltaTime;
    public float speed;
    public Text countText;
    public Text winText;
    public GameObject anotherPlayer;

    private Rigidbody rb;
    private int score;
    private SphereCollider coll;



    private int id;
    private int packetNumber = 0;

    Dictionary<int, GameObject> otherPlayers;
    Dictionary<int, int> LatestUpdate;
    Dictionary<int, int> shouldDeleteOld = new Dictionary<int, int>();
    Dictionary<int, Interpolator> interpolators;

    UdpClient client = new UdpClient();
    IPEndPoint ep = new IPEndPoint(IPAddress.Parse("104.131.48.33"), 2000);


    void Start()
    {
        otherPlayers = new Dictionary<int, GameObject>();
        interpolators = new Dictionary<int, Interpolator>();
        LatestUpdate = new Dictionary<int, int>();

        rb = GetComponent<Rigidbody>();
        coll = GetComponent<SphereCollider>();
        score = 0;
        UpdateScore();
        winText.text = "";
        id = UnityEngine.Random.Range(int.MinValue, int.MaxValue);

        client.Connect(ep);
    }

    void CheckOutOfBounds()
    {
        if (rb.position.y < -10)
        {
            rb.mass = 1;
            rb.position = new Vector3(0, 100, 0);
            score = 0;
            rb.velocity = new Vector3(0, 0, 0);
            rb.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Update()
    {
        RemoveStalePlayers();
        CheckOutOfBounds();
    }

    void FixedUpdate()
    {
        deltaTime += Time.deltaTime;
        var data = DataToBytes();
        if (deltaTime > .1)
        {
            client.Send(data, data.Length);
            MakePlayersFromBytes(client.Receive(ref ep));
            deltaTime = 0;
        }

        UpdatePlayerLocations();

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
            // speed += score * .01f;
            //rb.transform.localScale = new Vector3((.1f * score + 1), .1f * score + 1f, .1f * score + 1);
            ////.radius = rb.transform.localScale.x / 2;
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

    //won't remove multiple
    private void RemoveStalePlayers()
    {
        int itemToRemove = 0;
        bool shouldRemove = false;
        try
        {
            foreach (var itemTracker in shouldDeleteOld.Keys)
            {
                shouldDeleteOld[itemTracker] -= 1;
                if (shouldDeleteOld[itemTracker] < 0)
                {
                    Destroy(otherPlayers[itemTracker]);
                    otherPlayers.Remove(itemTracker);
                    itemToRemove = itemTracker;
                    shouldRemove = true;
                    LatestUpdate.Remove(itemTracker);
                }
            }
            if (shouldRemove)
            {
                shouldDeleteOld.Remove(itemToRemove);
            }
        }
        catch { }
    }

    private void UpdatePlayerLocations()
    {
        foreach (var item in otherPlayers.Keys)
        {
            otherPlayers[item].GetComponent<Rigidbody>().position = interpolators[item].PositionAfterTime(deltaTime);
        }
    }


    private void MakePlayersFromBytes(byte[] data)
    {
        for (int x = 0; x < data.Length; x += 26)
        {
            var xPosition = BitConverter.ToSingle(data, x + 0);
            var yPosition = BitConverter.ToSingle(data, x + 4);

            var xSpeed = BitConverter.ToSingle(data, x + 8);
            var ySpeed = BitConverter.ToSingle(data, x + 12);

            PositionBehavior thisData = new PositionBehavior(xPosition, yPosition, xSpeed, ySpeed);

            var id = BitConverter.ToInt32(data, x + 16);
            var packetNumber = BitConverter.ToInt32(data, x + 20);

            if (id != this.id)
            {
                if (!otherPlayers.ContainsKey(id))
                {
                    var newObject = Instantiate(anotherPlayer);
                    newObject.SetActive(true);
                    var newInterpolator = new Interpolator(thisData);
                    otherPlayers[id] = newObject;
                    interpolators[id] = newInterpolator;
                    LatestUpdate[id] = packetNumber;
                    shouldDeleteOld[id] = 100;
                }
                else if (LatestUpdate[id] < packetNumber)
                {
                    LatestUpdate[id] = packetNumber;
                    shouldDeleteOld[id] = 100;
                    interpolators[id].CalculateNewInterpolation(thisData);
                    otherPlayers[id].GetComponent<Rigidbody>().position = interpolators[id].PositionAfterTime(0);
                }
            }
        }
    }

}


