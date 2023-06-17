using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 마우스 클릭, 터치 인터페이스 제공
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class gotoOption : MonoBehaviour
{
    public GameObject MenuGroup;
    public GameObject optionGroup;
    public GameObject SoundGroup;
    public BtnType currentState;
    public Image image;
    public static bool isShowing;
    public GameObject panel;

    bool isPaused;
    // Start is called before the first frame update
    [SerializeField] private GameObject go_baseUI;
    [SerializeField] private saveNload theSaveNLoad;

    

    void Start()
    {
        isShowing = false;
        isPaused = false;

    }

    public void GameSave()
    {
        
        PlayerPrefs.SetString("Playerch", SceneManager.GetActiveScene().name.ToString());
        Debug.Log(PlayerPrefs.GetString("Playerch"));
        PlayerPrefs.Save();
    }
    public void GameLoad()
    {
        if(PlayerPrefs.HasKey("Playerch")) return;

        string ch = PlayerPrefs.GetString("Playerch");
        
        if(SceneManager.GetActiveScene().Equals(ch))
            SceneManager.LoadScene(ch);
    }
    public void onClick(){
        
        switch(currentState){
            case BtnType.Continue:
                MenuGroup.SetActive(false);
                panel.SetActive(false);
                break;
            case BtnType.Save:
                SaveManager.Instance.SaveSceneName();
                break;

            case BtnType.Option:
                isShowing = true;
                MenuGroup.SetActive(false);
                optionGroup.SetActive(true);
                break;
            
             case BtnType.Sound:
                optionGroup.SetActive(false);
                SoundGroup.SetActive(true);
                break;

            case BtnType.Back:
                
                optionGroup.SetActive(false);
                MenuGroup.SetActive(true);
                isShowing = false;
                break;
            
            case BtnType.SoundBack:
                SoundGroup.SetActive(false);
                optionGroup.SetActive(true);
                break;
            case BtnType.Keep:
                MenuGroup.SetActive(false);
                panel.SetActive(false);
                onPauseclick();
                SceneManager.LoadScene("main_1");
                break;
            case BtnType.Quit:
                Application.Quit();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel")){
            
            if(!isShowing) // isShowing is false일때만 esc 적용 가능
            {
                onPauseclick();
                if(MenuGroup.activeSelf){
                    MenuGroup.SetActive(false);
                    panel.SetActive(false);
                }
                else{
                    MenuGroup.SetActive(true);
                    panel.SetActive(true);
                }
                    
            }
            
        }


    }

    void onPauseclick(){
        isPaused = !isPaused;
        
        Time.timeScale = (isPaused) ? 0.0f : 1.0f;

        var CanvasGroup = GameObject.Find("ui");
        if(CanvasGroup != null){
            for(int i = 0; i < CanvasGroup.transform.childCount; i++)
            {
                CanvasGroup.transform.GetChild(i).gameObject.SetActive(!isPaused);
            }
            
        }

        
    }

    IEnumerator MainSplash()
    {
        MenuGroup.SetActive(false);
        panel.SetActive(false);
        onPauseclick();

        yield return null;
        SceneManager.LoadScene("main_1");
                                              //코루틴 종료
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
}
