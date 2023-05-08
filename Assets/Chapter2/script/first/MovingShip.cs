using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingShip : MonoBehaviour
{
    public float moveSpeed;
    float xx;
    
    void Start(){
        //gameVariable.noMove = true;
    }

    private void FixedUpdate() {
        xx = Input.GetAxisRaw("Horizontal");
        this.transform.Translate(xx/10,0,0);
    }
    
}
