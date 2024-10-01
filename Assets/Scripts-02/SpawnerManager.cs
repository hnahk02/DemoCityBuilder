using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerManager : MonoBehaviour
{

    public Transform prefab;
    private Queue<Unit> unitTrainingQueue = new Queue<Unit>();
    private Dictionary<Unit.CostType, float> trainingTimeDict = new Dictionary<Unit.CostType, float>();

    public HUDs huds;

    private float progress;
    bool isTraining;

    private void OnEnable()
    {
        huds.OnClickButton += UpdateQueue;
    }

    private void Start()
    {   
        trainingTimeDict.Add(Unit.CostType.One, 5f);
        trainingTimeDict.Add(Unit.CostType.Two, 10f);
        trainingTimeDict.Add(Unit.CostType.Three, 15f);

        
    }

    private void FixedUpdate()
    {
        if (unitTrainingQueue.Count > 0 && isTraining == false)
        {
            foreach (var kvp in trainingTimeDict)
            {
                if (kvp.Key == unitTrainingQueue.Peek().costType)
                {
                    isTraining = true;
                    progress = kvp.Value;
                    Debug.Log($"Training for unit {unitTrainingQueue.Peek().costType}");
                }
            }
        }

        if (isTraining)
        {
            progress -= Time.fixedDeltaTime;
            if (progress < 0) // complete training 
            {
                unitTrainingQueue.Dequeue();
                isTraining = false;
                Debug.Log("Finish training process");
            }
        }
    }

    private void UpdateQueue(int id)
    {
        if (id > 0)
        {
            GameObject gameObject = Instantiate(prefab.gameObject, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Unit unit = gameObject.AddComponent<Unit>();
            unit.SetCostType(id);
            unitTrainingQueue.Enqueue(unit);
        }
    }
}
