using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputSystem : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;

    public  Action<Vector3> OnMouseClick;
    Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            GroundCheck();
        }
    }

    private void GroundCheck(){
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayerMask )){
            OnMouseClick?.Invoke(hit.point);
        }
    }
}
