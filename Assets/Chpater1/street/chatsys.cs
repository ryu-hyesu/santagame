using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using Ink.UnityIntegration;
using UnityEngine.SceneManagement;

public class chatsys : MonoBehaviour
{
    Scene scene;    //ch 구분
    //프리팹 크기 조절
    RectTransform ret;

    //public Queue<int> npcOrders;
    [Header("Global Ink File")]
    [SerializeField] private InkFile globalsInkFile;
    private string currentSentence;
    private int npcOrder;
    [Header("Dialogue UI")]
    TextMeshProUGUI text;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject canvas;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;

    private TextMeshProUGUI[] choicesText;
    [SerializeField] private GameObject panel;  //
    
    GameObject quad;
    
    public textEffect talktext;
    Animator anim;
   int beforeNpc = 5;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set;}
    public static chatsys instance;
    static Choice choiceSelected;   //

    //받아온 거
    Transform[] chatPoint;
    private DialogueVariables dialogueVariables;    //

    //거리 -> 집 장면 이동 관련
    string changeScene;
    public bool isSceneChanged;

    private Camera uiCamera;    //

    //말풍선 위치
    Vector3 pos;

    //ch3
    public bool activateChoices = false;
    public bool madeChoices = true;

    private void Awake(){
        scene = SceneManager.GetActiveScene();    //현재 무슨 씬인지

        if(instance != null){
            Debug.LogWarning("Found more than one Dialogue");
        }
        instance = this;
        
        if(scene.name != "ch3_dialogue" && scene.name != "ch3_game"){
            dialogueVariables = new DialogueVariables(globalsInkFile.filePath); //
        }
        pos = new Vector3(0,1.2f,0);
        
    }

    public static chatsys GetInstance(){ //다른 데서 쓸 수 있음.
        return instance;
    }

    void Start()
    {
        Canvas.ForceUpdateCanvases();

        dialogueIsPlaying = false;
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;

        foreach(GameObject choice in choices){
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

        if(scene.name =="ch3_dialogue"){
            for(int i=0;i<choices.Length;i++){
                choices[i].gameObject.SetActive(false);
            }   
        }
     }

     private void Update() {
        if(scene.name =="ch3_dialogue"){
            if(!dialogueIsPlaying){
                return;
            }
            //다이얼로그 플레이 중일 때 안 되도록
            if(Input.GetKeyDown(KeyCode.Space)){
                DialogueFlow();
            }
            
            if(!madeChoices&&activateChoices){
                DisplayChoices();
                madeChoices = true;
            }
        }
    }
    
    bool starting = true;   //

    public bool upup(TextAsset inkJSON, Transform[] chatPos) {
        if(starting){
            Debug.Log("start");
            dialogueIsPlaying = true;
            starting = false;
            Ondialogue(inkJSON,chatPos);
        }

        if(!dialogueIsPlaying){ // 끝
            starting = true;
            return true;
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
            DialogueFlow();

        return false;

    }

    public void Ondialogue(TextAsset inkJSON, Transform[] chatPos)
    {
        dialogueIsPlaying = true;
        currentStory = new Story(inkJSON.text);
        chatPoint = chatPos;

        if(scene.name != "ch3_dialogue" && scene.name != "ch3_game")
            dialogueVariables.StartListening(currentStory);
        
        quad = Instantiate(prefab,canvas.transform);
        text = quad.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        ret = quad.GetComponent<RectTransform>();
        talktext = text.GetComponent<textEffect>();

        
        quad.transform.position = Camera.main.WorldToScreenPoint(chatPoint[npcOrder].position + pos);
        anim = quad.GetComponent<Animator>();
        quad.transform.SetSiblingIndex(0);
        DialogueFlow();
    }
    
    private void HandleTags(List<string> currentTags){
        foreach (string tag in currentTags){
            string[] splitTag = tag.Split(':');
            if(splitTag.Length != 2){
                Debug.LogError("Tag could not ve approriately parsed:"+tag);
            }

            string tagValue = splitTag[1].Trim();
            npcOrder = int.Parse(tagValue);

            if(scene.name != "ch3_dialogue" && scene.name != "ch3_game"){
                //거리 -> 집 장면 이동 관련
                changeScene = splitTag[0].Trim();
            }
            
        }
    }

    public void DialogueFlow()
    {
        if(talktext.isAnimation){
            talktext.SetMsg("");
            return;
        }

        if(currentStory.canContinue){
            //다음 대사 가져옴.
            currentSentence = currentStory.Continue();
            //DisplayChoices();
            if(scene.name != "ch3_dialogue" && scene.name != "ch3_game" && scene.name != "street")
                ShowChoices();

            HandleTags(currentStory.currentTags);

            if(beforeNpc != npcOrder){
                Destroy(quad);
                quad = Instantiate(prefab,canvas.transform);
                text = quad.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
                
                if(scene.name != "ch3_dialogue" && scene.name != "ch3_game")
                    quad.transform.SetSiblingIndex(0);

                talktext = text.GetComponent<textEffect>();
                quad.transform.position = Camera.main.WorldToScreenPoint(chatPoint[npcOrder].position + pos);
                anim = quad.GetComponent<Animator>();
            }
            talktext.SetMsg(currentSentence);
            beforeNpc = npcOrder;
        }
        else{
            if(scene.name != "ch3_dialogue" && scene.name != "ch3_game"){
                //거리 -> 집 장면 이동
                if (changeScene == "darling")
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Debug.Log("장면이동");
                        SceneManager.LoadScene("1_livingroom");
                    }
            }
            StartCoroutine("EndDialogue");
            return;
        }
            
    }

    public void ShowChoices(){
        List<Choice> currentChoices = currentStory.currentChoices;

        if(currentChoices.Count > 0)
            panel.SetActive(true);
        else
            panel.SetActive(false);

        if(currentChoices.Count > choices.Length){
            Debug.Log("More choices were given then the UI can surpport. Number of choices given"
            + currentChoices.Count);
        }

        int index= 0;
        // enable and initiaize the choices up to the amount of choicees for this line ofr dialogue
        foreach(Choice choice in currentChoices){
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        // go through the remaing choices the UI suppourts ane make sure they're hidden
        for(int i=index; i<choices.Length; i++){
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if(currentChoices.Count > choices.Length){
            Debug.LogError("more choices were given than the UI can support. Number of choices given: "+currentChoices.Count);
        }
        int index = 0;
        foreach(Choice choice in currentChoices){
            if(quad) Destroy(quad);
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        StartCoroutine(SelectFirstChoice());
    }


    private IEnumerator SelectFirstChoice(){
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }


    public void MakeChoice(int choiceIndex){
        currentStory.ChooseChoiceIndex(choiceIndex);
        DialogueFlow();

        if(scene.name == "ch3_dialogue"){
            for(int i=0;i<choices.Length;i++){
                choices[i].gameObject.SetActive(false);
            }
        }
    }


    private IEnumerator EndDialogue(){
        yield return new WaitForSeconds(0.2f);
        
        if(scene.name != "ch3_dialogue" && scene.name != "ch3_game")
            dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        anim.SetBool("start",false);
        anim.SetBool("end",true);
        yield return null;
        
    }

}