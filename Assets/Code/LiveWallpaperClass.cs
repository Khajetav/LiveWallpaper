using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LiveWallpaperClass : MonoBehaviour
{
    private AndroidJavaClass activity;
    private Text textField;
    private bool WallpaperHasBeenSet;
    void Update()
    {
        WallpaperHasBeenSet = PlayerPrefs.GetInt("MyBool") == 1;
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Animated")
        {
            textField = GameObject.Find("ACTStateText").GetComponent<Text>();
            textField.text = GetACTState() + " " + WallpaperHasBeenSet;

        }
        if (currentSceneName != "Main")
        {
            if (WallpaperHasBeenSet && GetACTState() || !WallpaperHasBeenSet && GetACTState())
            {
                SceneManager.LoadScene("Main");
            }
        }
        if (currentSceneName != "Animated")
        {
            if (WallpaperHasBeenSet && !GetACTState())
            {
                SceneManager.LoadScene("Animated");
            }
        }
    }
    void Start()
    {

        activity = new AndroidJavaClass("lwp.UnityPlayerActivity");
    }

    public void SetWallpaper()
    {
        activity.CallStatic("SetWallpaper");
        WallpaperHasBeenSet = true;
        PlayerPrefs.SetInt("MyBool", WallpaperHasBeenSet ? 1 : 0);

    }

    public void BackToTheMenu()
    {
        SceneManager.LoadScene("Main");
    }
    public bool GetACTState()
    {
        bool actState = false;

        // Create an AndroidJavaClass object for your App class
        AndroidJavaClass appClass = new AndroidJavaClass("lwp.App");

        if (appClass != null)
        {
            // Use AndroidJavaClass to access the static variable ACT
            actState = appClass.GetStatic<bool>("ACT");
        }

        return actState;
    }
}
