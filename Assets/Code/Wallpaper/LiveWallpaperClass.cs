using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LiveWallpaperClass : MonoBehaviour
{
    private bool WallpaperHasBeenSet;
    void Update()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        // if the scene is the animated wallpaper
        // and we aren't looking at the wallpaper
        if (currentSceneName == "Animated" && GetWallpaperState())
        {
            SceneManager.LoadScene("mainScene");
        }

        if (currentSceneName != "Animated")
        {
            if (!GetWallpaperState())
            {
                SceneManager.LoadScene("Animated");
            }
        }
    }
    void Start()
    {
    }

    public void SetWallpaper()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("SetWallpaper");
        }
        WallpaperHasBeenSet = true;
        PlayerPrefs.SetInt("WallpaperStatus", WallpaperHasBeenSet ? 1 : 0);

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
}
