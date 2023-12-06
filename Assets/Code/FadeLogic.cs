using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeLogic : MonoBehaviour
{
    public Image panelImage;
    [SerializeField]
    private Scene sceneName;

    void Start()
    {
        panelImage = GameObject.Find("fadePanel").GetComponent<Image>();
        StartCoroutine(FadeOut(() => Debug.Log("Fade Out Complete!")));
    }

    public void CallFadeIn(Action onCompleted = null)
    {
        StartCoroutine(FadeIn(onCompleted));
    }

    public void LoadSceneAfterFade(string sceneName)
    {
        StartCoroutine(FadeIn(() => SceneManager.LoadScene(sceneName)));
    }
    public void CallFadeOut(Action onCompleted = null)
    {
        StartCoroutine(FadeOut(() => SceneManager.LoadScene("mainScene")));
    }

    IEnumerator FadeIn(Action onCompleted)
    {
        float duration = 1f;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsed / duration);
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, alpha);
            yield return null;
        }

        onCompleted?.Invoke();
    }

    IEnumerator FadeOut(Action onCompleted)
    {
        float duration = 1f;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsed / duration);
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, alpha);
            yield return null;
        }

        onCompleted?.Invoke();
    }
}
