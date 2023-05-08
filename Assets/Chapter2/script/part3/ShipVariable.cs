using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipVariable : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isTRY = false;
    public static bool abool = true;
    public static bool isShow = true;
    [Header("NPC")]
    [SerializeField]
    GameObject hook;
    [SerializeField]
    GameObject croco;
    [SerializeField]
    GameObject semi;
    [SerializeField]
    GameObject Door;
    [SerializeField]
    GameObject paper;

    public GameObject npc1;
    public GameObject npc2;

    void Awake()
    {
        npc1.SetActive(ShipVariable.abool);
        npc2.SetActive(!ShipVariable.abool);

        hook.SetActive(ShipVariable.isShow);
        croco.SetActive(ShipVariable.isShow);
        semi.SetActive(ShipVariable.isShow);
        Door.SetActive(ShipVariable.isShow);
        paper.SetActive(!ShipVariable.isShow);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
