using UnityEngine;

public class LaserBolt : MonoBehaviour
{

    public float boltSpeed;
    public bool playerShot;
    private Rigidbody rb;
    private GameObject enemyAI;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyAI = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (Mathf.Abs(rb.position.y) > 15)
        {
            Destroy(gameObject);
        }
        else
        {
            int direction = playerShot ? 1 : -1;
            rb.transform.position += new Vector3(0, direction * boltSpeed * Time.deltaTime, 0);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!playerShot && other.tag != "Baddie")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (playerShot && other.tag == "Baddie")
        {
            enemyAI.GetComponent<EnemyController>().EnemyTransforms.Remove(other.gameObject.transform);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
