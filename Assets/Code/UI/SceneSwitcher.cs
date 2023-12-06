using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public Animator animator;
    private string sceneToLoad;

    // Call this to start the fade out
    public void FadeToScene(string sceneName)
    {
        sceneToLoad = sceneName;
        animator.SetTrigger("FadeOut");
    }

    // This is called via an Animation Event at the end of the fade-out animation
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
