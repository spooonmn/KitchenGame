using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{


    public static KitchenGameManager Instance { get; private set; } 
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,

    }


    public event EventHandler OnGameStateChanged;

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

    private State state;
    private float waitingToStartTimer = 1f;
    private float countDownToStartTimer = 3f;
    private float GamePlayingTimer;
    private float GamePlayingTimerMax = 20f;

    private bool isGamePaused = false;

    private void Awake()
    {
        state = State.WaitingToStart;
        Instance = this;
    }
    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePause();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0)
                {
                    state = State.CountDownToStart;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer < 0)
                {
                    state = State.GamePlaying;
                    GamePlayingTimer = GamePlayingTimerMax;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                GamePlayingTimer -= Time.deltaTime;
                if (GamePlayingTimer < 0)
                {
                    state = State.GameOver;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
            
                }
                break;
            case State.GameOver:
                
                break;
        }
        //Debug.Log(state);
    }


    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }


    public bool IsCountDownToStartActive()
    {
        return state == State.CountDownToStart;
    }
    

    public float GetCountDownToStartTimer()
    {
        return countDownToStartTimer;
    }   
    public float GetGamePlayingTimer()
    {
        return GamePlayingTimer;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (GamePlayingTimer / GamePlayingTimerMax);
    }


    public void TogglePause()
    {
        
        isGamePaused = !isGamePaused;
        //Debug.Log("KitchenGameManager pause: " +isGamePaused);

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);

        }
    }

}
