using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class CountDownTextUI : MonoBehaviour
{

    private TextMeshProUGUI countDownText;
    [SerializeField] private GameObject textObject;

    private void Start()
    {
        KitchenGameManager.Instance.OnGameStateChanged += KitchenGameManager_OnGameStateChanged;
        //gameObject.SetActive(false);
        textObject.SetActive(false);
        countDownText = textObject.GetComponent<TextMeshProUGUI>();
        
    }

    private void KitchenGameManager_OnGameStateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountDownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        //gameObject.SetActive(false);
        textObject.SetActive(false);
    }

    private void Show()
    {
        textObject.SetActive(true);
    }

    private void Update()
    {
        HandleCountDownTimer();
        HandleGameOverCountDown();
        //Debug.Log("Running");

    }

    private void HandleCountDownTimer()
    {
        if (KitchenGameManager.Instance.IsCountDownToStartActive())
        {
            countDownText.text = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountDownToStartTimer()).ToString();
        }
    }

    private void HandleGameOverCountDown()
    {
        if(KitchenGameManager.Instance.IsGamePlaying())
        {
            //Debug.Log(KitchenGameManager.Instance.GetGamePlayingTimer());
            if(KitchenGameManager.Instance.GetGamePlayingTimer() <= 5 )
            {
                Show();
                countDownText.text = Mathf.CeilToInt(KitchenGameManager.Instance.GetGamePlayingTimer()).ToString();

            }

        }
    }



}
