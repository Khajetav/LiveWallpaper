using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    public GameObject currencyHolder;
    public GameObject selectButton;
    public GameObject imageLock;
    public Confirmation confirmationScript; 
    private int currentIndex = 0;

    void Start()
    {
        //CurrencyHandler.SaveCurrency(20);
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
    private IEnumerator ShakeCoroutine()
    {
        Vector3 originalPosition = imageLock.transform.localPosition;
        float elapsed = 0.0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            // animation dies down with time but doesn't crash because it divided by 0
            float x = Random.Range(-1f, 1f) * 5f * (elapsed == 0 ? 0 : (duration / elapsed));

            //float y = Random.Range(-1f, 1f) * 0.2f;

            imageLock.transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        imageLock.transform.localPosition = originalPosition;
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
        UpdateDisplay();
    }



    public void PreviousBackground()
    {
        currentIndex = (currentIndex - 1 + backgrounds.Count) % backgrounds.Count;
        UpdateDisplay();
    }



    public void SelectBackground()
    {
        if (backgrounds[currentIndex].IsUnlocked)
        {

        }
    }

    private void UpdateDisplay()
    {
        Debug.Log(backgrounds[currentIndex].Name);
        // unlocked bg
        if (backgrounds[currentIndex].IsUnlocked)
        {
            selectButton.SetActive(true);
            currencyHolder.SetActive(false);
        }
        // locked bg
        else
        {
            costText.text = backgrounds[currentIndex].Cost.ToString();
            selectButton.SetActive(false);
            currencyHolder.SetActive(true);
        }
        displayImage.sprite = backgrounds[currentIndex].Image;
        nameText.text = backgrounds[currentIndex].Name;
        heartsCurrencyOwnedText.text = CurrencyHandler.LoadCurrency().ToString();

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
