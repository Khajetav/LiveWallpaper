using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private AchievementNotificationManager notificationManager;

    public List<Achievement> achievements = new List<Achievement>();
    public void UnlockAchievement(string achievementName)
    {
        Debug.Log("Achievement: " + achievementName);
        Achievement achievementToUnlock = achievements.Find(a => a.name == achievementName);
        if (achievementToUnlock != null && !achievementToUnlock.isUnlocked)
        {
            Debug.Log("Achievement if: " + achievementName);
            achievementToUnlock.Unlock(notificationManager);
        }
    }
}
