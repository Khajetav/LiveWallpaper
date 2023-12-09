using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LiveWallpaperClass : MonoBehaviour
{
    private bool WallpaperHasBeenSet;
    void Update()
    {
        WallpaperHasBeenSet = PlayerPrefs.GetInt("MyBool") == 1;
        #if DEBUG
        //  WallpaperHasBeenSet = false;

        #endif
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName != "Main")
        {
            if (WallpaperHasBeenSet && GetWallpaperState() || !WallpaperHasBeenSet && GetWallpaperState())
            {
                SceneManager.LoadScene("Main");
            }
        }
        if (currentSceneName != "Animated")
        {
            if (WallpaperHasBeenSet && !GetWallpaperState())
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
        PlayerPrefs.SetInt("MyBool", WallpaperHasBeenSet ? 1 : 0);

    }

    public void BackToTheMenu()
    {
        SceneManager.LoadScene("Main");
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
