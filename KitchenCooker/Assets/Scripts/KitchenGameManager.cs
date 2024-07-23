using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{


    public static KitchenGameManager Instance { get; private set; } 
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }


    public event EventHandler OnGameStateChanged;

    private State state;
    private float waitingToStartTimer = 1f;
    private float countDownToStartTimer = 3f;
    private float GamePlayingTimer = 6f;

    private void Awake()
    {
        state = State.WaitingToStart;
        Instance = this;
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
        Debug.Log(state);
    }


    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }


    public bool IsCountDownToStartActive()
    {
        return state == State.CountDownToStart;
    }

    public float GetCountDownToStartTimer()
    {
        return countDownToStartTimer;
    }   

}
