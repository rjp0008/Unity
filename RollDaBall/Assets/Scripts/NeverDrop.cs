using UnityEngine;
using System.Collections;

public class NeverDrop : MonoBehaviour
{

    private Transform tf;

    // Use this for initialization
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tf.position = new Vector3(tf.position.x, 0.5f, tf.position.z);
    }
}
