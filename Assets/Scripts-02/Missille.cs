using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missille : MonoBehaviour
{
    public float missileSpeed = 500f;  // Speed of the missile
    public float lifetime = 10f;       // Time before the missile is destroyed
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * missileSpeed;  // Initial forward velocity
        Destroy(gameObject, lifetime);                   // Destroy missile after a certain time
    }

    void OnCollisionEnter(Collision collision)
    {
        // Handle missile collision with targets
        Debug.Log("Missile hit: " + collision.gameObject.name);

        // Add explosion effect or damage here (optional)
        
        Destroy(gameObject);  // Destroy the missile on impact
    }
}



