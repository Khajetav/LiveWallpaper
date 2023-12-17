using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

// CONSTRUCTOR for achievement
// takes three parameters
// name, description and the image
[System.Serializable]
public class Achievement
{
    public string name;
    public Sprite imageAchievement;
    public string description;

    public Achievement(string name, string description, Sprite imageAchievement)
    {
        this.name = name;
        this.description = description;
        this.imageAchievement = imageAchievement;
    }
}

// ACHIEVEMENT MANAGER
// checks if an achievement exists
// unlocks the achievement
// forms a notification
// fades the notification in and out

public class AchievementManager : MonoBehaviour
{
    public List<Achievement> achievements = new List<Achievement>();
    public GameObject achievementNotificationPrefab;
    public float notificationDisplayTime = 2f;
    
    // call this to unlock an achievement
    // UnlockAchievement("Click!");
    public void UnlockAchievement(string achievementName)
    {
        // foreach to find if there's a name with such an achievement
        Achievement achievementToUnlock = achievements.Find(achievementTemp => achievementTemp.name == achievementName);
        if (achievementToUnlock != null && PlayerPrefs.GetInt(achievementToUnlock.name)==0)
        {
            PlayerPrefs.SetInt(achievementToUnlock.name, 1);
            PlayerPrefs.Save();
            ShowAchievementNotification(achievementToUnlock);
        }
    }
    public void ShowAchievementNotification(Achievement achievement)
    {
        // attach the notification to the UI canvas
        Canvas canvas = GameObject.FindFirstObjectByType<Canvas>();
        GameObject notificationInstance = Instantiate(achievementNotificationPrefab, canvas.transform, false);

        // change the notification text
        TextMeshProUGUI nameText = notificationInstance.transform.Find("TextAchievementName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI descriptionText = notificationInstance.transform.Find("TextAchievementDescription").GetComponent<TextMeshProUGUI>();
        nameText.text = achievement.name;
        descriptionText.text = achievement.description;

        // change the notification image
        Image achievementImage = notificationInstance.transform.Find("ImageHolder/ImageAchievement").GetComponent<Image>();
        achievementImage.sprite = achievement.imageAchievement;
        StartCoroutine(FadeNotification(notificationInstance));
    }

    #region Fade animations for the notification

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
        yield return new WaitForSeconds(notificationDisplayTime);
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
    #endregion
}

