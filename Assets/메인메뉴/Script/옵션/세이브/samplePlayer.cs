using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class samplePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    
    float moveZ = 0f;

    float moveX = 0f;

    if (Input.GetKey(KeyCode.W))

    {

        moveZ += 1f;

    }

 

    if (Input.GetKey(KeyCode.S))

    {

        moveZ -= 1f;

    }

 

    if (Input.GetKey(KeyCode.A))

    {

        moveX -= 1f;

    }

 

    if (Input.GetKey(KeyCode.D))

    {

        moveX += 1f;

    }

 

    transform.Translate(new Vector3(moveX, 0f, moveZ) * 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("dd");
        if(other.tag == "Player")
            SceneManager.LoadScene("play");
    }
}
