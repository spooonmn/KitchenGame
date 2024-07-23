using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class CountDownTextUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI countDownText;


    private void Start()
    {
        KitchenGameManager.Instance.OnGameStateChanged += KitchenGameManager_OnGameStateChanged;
        gameObject.SetActive(false);
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
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (KitchenGameManager.Instance.IsCountDownToStartActive())
        {
            countDownText.text = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountDownToStartTimer()).ToString();
        }
    }

}
