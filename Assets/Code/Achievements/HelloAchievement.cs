using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloAchievement : MonoBehaviour
{
    public AchievementManager achievementManager;
    void Start()
    {
        achievementManager.UnlockAchievement("Hello!");
    }
}
