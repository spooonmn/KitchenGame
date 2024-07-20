using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;

     private IHasProgress hasProgress;
    private enum Type
    {
        Stepped,
        Smooth,
    }
    [SerializeField] private Type type;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
        {
            Debug.LogError("GameObject" + hasProgressGameObject + "dosent impliment iHasProgress");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        hasProgress.OnKitchenObjectRemoved += HasProgress_OnKitchenObjectRemoved;

        barImage.fillAmount = 0;
        Hide();
    }

    private void HasProgress_OnKitchenObjectRemoved(object sender, EventArgs e)
    {
        barImage.fillAmount = 0;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        Show();
        
        switch (type)
        {
            case Type.Stepped:
                barImage.fillAmount = e.progressNormalized;
                break;
            case Type.Smooth:
                // Stop any existing coroutine to avoid multiple coroutines running at the same time
                StopAllCoroutines();
                // Start the lerping coroutine
                StartCoroutine(LerpFillAmount(e.progressNormalized));
                break;
        }
        

    }
    private IEnumerator LerpFillAmount(float targetFillAmount)
    {
        float startFillAmount = barImage.fillAmount;
        float duration = 0.2f; // Duration of the lerp in seconds
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;
            t = Mathf.SmoothStep(0, 1, t); // Apply ease-in-out
            barImage.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, t);
            yield return null; // Wait for the next frame
        }

        // Ensure the final fill amount is set
        barImage.fillAmount = targetFillAmount;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
