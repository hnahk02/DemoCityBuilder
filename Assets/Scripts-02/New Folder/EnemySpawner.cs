using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public EnemyPathway path;
    public int[] wayConfig;
    
    void Update(){
        if(Input.GetKeyDown(KeyCode.T)){
            SpawnEnemy();
        }
    }

    void SpawnEnemy(){
        Enemy enemy = Instantiate(enemyPrefab, path.paths[0].transform.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.enemyPathway = path;
        enemy.gameObject.SetActive(true);
    }
}
