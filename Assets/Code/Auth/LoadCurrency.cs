using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadCurrency : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("HeartsCurrency"))
        {
            CurrencyHandler.SaveCurrency(0);
        }
        currencyText.text = CurrencyHandler.LoadCurrency().ToString();
    }
}
