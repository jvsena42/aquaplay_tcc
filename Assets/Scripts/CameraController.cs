﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [SerializeField] private float zoomStep , minCamSize, maxCamSize;

    private Vector3 dragOrigin;

    [SerializeField] TilemapRenderer mapRenderer;

    private float mapMinX, mapMaxX, mapMinY, mapMaxY;
    
    private void Awake() {
        
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x /2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x /2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y /2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y /2f;

    }

    void Update()
    {
        PanCamera();
    }

    private void PanCamera(){

        //Save position of mouse in space
        if(Input.GetMouseButtonDown(0)){
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        //calculate the diference between positions if it is still held down
        if(Input.GetMouseButtonDown(0)){
            Vector3 diference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            //move the camera that distance
            cam.transform.position = ClampCamera(cam.transform.position + diference);
            
        }
    
    }

    public void ZoomIn(){
        float newSize = cam.orthographicSize - zoomStep;

        cam.orthographicSize = Mathf.Clamp(newSize,minCamSize,maxCamSize);

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    public void ZoomOut(){
        float newSize = cam.orthographicSize + zoomStep;

        cam.orthographicSize = Mathf.Clamp(newSize,minCamSize,maxCamSize);

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    private Vector3 ClampCamera (Vector3 targetPosition){
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x,minX,maxX);
        float newY = Mathf.Clamp(targetPosition.y,minY,maxY);

        return new Vector3(newX,newY,targetPosition.z);
    }
}