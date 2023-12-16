using UnityEngine;

[System.Serializable]
public class Achievement
{
    public string name;
    public Sprite imageAchievement;
    public string description;
    public bool isUnlocked;

    public Achievement(string name, string description, Sprite imageAchievement)
    {
        this.name = name;
        this.description = description;
        this.imageAchievement = imageAchievement;
        isUnlocked = PlayerPrefs.GetInt(name, 0) == 1;
    }

    public void Unlock(AchievementNotificationManager notificationManager)
    {
        Debug.Log("Achievement constructor: " + name);
        if (!isUnlocked)
        {
            Debug.Log("!isUnlocked: " + name);
            isUnlocked = true;
            PlayerPrefs.SetInt(name, 1);
            PlayerPrefs.Save();
            notificationManager.ShowAchievementNotification(this);
        }
    }
}
