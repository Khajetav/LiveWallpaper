using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI achievementsText;
    [SerializeField] private TextMeshProUGUI hatsText;
    [SerializeField] private TextMeshProUGUI backgroundsText;
    [SerializeField] private TextMeshProUGUI donotpressText;
    [SerializeField] private TextMeshProUGUI heartsText;
    [SerializeField] private AchievementManager achievementManager;
    // Start is called before the first frame update
    void Start()
    {
        // handle achievements
        int totalAchievements=0;
        if (PlayerPrefs.GetInt("Back To The Roots") == 1)
            totalAchievements++;
        if (PlayerPrefs.GetInt("All Your Baseballs Are Belong To Us") == 1)
            totalAchievements++;
        if (PlayerPrefs.GetInt("Background Keeper") == 1)
            totalAchievements++;
        if (PlayerPrefs.GetInt("Click!") == 1)
            totalAchievements++;
        if (PlayerPrefs.GetInt("Morning!") == 1)
            totalAchievements++;
        if (PlayerPrefs.GetInt("Wood Needed") == 1)
            totalAchievements++;
        if (PlayerPrefs.GetInt("Go Green!") == 1)
            totalAchievements++;
        if (PlayerPrefs.GetInt("Hat.") == 1)
            totalAchievements++;
        if (PlayerPrefs.GetInt("Bottom Top Hat") == 1)
            totalAchievements++;
        if (PlayerPrefs.GetInt("Hat On A Hat On A Hat") == 1)
            totalAchievements++;
        if (PlayerPrefs.GetInt("Love Is In The Air!") == 1)
            totalAchievements++;
        if (PlayerPrefs.GetInt("Baby Don't Hurt Me") == 1)
            totalAchievements++;
        if (PlayerPrefs.GetInt("I'm Lovin' It") == 1)
            totalAchievements++;
        if (PlayerPrefs.GetInt("It's Cold Out There") == 1)
            totalAchievements++;
        if (PlayerPrefs.GetInt("This Or That") == 1)
            totalAchievements++;
        if (PlayerPrefs.GetInt("Hello World") == 1)
            totalAchievements++;
        achievementsText.text = totalAchievements.ToString() + "/16";

        // handle hats
        int totalHats = 0;
        if ((PlayerPrefs.GetInt("accessory0") == 1))
            totalHats++;
        if ((PlayerPrefs.GetInt("accessory1") == 1))
            totalHats++;
        if ((PlayerPrefs.GetInt("accessory2") == 1))
            totalHats++;
        if ((PlayerPrefs.GetInt("accessory3") == 1))
            totalHats++;
        hatsText.text = totalHats.ToString() + "/4";

        // handle backgrounds
        int totalBackgrounds = 0;
        if ((PlayerPrefs.GetInt("background0") == 1))
            totalBackgrounds++;
        if ((PlayerPrefs.GetInt("background1") == 1))
            totalBackgrounds++;
        if ((PlayerPrefs.GetInt("background2") == 1))
            totalBackgrounds++;
        if ((PlayerPrefs.GetInt("background3") == 1))
            totalBackgrounds++;
        backgroundsText.text = totalBackgrounds.ToString() + "/4";

        // handle do not press
        if (PlayerPrefs.HasKey("DoNotPressCount"))
        {
            donotpressText.text = PlayerPrefs.GetInt("DoNotPressCount").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("DoNotPressCount", 0);
        }

        // handle hearts
        if (PlayerPrefs.HasKey("heartsTotal"))
        {
            heartsText.text = PlayerPrefs.GetInt("heartsTotal").ToString();
        }


    }


    public void DoNotPress()
    {
        achievementManager.UnlockAchievement("You Did It.");
        PlayerPrefs.SetInt("DoNotPressCount", PlayerPrefs.GetInt("DoNotPressCount") + 1);
        donotpressText.text = PlayerPrefs.GetInt("DoNotPressCount").ToString();
    }
}
