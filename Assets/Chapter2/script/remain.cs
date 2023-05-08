using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remain : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isTRY = false;
    public static bool abool = true;
    [SerializeField] Transform player;
    [SerializeField] Transform playerPos;
    public GameObject cake;
    void Awake() {

        if(!abool){
        player.position = playerPos.position;
        cake.SetActive(true);
        }
        
        //DontDestroyOnLoad(gameObject);

        
    }
}
