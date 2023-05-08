using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTransform : MonoBehaviour
{
    public Transform playertransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playertransform.position + new Vector3(0,3,0);
    }
}
