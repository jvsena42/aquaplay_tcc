using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMovement : MonoBehaviour
{
    Vector3 touchStart;
    public float zommOutMin = 1;
    public float zommOutMax = 30;

    //Tilemap
    [SerializeField] Tilemap theMap;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth,halfHeight,0f);
        topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth,-halfHeight,0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        Pan();
        LimitCamera();
    }

    private void LimitCamera(){
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,bottomLeftLimit.x,topRightLimit.x),
                                        Mathf.Clamp(transform.position.y,bottomLeftLimit.y,topRightLimit.y),
                                                    transform.position.z);
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
