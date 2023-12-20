using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSManager : MonoBehaviour
{
    //public int targetFps = 30;
    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().ToString();
        if (currentScene != "Animated")
        {
            Application.targetFrameRate = PlayerPrefs.GetInt("menuFPS", 60);
        }
        if (currentScene == "Animated")
        {
            Application.targetFrameRate = PlayerPrefs.GetInt("wallpaperFPS", 10);
        }
    }
}
