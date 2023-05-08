using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using Ink.UnityIntegration;
using UnityEngine.SceneManagement;

public class ChatSystem3 : MonoBehaviour
{
    Scene scene;    //ch 구분

    [Header("Global Ink File")]
    [SerializeField] private InkFile globalsInkFile;
    private string currentSentence;
    private int npcOrder;
    [Header("Dialogue UI")]
    TextMeshProUGUI text;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject canvas;

    [SerializeField] private GameObject panel;  //
    
    GameObject quad;
    
    public GameObject parent;
    public textEffect talktext;
    Animator anim;
	int beforeNpc = 5;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set;}
    public static ChatSystem3 instance;
    static Choice choiceSelected;   //

    bool next = false;

    //받아온 거
    Transform[] chatPoint;
    public bool isStart = false;
    //말풍선 위치
    Vector3 pos;
    EnemyMove enemyMove;

    private void Awake(){
        scene = SceneManager.GetActiveScene();    //현재 무슨 씬인지

        enemyMove = parent.GetComponent<EnemyMove>();

        if(instance != null){
            Debug.LogWarning("Found more than one Dialogue");
        }
        instance = this;
        
        pos = new Vector3(0,1.5f,0);
        
    }

    public static ChatSystem3 GetInstance(){ //다른 데서 쓸 수 있음.
        return instance;
    }

    void Start()
    {
        dialogueIsPlaying = false;
        int index = 0;
        
     }

     private void Update() {
        
        if(enemyMove.die){
            Destroy(quad);
            //Invoke("DestroyThis",1);
        }

        else if(quad)
            quad.transform.position = Camera.main.WorldToScreenPoint(chatPoint[npcOrder].position + pos);
        if(!dialogueIsPlaying){
            return;
        }
        /*if(Input.GetKeyDown(KeyCode.Space)){
                DialogueFlow();
        }*/
        //StartCoroutine("NextFlow");
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
        
        //Invoke("DialogueFlow",2);
        return false;

    }

    public void Ondialogue(TextAsset inkJSON, Transform[] chatPos)
    {
        //if(quad){
            dialogueIsPlaying = true;
            currentStory = new Story(inkJSON.text);
            chatPoint = chatPos;
            
            quad = Instantiate(prefab,this.transform.position+pos, Quaternion.identity);
            quad.transform.SetParent(chatPoint[npcOrder].transform, false);
            text = quad.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            talktext = text.GetComponent<textEffect>();

            //
            //quad.transform.position = Camera.main.WorldToScreenPoint(chatPoint[npcOrder].position + pos);
            //quad.transform.parent = parent.transform;
            anim = quad.GetComponent<Animator>();
            quad.transform.SetSiblingIndex(0);
            DialogueFlow();
        //}
        //
    }
    
    private void HandleTags(List<string> currentTags){
        foreach (string tag in currentTags){
            string[] splitTag = tag.Split(':');
            if(splitTag.Length != 2){
                Debug.LogError("Tag could not ve approriately parsed:"+tag);
            }

            string tagValue = splitTag[1].Trim();
            npcOrder = int.Parse(tagValue)-1;
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
            //currentSentence = "선장님이 저자식한테 얼마나 잘해주셨는데!";
        }    
            HandleTags(currentStory.currentTags);

            //if(beforeNpc != npcOrder){
                Destroy(quad);
                quad = Instantiate(prefab,canvas.transform);
                text = quad.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
                
                talktext = text.GetComponent<textEffect>();
                quad.transform.position = Camera.main.WorldToScreenPoint(chatPoint[npcOrder].position + pos);
                anim = quad.GetComponent<Animator>();
            //}
            talktext.SetMsg(currentSentence);
            beforeNpc = npcOrder;
        //}
        //else{
        //    StartCoroutine("EndDialogue");
        //    return;
        //}
        StartCoroutine("NextFlow");
    }

    private IEnumerator EndDialogue(){
        yield return new WaitForSeconds(0.2f);
        
        dialogueIsPlaying = false;
        anim.SetBool("start",false);
        anim.SetBool("end",true);
        //Destroy(quad);
    }

    private IEnumerator NextFlow(){
        yield return new WaitForSeconds(2f);
        if(quad)
            DialogueFlow();
    }

    private void DestroyThis(){
        Destroy(quad);
    }
}
