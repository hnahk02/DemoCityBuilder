using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameRuntime : MonoBehaviour
{
    public static GameRuntime Instance;

    private void Awake()
    {
        Instance = this;
    }

    public HQBuilding playerBuilding;
    public HQBuilding enemyBuilding;
}
