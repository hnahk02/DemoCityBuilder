using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int pathIndex;

    public EnemyPathway enemyPathway;

    void Start()
    {
        pathIndex = 0;
    }
    void Update()
    {
        if (pathIndex < enemyPathway.paths.Count())
        {
            if (Vector3.Distance(transform.position, enemyPathway.paths[pathIndex].position) < 0.1f)
            {
                pathIndex++;

            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, enemyPathway.paths[pathIndex].position, speed * Time.deltaTime);
            }
        }


    }
}
