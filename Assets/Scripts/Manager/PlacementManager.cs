using System;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int Width, Height;
    Grid placementGrid;

    private Dictionary<Vector3Int, StructureModel> tempRoadObjects = new Dictionary<Vector3Int, StructureModel>();

    private void Start()
    {
        placementGrid = new Grid(Width, Height);
    }

    internal CellType[] GetNeighbourTypesFor(Vector3Int position)
    {
        return placementGrid.GetAllAdjacentCellTypes(position.x, position.z);
    }


    internal bool CheckIfPositionIsBound(Vector3Int position)
    {
        if (position.x >= 0 && position.x < Width && position.z >= 0 && position.z < Height)
        {
            return true;
        }

        return false;
    }

    internal bool CheckIfPositionIsFree(Vector3Int position)
    {
        return CheckIfPositionIsOfType(position, CellType.Empty);
    }

    private bool CheckIfPositionIsOfType(Vector3Int position, CellType cellType)
    {
        return placementGrid[position.x, position.z] == cellType;
    }

    internal void PlaceTempStructure(Vector3Int position, GameObject structurePrefab, CellType type)
    {
       placementGrid[position.x, position.z] = type;
       StructureModel structure = CreateNewStructureModel(position, structurePrefab, type);
       tempRoadObjects.Add(position, structure);
    }

    private StructureModel CreateNewStructureModel(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        GameObject structure = new GameObject(type.ToString());
        structure.transform.SetParent(transform);
        structure.transform.localPosition = position;
        var structureModel = structure.AddComponent<StructureModel>();
        structureModel.CreateModel(structurePrefab);

        return structureModel;
    }

   public void ModifyStructureModel(Vector3Int position, GameObject newModel, Quaternion rotation)
    {
        if (tempRoadObjects.ContainsKey(position))
            tempRoadObjects[position].SwapModel(newModel, rotation);
    }

    // internal object GetNeighbourTypesFor(Vector3Int tempPosition, CellType road)
    // {
        
    // }
}
