using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class BackgroundShop : MonoBehaviour
{
    public List<BackgroundObject> backgrounds = new List<BackgroundObject>();
    public Image displayImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI heartsCurrencyOwnedText;
    public TextMeshProUGUI selectText;
    public TextMeshProUGUI ownedStatusText;
    public GameObject panelLocked;
    public UnityEngine.UI.Button selectButton;
    public GameObject imageLock;
    public Confirmation confirmationScript; 
    private int currentIndex = 0;
    public Image panelImage;
    void Start()
    {
        currentIndex = PlayerPrefs.GetInt("selectedBackgroundIndex");
        CurrencyHandler.SaveCurrency(100);
        //PlayerPrefs.DeleteAll();
        confirmationScript.OnConfirmPress += HandleConfirm;
        confirmationScript.OnCancelPress += HandleCancel;
        UpdateDisplay();
        heartsCurrencyOwnedText.text = CurrencyHandler.LoadCurrency().ToString();
    }

    public void UnlockBackground()
    {
        int currentCurrency = CurrencyHandler.LoadCurrency();
        
        if (PlayerPrefs.GetInt("background"+currentIndex)==0 && currentCurrency >= backgrounds[currentIndex].Cost)
        {
            confirmationScript.OpenThePanel();
        }
        else
        {
            StartCoroutine(ShakeCoroutine());
            Debug.Log("Not enough currency");
        }
    }

    private void HandleConfirm()
    {
        Debug.Log("The confirm button was pressed.");
        int currentCurrency = CurrencyHandler.LoadCurrency();
        currentCurrency -= backgrounds[currentIndex].Cost;
        CurrencyHandler.SaveCurrency(currentCurrency);
        // 1 = unlocked
        // 0 = locked
        PlayerPrefs.SetInt("background"+currentIndex,1);
        UpdateDisplay();
    }

    private void HandleCancel()
    {
        Debug.Log("The cancel button was pressed.");
        return;
    }
    public void NextBackground()
    {
        currentIndex = (currentIndex + 1) % backgrounds.Count;
        PlayerPrefs.SetInt("selectedBackgroundIndex", currentIndex);
        UpdateDisplay();
    }



    public void PreviousBackground()
    {
        currentIndex = (currentIndex - 1 + backgrounds.Count) % backgrounds.Count;
        PlayerPrefs.SetInt("selectedBackgroundIndex", currentIndex);
        UpdateDisplay();
    }



    public void SelectBackground()
    {
        selectText.text = "ACTIVE";
        selectButton.interactable = false;
        PlayerPrefs.SetString("background", backgrounds[currentIndex].Name);
        Debug.Log("Current bg name: " + backgrounds[currentIndex].Name);
        CallFadeIn();


    }
    public void CallFadeIn(Action onCompleted = null)
    {
        StartCoroutine(FadeIn(onCompleted));
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
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        onCompleted?.Invoke();
    }

    private void UpdateDisplay()
    {
        // first time code
        if (!PlayerPrefs.HasKey("background"))
        {
            PlayerPrefs.SetString("background", "Night");
            PlayerPrefs.SetInt("background0", 1);
        }
        Debug.Log(backgrounds[currentIndex].Name);
        Debug.Log(PlayerPrefs.GetString("background"));
        // unlocked bg
        if (PlayerPrefs.GetInt("background" + currentIndex)==1)
        {
            selectButton.interactable = true;
            panelLocked.SetActive(false);
            ownedStatusText.text = "Owned";
            // if it's both unlocked and equipped
            
            if (PlayerPrefs.GetString("background")==backgrounds[currentIndex].Name)
            {
                selectText.text = "ACTIVE";
                selectButton.interactable = false;
            }
            else
            {
                selectText.text = "SELECT";
            }
        }
        // locked bg
        else
        {
            costText.text = backgrounds[currentIndex].Cost.ToString();
            selectButton.interactable = false;
            selectText.text = "";
            panelLocked.SetActive(true);
            ownedStatusText.text = "Not owned";
        }
        displayImage.sprite = backgrounds[currentIndex].Image;
        nameText.text = backgrounds[currentIndex].Name;
        heartsCurrencyOwnedText.text = CurrencyHandler.LoadCurrency().ToString();

    }

    // animation so that the lock shakes 
    private IEnumerator ShakeCoroutine()
    {
        Vector3 originalPosition = imageLock.transform.localPosition;
        float elapsed = 0.0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            // animation dies down with time but doesn't crash because it divided by 0
            float x = UnityEngine.Random.Range(-1f, 1f) * 5f * (elapsed == 0 ? 0 : (duration / elapsed));

            //float y = Random.Range(-1f, 1f) * 0.2f;

            imageLock.transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        imageLock.transform.localPosition = originalPosition;
    }
    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        if (confirmationScript != null)
        {
            confirmationScript.OnConfirmPress -= HandleConfirm;
            confirmationScript.OnCancelPress -= HandleCancel;
        }
    }
}
