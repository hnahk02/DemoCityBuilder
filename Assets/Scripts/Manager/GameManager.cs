using System.Collections;
using System.Collections.Generic;
using SVS;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputManager InputManager;
    public CameraMovement CameraMovement;

    public RoadManager RoadManager;

    private void Start()
    {
        InputManager.OnMouseClick += HandleOnMouseClick;
    }

    private void Update()
    {
        CameraMovement.MoveCamera(new Vector3(InputManager.CameraMovementVector.x, 0, InputManager.CameraMovementVector.y));
    }

    private void HandleOnMouseClick(Vector3Int position)
    {
        Debug.Log(position);
        RoadManager.PlaceRoad(position);
    }


}
