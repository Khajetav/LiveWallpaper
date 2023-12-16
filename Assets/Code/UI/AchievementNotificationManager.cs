using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class AchievementNotificationManager : MonoBehaviour
{
    public GameObject achievementNotificationPrefab;
    public float displayTime = 2f;

    public void ShowAchievementNotification(Achievement achievement)
    {
        Debug.Log("Achievement notification: " + achievement.name);
        Canvas canvas = GameObject.FindFirstObjectByType<Canvas>();
        GameObject notificationInstance = Instantiate(achievementNotificationPrefab, canvas.transform, false);
        TextMeshProUGUI nameText = notificationInstance.transform.Find("TextAchievementName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI descriptionText = notificationInstance.transform.Find("TextAchievementDescription").GetComponent<TextMeshProUGUI>();
        if (nameText != null) nameText.text = achievement.name;
        if (descriptionText != null) descriptionText.text = achievement.description;
        Image achievementImage = notificationInstance.transform.Find("ImageHolder/ImageAchievement").GetComponent<Image>();
        if (achievementImage != null) achievementImage.sprite = achievement.imageAchievement;
        Debug.Log("Starting coroutine: " + name);
        StartCoroutine(FadeNotification(notificationInstance));
    }



    private IEnumerator FadeNotification(GameObject notification)
    {
        CanvasGroup canvasGroup = notification.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = notification.AddComponent<CanvasGroup>();
        }

        yield return StartCoroutine(FadeIn(() =>
        {
            StartCoroutine(WaitAndFadeOut(canvasGroup));
        }, canvasGroup));
    }

    private IEnumerator WaitAndFadeOut(CanvasGroup canvasGroup)
    {
        yield return new WaitForSeconds(displayTime);
        yield return StartCoroutine(FadeOut(() =>
        {
            Destroy(canvasGroup.gameObject);
        }, canvasGroup));
    }

    private IEnumerator FadeIn(Action onCompleted, CanvasGroup canvasGroup)
    {
        float duration = 1f;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / duration);
            yield return null;
        }

        onCompleted?.Invoke();
    }

    private IEnumerator FadeOut(Action onCompleted, CanvasGroup canvasGroup)
    {
        float duration = 1f;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / duration);
            yield return null;
        }

        onCompleted?.Invoke();
    }
}
