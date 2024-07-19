using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{

    [SerializeField] private Image barImage;

    [SerializeField] private CuttingCounter cuttingCounter;


    private void Start()
    {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
        cuttingCounter.OnKitchenObjectRemoved += CuttingCounter_OnKitchenObjectRemoved;

        barImage.fillAmount = 0;
        Hide();
    }

    private void CuttingCounter_OnKitchenObjectRemoved(object sender, EventArgs e)
    {
        barImage.fillAmount = 0;
        Hide();
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        Show();
        // Stop any existing coroutine to avoid multiple coroutines running at the same time
        StopAllCoroutines();
        // Start the lerping coroutine
        StartCoroutine(LerpFillAmount(e.progressNormalized));
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
