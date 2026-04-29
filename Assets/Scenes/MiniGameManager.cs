using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    private float timer = 0f;
    private bool isTimerRunning = false;
    private const float TimerDuration = 30;

    public Canvas miniGameCanvas; 
    public GameObject _miniGame; 

    void Awake()
    {
        
        DontDestroyOnLoad(gameObject);

        
        miniGameCanvas = GetComponentInChildren<Canvas>();
        if (miniGameCanvas != null)
        {
            DontDestroyOnLoad(miniGameCanvas.gameObject);
        }
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            if (timer >= TimerDuration)
            {
                OpenMiniGameWindow();
                isTimerRunning = false;
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MenuScene") 
        {
            StartTimer();
        }
    }

    void StartTimer()
    {
        timer = 0f;
        isTimerRunning = true;
    }

    void OpenMiniGameWindow()
    {
        if (miniGameCanvas != null)
        {
            miniGameCanvas.gameObject.SetActive(true);
        }

        _miniGame.transform.DOScale(Vector3.one, 0.3f);
        Debug.Log("Opening Mini Game Window");
    }
}
