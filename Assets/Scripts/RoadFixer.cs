
using System;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;
using System.Collections.Generic;

public class RoadFixer : MonoBehaviour
{
    public GameObject DeadEnd, Straight, Corner, ThreeWay, FourWay;

    public void FixRoadAtPosition(PlacementManager placementManager, Vector3Int tempPosition)
    {
        var res = placementManager.GetNeighbourTypesFor(tempPosition);
        int roadCount = 0;
        //[right,up,left,down]
        roadCount = res.Where(x => x == CellType.Road).Count();
        Debug.Log("Road count: " + roadCount);
        if (roadCount == 0 || roadCount == 1)
        {
            CreateDeadEnd(placementManager, res, tempPosition);
        }
        else if (roadCount == 2)
        {
            if (CreateStraightRoad(placementManager, res, tempPosition))
                return;
            CreateCorner(placementManager, res, tempPosition);
        }
        else if (roadCount == 3)
        {
            Create3Way(placementManager, res, tempPosition);
        }
        else
        {
            Create4Way(placementManager, res, tempPosition);
        }

    }
  private void Create4Way(PlacementManager placementManager, CellType[] result, Vector3Int temporaryPosition)
    {
        placementManager.ModifyStructureModel(temporaryPosition, FourWay, Quaternion.identity);
    }

    //[left, up, right, down]
    private void Create3Way(PlacementManager placementManager, CellType[] result, Vector3Int temporaryPosition)
    {
        if(result[1] == CellType.Road && result[2] == CellType.Road && result[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, ThreeWay, Quaternion.identity);
        }else if (result[2] == CellType.Road && result[3] == CellType.Road && result[0] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, ThreeWay, Quaternion.Euler(0,90,0));
        }
        else if (result[3] == CellType.Road && result[0] == CellType.Road && result[1] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, ThreeWay, Quaternion.Euler(0, 180, 0));
        }
        else if (result[0] == CellType.Road && result[1] == CellType.Road && result[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, ThreeWay, Quaternion.Euler(0, 270, 0));
        }

    }

    //[left, up, right, down]
    private void CreateCorner(PlacementManager placementManager, CellType[] result, Vector3Int temporaryPosition)
    {
        if (result[1] == CellType.Road && result[2] == CellType.Road )
        {
            placementManager.ModifyStructureModel(temporaryPosition, Corner, Quaternion.Euler(0, 90, 0));
        }
        else if (result[2] == CellType.Road && result[3] == CellType.Road )
        {
            placementManager.ModifyStructureModel(temporaryPosition, Corner, Quaternion.Euler(0, 180, 0));
        }
        else if (result[3] == CellType.Road && result[0] == CellType.Road )
        {
            placementManager.ModifyStructureModel(temporaryPosition, Corner, Quaternion.Euler(0, 270, 0));
        }
        else if (result[0] == CellType.Road && result[1] == CellType.Road )
        {
            placementManager.ModifyStructureModel(temporaryPosition, Corner, Quaternion.identity);
        }
    }

    //[left, up, right, down]
    private bool CreateStraightRoad(PlacementManager placementManager, CellType[] result, Vector3Int temporaryPosition)
    {
        if (result[0] == CellType.Road && result[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, Straight, Quaternion.identity);
            return true;
        }else if (result[1] == CellType.Road && result[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, Straight, Quaternion.Euler(0,90,0));
            return true;
        }
        return false;
    }

    //[left, up, right, down]
    private void CreateDeadEnd(PlacementManager placementManager, CellType[] result, Vector3Int temporaryPosition)
    {
        if (result[1] == CellType.Road )
        {
            placementManager.ModifyStructureModel(temporaryPosition, DeadEnd, Quaternion.Euler(0, 270, 0));
        }
        else if (result[2] == CellType.Road )
        {
            placementManager.ModifyStructureModel(temporaryPosition, DeadEnd, Quaternion.identity);
        }
        else if (result[3] == CellType.Road )
        {
            placementManager.ModifyStructureModel(temporaryPosition, DeadEnd, Quaternion.Euler(0, 90, 0));
        }
        else if (result[0] == CellType.Road )
        {
            placementManager.ModifyStructureModel(temporaryPosition, DeadEnd, Quaternion.Euler(0, 180, 0));
        }
    }
}
