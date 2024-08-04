using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button sFXButton;
    [SerializeField] private Button musicButton;

    [SerializeField] private TextMeshProUGUI sFXText;
    [SerializeField] private TextMeshProUGUI musicText;



    private void Awake()
    {
        sFXButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
    }

    private void Start()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        sFXText.text = ("Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f));
        musicText.text = ("Music: " + Mathf.Round(MusicManager.Instance.GetVolume()* 10f));


    }
}
