using UnityEngine;
using UnityEngine.SceneManagement;
public class LiveWallpaperClass : MonoBehaviour
{
    private AndroidJavaClass activity;
    private string wrapper = "lwp.Wrapper";

    void Start()
    {
        activity = new AndroidJavaClass("lwp.UnityPlayerActivity");
    }

    public void SetWallpaper()
    {
        SceneManager.LoadScene("Animated");
        activity.CallStatic("SetWallpaper");
    }

    public void BackToTheMenu()
    {
        SceneManager.LoadScene("Main");
    }
}
