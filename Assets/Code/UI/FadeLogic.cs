using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeLogic : MonoBehaviour
{
    [SerializeField] private GameObject panelFadePrefab;
    private Canvas canvas;

    // call LoadSceneAfterFade("sceneMain") or something

    void Start()
    {
        canvas = FindFirstObjectByType<Canvas>(); 
        InstantiateAndFadeOut();
    }

    public void LoadSceneAfterFade(string sceneName)
    {
        InstantiateAndFadeIn(() => SceneManager.LoadScene(sceneName));
    }

    // once the scene is loaded in
    // spawn the panel
    // and fade it out
    private void InstantiateAndFadeOut(Action onCompleted = null)
    {
        GameObject panelFade = Instantiate(panelFadePrefab, canvas.transform, false);
        Image panelImage = panelFade.GetComponent<Image>();
        StartCoroutine(FadeOut(panelImage, () =>
        {
            Destroy(panelFade);
            onCompleted?.Invoke();
        }));
    }

    // create a panel
    // and slowly fade it in
    private void InstantiateAndFadeIn(Action onCompleted)
    {
        GameObject panelFade = Instantiate(panelFadePrefab, canvas.transform, false);
        Image panelImage = panelFade.GetComponent<Image>();
        StartCoroutine(FadeIn(panelImage, onCompleted));
    }

    #region animations
    IEnumerator FadeIn(Image panelImage, Action onCompleted)
    {
        panelImage.gameObject.SetActive(true);
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

    IEnumerator FadeOut(Image panelImage, Action onCompleted)
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
    #endregion

}
