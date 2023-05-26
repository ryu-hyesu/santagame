using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChatSystem2 : MonoBehaviour
{
    public Image image;

    public GameObject remainTimeTextUI;

    public string currentSentence;
    public int npcOrder;

    public TextMeshProUGUI text;

    public GameObject prefab;
    public GameObject canvas;
    GameObject quad;
    int beforeNpc = 5;
    public static ChatSystem2 instance;
    textEffect talktext;
    Animator anim;
    
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set;}

    bool timeOver = false;
    public NPCSentence npcSentence;

    //받아온 거
    public Transform[] chatPoint;

    public int gameSceneNum;
    bool ch1End;

    //말풍선 위치
    Vector3 pos;

    //효과음
    AudioSource audioSoure;
    Scene scene;
    Color color;

    public bool activateChoices = false;
    public bool madeChoices = true;
    bool isShow = false;

    private void Awake(){
        if(instance != null){
            Debug.LogWarning("Found more than one Dialogue");
        }
        instance = this;

        pos = new Vector3(0, 3.5f, 0);
        scene = SceneManager.GetActiveScene();
    }

    public static ChatSystem2 GetInstance(){ //다른 데서 쓸 수 있음.
        return instance;
    }

    void Start()
    {
        dialogueIsPlaying = false;
        //quizManager = GameObject.Find("QuizManager").GetComponent<QuizManager>();
        color = image.color;

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices){
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

        for(int i=0;i<choices.Length;i++){
            choices[i].gameObject.SetActive(false);
        }
        audioSoure = GetComponent<AudioSource>();
    }

    private void Update() {
        if(!dialogueIsPlaying){
            remainTimeTextUI.SetActive(false);
            return;
        }
        //다이얼로그 플레이 중일 때 안 되도록
        if(Input.GetKeyDown(KeyCode.Space)){
            DialogueFlow();
        }

        if (!madeChoices && activateChoices && remainTimeTextUI.GetComponent<RemainTime>().rTime != 0f)
        {
            DisplayChoices();
            madeChoices = true;
        }

        if (!timeOver && remainTimeTextUI.GetComponent<RemainTime>().rTime == 0f){
            TimeOver();
        }
    }

    bool starting = true;   //

    public bool upup(TextAsset inkJSON, Transform[] chatPos)
    {
        if (starting)
        {
            Debug.Log("start");
            dialogueIsPlaying = true;
            starting = false;
            Ondialogue(inkJSON, chatPos);
        }

        if (!dialogueIsPlaying)
        { // 끝
            starting = true;
            return true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            DialogueFlow();

        return false;

    }

    void TimeOver()
    {
        for(int i=0;i<choices.Length;i++){
            choices[i].gameObject.SetActive(false);
        }

        color.a = 0;
        image.color = color;
        
        timeOver = true;
        remainTimeTextUI.SetActive(false);
        //remainTimeTextUI.GetComponent<RemainTime>().rTime = 10f;
        text = quad.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        talktext = text.GetComponent<textEffect>();
        quad.transform.position = Camera.main.WorldToScreenPoint(chatPoint[0].position + pos);
        anim = quad.GetComponent<Animator>();
        talktext.SetMsg("시간이 너무 오래 걸려! 조금 더 서둘러.");

        if(Input.GetKeyDown(KeyCode.Space)){
            Destroy(quad);
            StartCoroutine("EndDialogue");
        }
    }
    //EnterDialogueMode
    public void Ondialogue(TextAsset inkJSON, Transform[] chatPos)
    {
        gameVariable.isTalk = true;
        dialogueIsPlaying = true;
        currentStory = new Story(inkJSON.text);
        chatPoint = chatPos;
        
        quad = Instantiate(prefab,canvas.transform);
        text = quad.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        talktext = text.GetComponent<textEffect>();
        quad.transform.position = Camera.main.WorldToScreenPoint(chatPoint[npcOrder].position + pos);
        anim = quad.GetComponent<Animator>();
        quad.transform.SetSiblingIndex(0);
        DialogueFlow();
    }

    public void DialogueFlow()
    {
        if (talktext.isAnimation){
            talktext.SetMsg("");
            return;
        }
        if(currentStory.canContinue){
            currentSentence = currentStory.Continue();

            if (scene.name != "9_livingroom")
                ShowChoices();

            HandleTags(currentStory.currentTags);

            if(beforeNpc != npcOrder){
                Destroy(quad);
                quad = Instantiate(prefab,canvas.transform);
                text = quad.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
                talktext = text.GetComponent<textEffect>();
                quad.transform.position = Camera.main.WorldToScreenPoint(chatPoint[npcOrder].position + pos);
                anim = quad.GetComponent<Animator>();
                
                if (scene.name == "13_kitchen")
                {
                    if (npcOrder == 2)
                    {
                        audioSoure.Play();
                    }
                }
            }

            talktext.SetMsg(currentSentence);
            beforeNpc = npcOrder;

            //DisplayChoices();
        }else{
            StartCoroutine("EndDialogue");
            return;
        }
    }

    void ShowChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        /*if (currentChoices.Count > 0)
            panel.SetActive(true);
        else
            panel.SetActive(false);
*/
        if (currentChoices.Count > choices.Length)
        {
            Debug.Log("More choices were given then the UI can surpport. Number of choices given"
            + currentChoices.Count);
        }

        int index = 0;
        // enable and initiaize the choices up to the amount of choicees for this line ofr dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        // go through the remaing choices the UI suppourts ane make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator EndDialogue(){
        yield return new WaitForSeconds(0.2f);
        //flag = 0;
        gameVariable.isTalk = false;
        dialogueIsPlaying = false;
        anim.SetBool("start",false);
        anim.SetBool("end",true);
        //Destroy(quad);

        //대화->게임 장면 전환
        if (gameSceneNum == 1 || gameSceneNum == 2)
        {
            StartCoroutine("FadeIn");
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if(currentChoices.Count > choices.Length){
            Debug.LogError("more choices were given than the UI can support. Number of choices given: "+currentChoices.Count);
        }
        int index = 0;
      
        foreach (Choice choice in currentChoices)
        {
            //if (quad) Destroy(quad);
            timeOver = false;
            choices[index].gameObject.SetActive(true);
            
            image.gameObject.SetActive(true);           
            color.a = 0.2f;
            image.color = color;

            isShow = true;
            remainTimeTextUI.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        /*foreach (Choice choice in currentChoices){
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }*/
        /*for(int i=index;i<choices.Length;i++){
            choices[i].gameObject.SetActive(false);
        }*/

        //remainTimeTextUI.SetActive(false);
        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        remainTimeTextUI.GetComponent<RemainTime>().rTime = 5f;
        remainTimeTextUI.SetActive(false);
        
        StartCoroutine(SelectFirstChoice());
        
        for (int i=0;i<choices.Length;i++){
            choices[i].gameObject.SetActive(false);
            
            color.a = 0;
            image.color = color;
        }
    }

    private void HandleTags(List<string> currentTags){
        foreach (string tag in currentTags){
            string[] splitTag = tag.Split(':');
            //if(splitTag.Length != 2){
            //    Debug.LogError("Tag could not ve approriately parsed:"+tag);
            //}
            string tagValue = splitTag[1].Trim();
            npcOrder = int.Parse(tagValue);

            tagValue = splitTag[3].Trim();
            /*if(int.Parse(tagValue)==1)
                remainTimeTextUI.SetActive(true);
            else if(int.Parse(tagValue)==0 || int.Parse(tagValue)==-1){
                remainTimeTextUI.SetActive(false);
            }*/
            if(int.Parse(tagValue)==-1)
                npcSentence.did = true;

            //대화->게임 장면 전환
            tagValue = splitTag[5].Trim();
            gameSceneNum = int.Parse(tagValue);
        }
    }

    IEnumerator FadeIn()
    {
        float f;
        for (f = 0f; f <= 1; f += 0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            image.color = new Color(0, 0, 0, f);
        }
        if (gameSceneNum == 1)
            SceneManager.LoadScene("12_game2");    //11_kitchen 리자 대화 후 -> soup game
        if (gameSceneNum == 2)
            SceneManager.LoadScene("New Scene");     //Chapter1(House2_2) -> Chapter2 시작 씬
    }

    IEnumerator FadeOut()
    {
        float f;
        for (f = 1f; f >= 0f; f -= 0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            image.color = new Color(0, 0, 0, f);
        }
    }
}