using UnityEngine;

public class PauseManager : MonoBehaviour
{
    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
            // deprecated, but oh well
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
