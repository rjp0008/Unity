using UnityEngine;
using System.Collections;

public class EnemyPilot : MonoBehaviour
{
    private GameObject enemyAI;
    private int id;
    private static int nextId = 0;
    // Use this for initialization
    void Start()
    {
        enemyAI = GameObject.FindGameObjectWithTag("GameController");
        id = ++nextId;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int FighterID
    {
        get { return id; }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            enemyAI.GetComponent<EnemyController>().enemyTransforms.Remove(other.gameObject.transform);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
