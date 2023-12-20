using UnityEngine;
using TMPro;

public class AchievementDescriptions : MonoBehaviour
{
    public TextMeshProUGUI textAchievementTitle;
    public TextMeshProUGUI textAchievementDescription;
    [SerializeField] private GameObject clickPanel;
    [SerializeField] private AchievementManager achievementManager;

    public void ChangeAchievement(string achievementName)
    {
        switch (achievementName)
        {
            case "BackToTheRoots":
                SetAchievement("Back To The Roots", "Select the night theme again!");
                break;
            case "Base":
                SetAchievement("All Your Baseballs Are Belong To Us", "Buy the baseball hat!");
                break;
            case "Bookshelf":
                SetAchievement("Background Keeper", "Buy all the backgrounds!");
                break;
            case "Click":
                SetAchievement("Click!", "You clicked this!");
                achievementManager.UnlockAchievement("Click!");
                clickPanel.SetActive(false);
                break;
            case "Day":
                SetAchievement("Morning!", "Rise and shine!");
                break;
            case "DayJungle":
                SetAchievement("Wood Needed", "Unlock the day jungle background!");
                break;
            case "GoGreen":
                SetAchievement("You Did It.", "You pressed it.");
                break;
            case "Hat":
                SetAchievement("Hat.", "Hat hat hat.");
                break;
            case "HatOnAHatOnAHat":
                SetAchievement("Hat On A Hat On A Hat", "Fat Cat Hat Rat Mat Pat");
                break;
            case "Heart":
                SetAchievement("Love Is In The Air!", "Collect 10 hearts!");
                break;
            case "Hearts":
                SetAchievement("Baby Don't Hurt Me", "Collect 50 hearts!");
                break;
            case "Heartss":
                SetAchievement("I'm Lovin' It", "Collect 100 hearts!");
                break;
            case "NightJungle":
                SetAchievement("It's Cold Out There", "Unlock the night jungle wallpaper.");
                break;
            case "ThisOrThat":
                SetAchievement("This Or That", "Change wallpapers 5 times.");
                break;
            case "Wave":
                SetAchievement("Hello World", "Welcome!");
                break;
            case "Tophat":
                SetAchievement("Bottom Top Hat", "Now that's fancy!");
                break;
            default:
                SetAchievement("Achievements", "Gotta catch them all!");
                break;
        }
    }

    private void SetAchievement(string title, string description)
    {
        textAchievementTitle.text = title;
        textAchievementDescription.text = description;
    }
}
