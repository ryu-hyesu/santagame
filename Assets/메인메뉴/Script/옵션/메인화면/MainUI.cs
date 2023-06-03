using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 마우스 클릭, 터치 인터페이스 제공
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
public class MainUI : MonoBehaviour
{
    public BtnType currentState; // 현재 버튼 상태 불러오기
    Vector3 defaultScale;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    public CanvasGroup SoundGroup;

    bool isSound;

    public static MainUI instance;
    private static bool isStart = false;
    private saveNload theSaveNLoad;
    private gameSave gamesave;
    private string name;
    public Image image;

    // fade in 변수
    public float animTime = 2f;
    private float start = 1f;           // Mathf.Lerp 메소드의 첫번째 값.  
    private float end = 0f;             // Mathf.Lerp 메소드의 두번째 값.  
    private float time = 0f;            // Mathf.Lerp 메소드의 시간 값.  
    private bool isPlaying = false;
    string ch;
    
    private void Start() {
        theSaveNLoad = FindObjectOfType<saveNload>();
        GameSave();
    }

    public void GameSave()
    {
        PlayerPrefs.SetString("Playerch", SceneManager.GetActiveScene().name.ToString());
        PlayerPrefs.Save();
    }

    public void onClick(){
        switch(currentState){
            case BtnType.New:
                Time.timeScale = 1.0f;
                CanvasGroupOff(mainGroup);
                ch = "Book_start";
                StartCoroutine("PlayFadeOut");
                break;
            
            case BtnType.Continue:
                Time.timeScale = 1.0f;
                if(!PlayerPrefs.HasKey("Playerch")) return;
                ch = PlayerPrefs.GetString("Playerch");

                StartCoroutine("PlayFadeOut");

                break;

            case BtnType.Keep:
                break;

            case BtnType.Save:
                break;

            case BtnType.Option:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                break;

            case BtnType.Sound:
                CanvasGroupOn(SoundGroup);
                CanvasGroupOff(optionGroup);
                break;

            case BtnType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                break;
            
            case BtnType.SoundBack:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(SoundGroup);
                break;

            case BtnType.Quit:
                Application.Quit();
                break;

        }

    }

    public void CanvasGroupOn(CanvasGroup cg){
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void CanvasGroupOff(CanvasGroup cg){
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    IEnumerator PlayFadeIn()  
        {  
            // 애니메이션 재생중.  
            isPlaying = true;  
   
            // Image 컴포넌트의 색상 값 읽어오기.  
            Color color = image.color;  
            time = 0f;  
            color.a = Mathf.Lerp(start, end, time);  
   
            while (color.a > 0f)  
            {  
                // 경과 시간 계산.  
                // 2초(animTime)동안 재생될 수 있도록 animTime으로 나누기.  
                time += Time.deltaTime / animTime;  
   
                // 알파 값 계산.  
                color.a = Mathf.Lerp(start, end, time);  
                // 계산한 알파 값 다시 설정.  
                image.color = color;  
   
                yield return null;  
            }  
   
            // 애니메이션 재생 완료.  
            isPlaying = false;  

            yield return null; 
            
            loading.LoadSceneHandler("prolog",0);
        }  

    IEnumerator PlayFadeOut()  
        {  
            // 애니메이션 재생중.  
            isPlaying = true;  
   
            // Image 컴포넌트의 색상 값 읽어오기.  
            Color color = image.color;  
            time = 0f;  
            color.a = Mathf.Lerp(end, start, time);  
   
            while (color.a < 1f)  
            {  
                // 경과 시간 계산.  
                // 2초(animTime)동안 재생될 수 있도록 animTime으로 나누기.  
                time += Time.deltaTime / animTime;  
   
                // 알파 값 계산.  
                color.a = Mathf.Lerp(end, start, time);  
                // 계산한 알파 값 다시 설정.  
                image.color = color;  
   
                yield return null;  
            }  
   
            // 애니메이션 재생 완료.  
            isPlaying = false; 

            yield return null;
            SceneManager.LoadScene(ch);
        }  

}
