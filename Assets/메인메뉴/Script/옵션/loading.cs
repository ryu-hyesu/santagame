using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 비동기 로드 : Scene을 불러 올 때 하던 작업을 멈추지 않음.
// loadScene() 경우 완료하기 전까지 작업을 멈춤.
public class loading : MonoBehaviour
{
    public Slider progressBar;
    public Text loadText;

    public static string loadScene1;
    public static int loadType;

    public static loading instance;
    private saveNload theSaveNLoad;

    private void Start() {
        // if(instance == null){
        //         instance = this;
        //         DontDestroyOnLoad(gameObject);
        //     }
        //     else
        //         Destroy(this.gameObject);
        StartCoroutine(LoadScene());
    }

    public static void LoadSceneHandler(string _name, int _loadType){
        loadScene1 = _name;
        loadType = _loadType;
        SceneManager.LoadScene("loading");
    }

    IEnumerator LoadScene(){
        yield return null;
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(loadScene1);
        operation.allowSceneActivation = false;

        //Debug.Log(operation.progress);
        while(!operation.isDone){
            
            yield return null;

            if(progressBar.value < 0.9f){
                Debug.Log(progressBar.value + "0.9이전" + operation.progress);
                progressBar.value = Mathf.MoveTowards(progressBar.value,0.9f,Time.deltaTime);
            }
            else if(operation.progress >= 0.9f){
                Debug.Log("0.9 이상" + operation.progress + "-> "+ progressBar.value);
                progressBar.value = Mathf.MoveTowards(progressBar.value,1f,Time.deltaTime);
                
            }
            
            if(progressBar.value >= 1f)
                loadText.text = "Press SpaceBar";

            if(Input.GetKeyDown(KeyCode.Space) && (progressBar.value >= 1f) && (operation.progress >= 0.9f)){
                
                operation.allowSceneActivation = true;
            }

        }
        
        //theSaveNLoad = FindObjectOfType<saveNload>();
                //theSaveNLoad.LoadData();
                //gameObject.SetActive(false);
                //StopCoroutine(LoadScene());
                //Destroy(this);
                //DestroyObject(this);

                yield break;
    }
}
