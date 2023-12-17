using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class AccessoryShop : MonoBehaviour
{
    public List<AccessoryObject> accessories = new List<AccessoryObject>();
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
    public Image panelImage;
    public AchievementManager achievementManager;
    private int currentIndex = 0;
    void Start()
    {
        if (!PlayerPrefs.HasKey("accessory"))
        {
            PlayerPrefs.SetString("accessory", "None");
            PlayerPrefs.SetInt("accessory0", 1);
        }
        currentIndex = PlayerPrefs.GetInt("selectedAccessoryIndex");
        //CurrencyHandler.SaveCurrency(100);
        //PlayerPrefs.DeleteAll();
        confirmationScript.OnConfirmPress += HandleConfirm;
        confirmationScript.OnCancelPress += HandleCancel;
        UpdateDisplay();
        heartsCurrencyOwnedText.text = CurrencyHandler.LoadCurrency().ToString();
    }

    public void UnlockAccessory()
    {
        int currentCurrency = CurrencyHandler.LoadCurrency();

        if (PlayerPrefs.GetInt("accessory" + currentIndex) == 0 && currentCurrency >= accessories[currentIndex].Cost)
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
        currentCurrency -= accessories[currentIndex].Cost;
        CurrencyHandler.SaveCurrency(currentCurrency);
        // 1 = unlocked
        // 0 = locked
        PlayerPrefs.SetInt("accessory" + currentIndex, 1);
        PlayerPrefs.Save();
        if (accessories[currentIndex].Name == "Base")
        {
            achievementManager.UnlockAchievement("All Your Baseballs Are Belong To Us");
        }
        else if (accessories[currentIndex].Name == "Fancy hat")
        {
            achievementManager.UnlockAchievement("Hat.");
        }
        else if (accessories[currentIndex].Name == "Tophat")
        {
            achievementManager.UnlockAchievement("Bottom Top Hat");
        }
        if (PlayerPrefs.GetInt("All Your Baseballs Are Belong To Us") == 1 && PlayerPrefs.GetInt("Hat.") == 1 && PlayerPrefs.GetInt("Bottom Top Hat") == 1)
        {
            achievementManager.UnlockAchievement("Hat On A Hat On A Hat");
        }
        UpdateDisplay();
    }

    private void HandleCancel()
    {
        Debug.Log("The cancel button was pressed.");
        return;
    }
    public void NextAccessory()
    {
        currentIndex = (currentIndex + 1) % accessories.Count;
        PlayerPrefs.SetInt("selectedAccessoryIndex", currentIndex);
        UpdateDisplay();
    }



    public void PreviousAccessory()
    {
        currentIndex = (currentIndex - 1 + accessories.Count) % accessories.Count;
        PlayerPrefs.SetInt("selectedAccessoryIndex", currentIndex);
        UpdateDisplay();
    }



    public void SelectAccessory()
    {
        selectText.text = "ACTIVE";
        selectButton.interactable = false;
        PlayerPrefs.SetString("accessory", accessories[currentIndex].Name);
        Debug.Log("Current acc name: " + accessories[currentIndex].Name);
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
        if (!PlayerPrefs.HasKey("accessory"))
        {
            PlayerPrefs.SetString("accessory", "None");
            PlayerPrefs.SetInt("accessory0", 1);
        }
        Debug.Log(accessories[currentIndex].Name);
        Debug.Log(PlayerPrefs.GetString("accessory"));
        // unlocked bg
        if (PlayerPrefs.GetInt("accessory" + currentIndex) == 1)
        {
            selectButton.interactable = true;
            panelLocked.SetActive(false);
            ownedStatusText.text = "Owned";
            // if it's both unlocked and equipped
            Debug.Log("accessory" + currentIndex);
            Debug.Log(accessories[currentIndex].Name);

            if (PlayerPrefs.GetString("accessory") == accessories[currentIndex].Name)
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
            costText.text = accessories[currentIndex].Cost.ToString();
            selectButton.interactable = false;
            selectText.text = "";
            panelLocked.SetActive(true);
            ownedStatusText.text = "Not owned";
        }
        displayImage.sprite = accessories[currentIndex].Image;
        nameText.text = accessories[currentIndex].Name;
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
        // unsubscribe to avoid memory leaks
        if (confirmationScript != null)
        {
            confirmationScript.OnConfirmPress -= HandleConfirm;
            confirmationScript.OnCancelPress -= HandleCancel;
        }
    }
}
