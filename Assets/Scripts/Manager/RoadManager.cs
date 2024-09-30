using SVS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public PlacementManager PlacementManager;

    public List<Vector3Int> TempPlacementPositions = new List<Vector3Int>();
    public List<Vector3Int> RoadPositionsToRecheck = new List<Vector3Int>();


    public GameObject straight;

    public RoadFixer RoadFixer;

    private void Start(){
        RoadFixer = GetComponent<RoadFixer>();
    }

    public void PlaceRoad(Vector3Int position)
    {
        if (PlacementManager.CheckIfPositionIsBound(position) == false) return;
        if (PlacementManager.CheckIfPositionIsFree(position) == false) return;

        TempPlacementPositions.Clear();
        TempPlacementPositions.Add(position);
        PlacementManager.PlaceTempStructure(position, straight, CellType.Road);
        FixRoadPrefabs();

    }

    private void FixRoadPrefabs()
    {
        foreach (var tempPosition in TempPlacementPositions)
        {
            RoadFixer.FixRoadAtPosition(PlacementManager, tempPosition);
            //var neighbour = PlacementManager.GetNeighbourTypesFor(tempPosition, CellType.Road);
        }
    }

    public void RemoveRoad()
    {

    }
}
