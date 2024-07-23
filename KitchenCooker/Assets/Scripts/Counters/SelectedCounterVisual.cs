using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArrey;


    private void Start()
    {
        // adding delegate to the event
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;

    }
    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        
        if(e.selectedCounter == (object)baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArrey)
            {
            visualGameObject.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArrey)
        {
            visualGameObject.SetActive(false);
        }
    }
}
