using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CallJava : MonoBehaviour
{
    public void SetWallpaper()
    {
        //using (AndroidJavaObject activity = new AndroidJavaObject("com.company.product  .OverrideExample"))
        //{
        //    activity.Call("SetWallpaper");
        //}
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("SetWallpaper");
        }
        //using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        //{
        //    AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        //    AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", currentActivity, new AndroidJavaClass("com.company.product.UnityWallpaperService"));

        //    // Add extra data to the intent if needed
        //    // intent.PutExtra("key", "value");

        //    currentActivity.Call("SetWallpaper", intent);
        //}

    }
}
