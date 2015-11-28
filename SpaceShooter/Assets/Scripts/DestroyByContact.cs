using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public GameObject Explosion;
    public GameObject PlayerExplosion;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary") return;
        Destroy(other.gameObject);
        Destroy(gameObject);
        var rb = gameObject.GetComponent<Rigidbody>();
        if (other.tag == "Player")
        {
            var otherRb = other.gameObject.GetComponent<Rigidbody>();
            Instantiate(PlayerExplosion, otherRb.position, otherRb.rotation);
        }
        Instantiate(Explosion, rb.position, rb.rotation);
    }
}
