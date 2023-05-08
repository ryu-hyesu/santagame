using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameSave : MonoBehaviour
{
    //public GameObject player;
    Scene scene;
    public static gameSave instance;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null){
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(this.gameObject);
        GameLoad();
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameSave1(){
            Debug.Log("dsfds");
    }

    public void GameSave()
    {
        Debug.Log("dsfds");
        //player.x, player.y;
        //PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        //PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetString("Playerch", scene.name.ToString());
        PlayerPrefs.Save();
    }
    public void GameLoad()
    {
        if(PlayerPrefs.HasKey("Playerch")) return;

        //float x = PlayerPrefs.GetFloat("PlayerX");
        //float y = PlayerPrefs.GetFloat("PlayerY");
        string ch = PlayerPrefs.GetString("Playerch");
        
        if(scene.name.Equals(ch))
            SceneManager.LoadScene(ch);
        //player.transform.position = new Vector3(x,y,0);
    }
    public void GameExit()
    {
        Application.Quit();
    }
}
