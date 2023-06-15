using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class soundManager : MonoBehaviour
{
    GameObject BackgroundMusic;
    AudioSource audioSource;
    Scene scene;

    public static soundManager instance = null;

    void Awake()
    {
        scene = SceneManager.GetActiveScene();
        audioSource = GetComponent<AudioSource>();
        //Debug.Log(scene.name);

        if (scene.name == "5_game1" || scene.name == "12_game2" || scene.name == "New Scene")
        {
            //Debug.Log("음악x");
            //audioSource.Pause();
            if (instance == null)
                instance = this;
            else if (instance != this)
            {
                Destroy(soundManager.instance.gameObject);
                instance = this;
            }
        }
        else if (scene.name == "street")
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
            {
                Destroy(soundManager.instance.gameObject);
                instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // SoundManager 인스턴스가 이미 있는지 확인, 이 상태로 설정
            if (instance == null)
                instance = this;

            // 인스턴스가 이미 있는 경우 오브젝트 제거
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

    }

}
