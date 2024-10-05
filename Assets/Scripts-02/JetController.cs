using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetController : MonoBehaviour
{
    public GameObject missilePrefab;   // Assign missile prefab in the Unity Inspector
    public Transform missileLaunchPoint; // Where the missile is launched from (e.g., under the wing)
    public float fireRate = 1f;        // Time between each missile shot
    private float nextFireTime = 0f;   // Time when the next missile can be fired

    void Update()
    {
        // Fire missile on pressing the space key, considering fire rate
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFireTime)
        {
            FireMissile();
            nextFireTime = Time.time + fireRate;  // Set time for next missile fire
        }
    }

    void FireMissile()
    {
        // Instantiate missile at the launch point with same rotation as jet
        GameObject missile = Instantiate(missilePrefab, missileLaunchPoint.position, missileLaunchPoint.rotation);
    }
}

