using UnityEngine;
using TMPro;
using System;

public class SettingsCode : MonoBehaviour
{
    public TextMeshProUGUI menuFPSText;
    public TextMeshProUGUI wallpaperFPSText;
    public TextMeshProUGUI optimisationText;
    private int[] fpsOptions = { 10, 30, 60 };
    private int currentMenuFPS;
    private int currentWallpaperFPS;
    private string currentOptimisation;
    public Confirmation confirmationScript;
    public FadeLogic fadeLogic;

    void Start()
    {
        currentMenuFPS = PlayerPrefs.GetInt("menuFPS", 60);
        currentWallpaperFPS = PlayerPrefs.GetInt("wallpaperFPS", 10);
        currentOptimisation = PlayerPrefs.GetString("optimisationToggle", "ON");
        Application.targetFrameRate = currentMenuFPS;
        UpdateUIText();
        confirmationScript.OnConfirmPress += HandleConfirm;
        confirmationScript.OnCancelPress += HandleCancel;
    }
    public void ResetButton()
    {
        confirmationScript.OpenThePanel();
    }
    private void HandleConfirm()
    {
        PlayerPrefs.DeleteAll();
        fadeLogic.LoadSceneAfterFade("settingsScene");
        UpdateUIText();
    }
    private void HandleCancel()
    {
        return;
    }
    public void ChangeMenuFPS(bool increase)
    {
        int index = Array.IndexOf(fpsOptions, currentMenuFPS);
        index += increase ? 1 : -1;
        if (index >= fpsOptions.Length) index = 0;
        if (index < 0) index = fpsOptions.Length - 1;
        currentMenuFPS = fpsOptions[index];
        PlayerPrefs.SetInt("menuFPS", currentMenuFPS);
        PlayerPrefs.Save();
        Application.targetFrameRate = PlayerPrefs.GetInt("menuFPS");
        UpdateUIText();
    }

    public void ChangeWallpaperFPS(bool increase)
    {
        int index = Array.IndexOf(fpsOptions, currentWallpaperFPS);
        index += increase ? 1 : -1;
        if (index >= fpsOptions.Length) index = 0;
        if (index < 0) index = fpsOptions.Length - 1;
        // Update the current FPS
        currentWallpaperFPS = fpsOptions[index];

        PlayerPrefs.SetInt("wallpaperFPS", currentWallpaperFPS);
        PlayerPrefs.Save();
        UpdateUIText();
    }
    public void ChangeOptimisation()
    {
        if (currentOptimisation == "ON")
        {
            currentOptimisation = "OFF";
            PlayerPrefs.SetString("optimisationToggle", "OFF");
        }
        else
        {
            currentOptimisation = "ON";
            PlayerPrefs.SetString("optimisationToggle", "ON");
        }
        UpdateUIText();
    }
    private void UpdateUIText()
    {
        menuFPSText.text = currentMenuFPS.ToString();
        wallpaperFPSText.text = currentWallpaperFPS.ToString();
        optimisationText.text = currentOptimisation.ToString();
    }



}
