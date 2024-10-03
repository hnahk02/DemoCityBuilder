using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TowerBuilderManager : MonoBehaviour
{

    public static TowerBuilderManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Header("UI")]
    public PlayerInputSystem input;
    public Button btnTower1;
    public Button btnTower2;
    public int maxTowerNum = 2;

    public Tower tower1Prefab, tower2Prefab;
    public Tower chosenTower;

    public int currentTowerNum;

    private void OnEnable()
    {
        input.OnMouseClick += HandleBuildTower;
    }

    private void Start()
    {
        btnTower1.onClick.AddListener(() =>
        {
            chosenTower = tower1Prefab;
        });

        btnTower2.onClick.AddListener(() =>
        {
            chosenTower = tower2Prefab;
        });
    }

    private void HandleBuildTower(Vector3 pos)
    {
        if(chosenTower == null) return;
        if (currentTowerNum < maxTowerNum)
        {
            Quaternion rot = Quaternion.Euler(new Vector3(0f, 90f, 0f));
            GameObject instance = Instantiate(chosenTower.gameObject, pos, rot);
            Tower tower = instance.GetComponent<Tower>();
            currentTowerNum++;
        }
        else
        {
            Debug.Log("Max tower num!!");
        }
    }



}
