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
    private int currentIndex = 0;

    void Start()
    {
        UpdateDisplay();
        heartsCurrencyOwnedText.text = CurrencyHandler.LoadCurrency().ToString();
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

    public void UnlockBackground()
    {
        int currentCurrency = CurrencyHandler.LoadCurrency();
        if (!backgrounds[currentIndex].IsUnlocked && currentCurrency >= backgrounds[currentIndex].Cost)
        {
            currentCurrency -= backgrounds[currentIndex].Cost;
            CurrencyHandler.SaveCurrency(currentCurrency);
            backgrounds[currentIndex].IsUnlocked = true;
            UpdateDisplay();
        }
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
}
