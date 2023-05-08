using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject firstPersonCamera;
    public float cameraMoveSpeed;
    public Vector3 arrivePos;

    private void Start() {
        transform.position = firstPersonCamera.transform.position;
        arrivePos = transform.position + new Vector3(10,0,0);
    }
    
    private void Update() {
        
        transform.position = Vector3.Lerp(transform.position, 
                                          arrivePos, 
                                          Time.deltaTime * cameraMoveSpeed);
    }

    public void moveCamera2(){
        transform.position = Vector3.Lerp(firstPersonCamera.transform.position, 
                                          firstPersonCamera.transform.position + new Vector3(500,0,0), 
                                          Time.deltaTime * cameraMoveSpeed);
    }
}
