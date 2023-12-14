using UnityEngine;

public class FocusManager : MonoBehaviour
{
    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
            var allBehaviours = FindObjectsOfType<MonoBehaviour>(true);
            foreach (var behaviour in allBehaviours)
            {
                behaviour.enabled = false;
            }
        }
        else
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            var allBehaviours = FindObjectsOfType<MonoBehaviour>(true);
            foreach (var behaviour in allBehaviours)
            {
                behaviour.enabled = true;
            }
        }
    }
}
