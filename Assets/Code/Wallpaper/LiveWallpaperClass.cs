using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LiveWallpaperClass : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 10;
    }
    void Update()
    {
        #if !UNITY_EDITOR
        bool IsActivityVisible = GetWallpaperState();
        // IsPreview: false
        // IsActivityVisible: true
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (IsActivityVisible)
        {
            if (currentSceneName == "Animated")
            {
                SceneManager.LoadScene("mainScene");
            }
        }
        else
        {
            if(currentSceneName != "Animated")
            {
                SceneManager.LoadScene("Animated");
            } 
        }
        #endif
    }
    public void SetWallpaper()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("SetWallpaper");
        }

    }

    public void BackToTheMenu()
    {
        SceneManager.LoadScene("mainScene");
    }
    public bool GetWallpaperState()
    {
        try
        {
            using (var unityPlayerSingletonClass = new AndroidJavaClass("kavukava.LiveWallpaper.UnityPlayerSingleton"))
            {
                return unityPlayerSingletonClass.GetStatic<bool>("WallpaperVisible");
            }
        }
        catch
        {
            return false;
        }

    }
    public bool GetIsPreview()
    {
        try
        {
            using (var unityPlayerSingletonClass = new AndroidJavaClass("kavukava.LiveWallpaper.UnityPlayerSingleton"))
            {
                return unityPlayerSingletonClass.GetStatic<bool>("IsPreview");
            }
        }
        catch
        {
            return false;
        }

    }
}
