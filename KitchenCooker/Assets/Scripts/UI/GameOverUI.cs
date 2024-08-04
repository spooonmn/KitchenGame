using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RecipesDeliveredText;
    [SerializeField] private GameObject textObject;


    private void Start()
    {
        KitchenGameManager.Instance.OnGameStateChanged += KitchenGameManager_OnGameStateChanged;
        
        Hide();
        
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

    private void KitchenGameManager_OnGameStateChanged(object sender, EventArgs e)
    {
        
        if (KitchenGameManager.Instance.IsGameOver())
        {
            Show();
            RecipesDeliveredText.text = DeliveryManager.Instance.GetDeliveredRecipes().ToString();
            
        }
        else
        {
            Hide();
        }
    }
}
