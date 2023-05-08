using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


[System.Serializable]
public class SaveData{
    public Vector3 playerPosition; 
    public string scNum;
}

public class saveNload : MonoBehaviour
{
    public SaveData MySaveData = new SaveData();
    public string sceneData;
    public string filePath; // 디렉토리
    private string filename = "/savefile.txt";
    private string scncename = "/scenefile.txt";
    private samplePlayer thePlayer;
    Scene scene;
    
    // Start is called before the first frame update
    void Start(){
        
        scene = SceneManager.GetActiveScene();

        filePath = Application.persistentDataPath + "/Mysavedata/";
        if(!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);
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

    public void saveDATA(){

        //thePlayer = FindObjectOfType<samplePlayer>();

        //MySaveData.playerPosition = thePlayer.transform.position;
        MySaveData.scNum = SceneManager.GetActiveScene().name;

        string json = JsonUtility.ToJson(MySaveData); //플레이어 위치를 제이쓴화

        File.WriteAllText(filePath + filename, json); //물리파일로 저장

        Debug.Log("저장 완료");
        Debug.Log(filePath);
    }

    public string LoadSceneName(){
        Debug.Log(MySaveData.scNum);
        if(File.Exists(filePath + filename)){
            Debug.Log("2");
            string loadJson = File.ReadAllText(filePath + filename);
            MySaveData = JsonUtility.FromJson<SaveData>(loadJson);
            return MySaveData.scNum;
        }
        else{
            Debug.Log("3");
            return "null";
        }
    }

    public void LoadData(){
        if(File.Exists(filePath + filename)){
            string loadJson = File.ReadAllText(filePath + filename);

            MySaveData = JsonUtility.FromJson<SaveData>(loadJson);

            //thePlayer = FindObjectOfType<samplePlayer>();

            //thePlayer.transform.position = MySaveData.playerPosition;
            
            //Debug.Log(LoadSceneName());
            Debug.Log("로드 완료");
        }
        else{
            Debug.Log("세이브 데이터가 없습니다.");
        }

    }

    
}
