using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameLogic : GameSystem
{
    [SerializeField] int gameRoundTime;
    GameTimer gameTimer;
    Spawner spawner;
    public event System.Action endGameEvent;

    //========== UI ==================
    [SerializeField] GameObject endGameWndGO;
    [SerializeField] GameObject pauseGameWndGO;
    Animator endGameWndAnimator;

    // Start is called before the first frame update
    void Start()
    {
        gameTimer = GameSystemsManager.Instance.GetSystem<GameTimer>();
        spawner = GameSystemsManager.Instance.GetSystem<Spawner>();
        gameTimer.timerEvent += TimerEventProcessing;
        gameTimer?.StartTimer();
       
        spawner?.StartSpawn();

        endGameWndAnimator = endGameWndGO.GetComponent<Animator>();
        endGameWndGO.SetActive(false);
        pauseGameWndGO.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TimerEventProcessing(int hours, int minutes, int seconds)
    {

        if (seconds == gameRoundTime)
        {
            //END of the game
            spawner.StopSpawn();
            gameTimer.StopTimer();
            endGameWndGO.SetActive(true);
            endGameWndAnimator.SetBool("ShowWND",true);
            endGameEvent();
        }
    }

    public void OnRestartGameButtonClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnExitGameButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnPauseGameButtonClick()
    {
        if (endGameWndGO.activeSelf) return;

        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseGameWndGO.SetActive(true);
        }
        
    }

    public void OnResumeGameButtonClick()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseGameWndGO.SetActive(false);
        }

    }

}
