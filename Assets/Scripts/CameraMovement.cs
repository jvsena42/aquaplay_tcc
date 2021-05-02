using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 touchStart;
    public float zommOutMin = 1;
    public float zommOutMax = 30;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Pan();
    }

    private void Pan(){
        if(Input.GetMouseButtonDown(0)){
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if(Input.touchCount ==2){
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPost = touchZero.position - touchZero.deltaPosition;    
            Vector2 touchOnePrevPost = touchOne.position - touchOne.deltaPosition;     

            float prevMagnitude = (touchZeroPrevPost - touchOnePrevPost).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float diference = currentMagnitude - prevMagnitude;
            
            float velocityZoom = diference * 0.01f;
            Zoom(velocityZoom);
            
        }else if(Input.GetMouseButton(0)){
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void Zoom(float increment){
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment,zommOutMin,zommOutMax);
    }
}
