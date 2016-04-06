using UnityEngine;
using System.Collections;

public class EnemyPilot : MonoBehaviour
{
    private GameObject enemyAI;
    private string id;
    private static int nextId = 0;
    // Use this for initialization
    void Start()
    {
        enemyAI = GameObject.FindGameObjectWithTag("GameController");
        id = nextId++.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string FighterID
    {
        get { return id; }
        set { id = value; }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            enemyAI.GetComponent<EnemyController>().EnemyTransforms.Remove(other.gameObject.transform);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
