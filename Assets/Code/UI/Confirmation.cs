using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Confirmation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private GameObject confirmationPanel;
    [SerializeField] private GameObject confirmationBackground;
    [SerializeField] private CanvasGroup canvasGroup;
    public event Action OnConfirmPress;
    public event Action OnCancelPress;
    private void Awake()
    {
        CurrencyHandler.SaveCurrency(100);
        confirmationPanel.SetActive(false);
    }

    public void CloseThePanel()
    {
        StartCoroutine(DoFadeOut());
        //confirmationPanel.SetActive(false);
    }
    public void OpenThePanel()
    {
        confirmationPanel.SetActive(true);
        confirmationBackground.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); 
        canvasGroup.alpha = 0;
        StartCoroutine(DoFadeIn());
        StartCoroutine(DoScaleUp());
    }
    public void ConfirmButtonClick()
    {
        OnConfirmPress?.Invoke();
        CloseThePanel();
    }

    public void CancelButtonClick()
    {
        OnCancelPress?.Invoke();
        CloseThePanel();
    }

    private IEnumerator DoFadeIn()
    {
        float duration = 0.2f;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentTime / duration);
            canvasGroup.alpha = alpha;
            currentTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    private IEnumerator DoScaleUp()
    {
        float duration = 0.2f;
        float currentTime = 0f;
        Vector3 startScale = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 endScale = Vector3.one;
        while (currentTime < duration)
        {
            Vector3 scale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            confirmationBackground.transform.localScale = scale;
            currentTime += Time.deltaTime;
            yield return null;
        }
        confirmationBackground.transform.localScale = endScale;
    }

    private IEnumerator DoFadeOut()
    {
        float duration = 0.1f;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            canvasGroup.alpha = alpha;
            currentTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        confirmationPanel.SetActive(false);
    }

}
