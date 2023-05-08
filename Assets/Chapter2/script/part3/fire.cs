using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;

    void Start()
    {
        Invoke("DestroyFire", 3);
    }

    void Update()
    {

        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if (raycast.collider != null)
        {
            if (raycast.collider.tag == "Player")
            {
                Debug.Log("attacked");
            }
            DestroyFire();
        }
        transform.Translate(transform.right * -1f * speed * Time.deltaTime);
    }

    void DestroyFire()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
