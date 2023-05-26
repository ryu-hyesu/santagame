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
        Debug.Log(scene.name);

        if (scene.name == "5_game1" || scene.name == "12_game2" || scene.name == "New Scene")
        {
            //Debug.Log("����x");
            //audioSource.Pause();
            Destroy(soundManager.instance.gameObject);
        }

        else
        {
            // SoundManager �ν��Ͻ��� �̹� �ִ��� Ȯ��, �� ���·� ����
            if (instance == null)
                instance = this;

            // �ν��Ͻ��� �̹� �ִ� ��� ������Ʈ ����
            else if (instance != this || scene.name == "5_game1")
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

    }

}
