using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSManager : MonoBehaviour
{
    //public int targetFps = 30;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("menuFPS"))
        {
            PlayerPrefs.SetInt("menuFPS",60);
        }

        if (!PlayerPrefs.HasKey("wallpaperFPS"))
        {
            PlayerPrefs.SetInt("wallpaperFPS", 10);
        }

        string currentScene = SceneManager.GetActiveScene().ToString();
        if(currentScene != "Animated")
        {

            Application.targetFrameRate = PlayerPrefs.GetInt("menuFPS");
        }
        if(currentScene == "Animated")
        {
            Application.targetFrameRate = PlayerPrefs.GetInt("wallpaperFPS");
        }
        
    }
}
