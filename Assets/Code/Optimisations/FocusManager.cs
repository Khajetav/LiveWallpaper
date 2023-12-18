using UnityEngine;

public class FocusManager : MonoBehaviour
{
    void OnApplicationFocus(bool hasFocus)
    {
        if (PlayerPrefs.GetString("optimisationToggle") == "ON")
        {
            if (!hasFocus)
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
                var allBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
                foreach (var behaviour in allBehaviours)
                {
                    behaviour.enabled = false;
                }
            }
            else
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
                var allBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
                foreach (var behaviour in allBehaviours)
                {
                    behaviour.enabled = true;
                }
            }
        }
    }
}
