using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SavedData
{
    public string sceneName;
}

public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;
    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SaveManager>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject("SaveManager");
                    instance = singleton.AddComponent<SaveManager>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return instance;
        }
    }

    private string saveFilePath = "saveData.json";

    public void SaveSceneName()
    {
        SavedData SavedData = new SavedData();
        SavedData.sceneName = SceneManager.GetActiveScene().name;

        Debug.Log(SavedData.sceneName);

        string jsonData = JsonUtility.ToJson(SavedData);
        System.IO.File.WriteAllText(saveFilePath, jsonData);
    }

    public string LoadSceneName()
    {
        if (System.IO.File.Exists(saveFilePath))
        {
            string jsonData = System.IO.File.ReadAllText(saveFilePath);
            SavedData SavedData = JsonUtility.FromJson<SavedData>(jsonData);
            
            Debug.Log(SavedData.sceneName);

            return SavedData.sceneName;
        }
        else
        {
            return null;
        }
    }
}
