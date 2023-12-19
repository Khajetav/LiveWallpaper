using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using System.Threading.Tasks;
using Unity.VisualScripting;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;

public class PetPet : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private Transform transformMainCanvas;
    public AchievementManager achievementManager;

    // the heart that will pop out of the pet
    public GameObject heartPrefab;
    //private float lastTapTime = 0f;
    //private float tapCooldown = 0f;
    public void Start()
    {
        currencyText.text = CurrencyHandler.LoadCurrency().ToString();
        UpdateCurrencyUI();
    }

    // TAP THE PET 
    public void OnPet()
    {
        // default cooldown is 60f
        //if (Time.time - lastTapTime >= tapCooldown)
        //{
        //    lastTapTime = Time.time;

            PlayerPrefs.SetInt("heartsTotal",PlayerPrefs.GetInt("heartsTotal", 0) + 1);
            int currency = CurrencyHandler.LoadCurrency();
            currency++;
            if (currency >= 10)
            {
                achievementManager.UnlockAchievement("Love Is In The Air!");
            }
            if (currency >= 50)
            {
                achievementManager.UnlockAchievement("Baby Don't Hurt Me");
            }
            if (currency >= 100)
            {
                achievementManager.UnlockAchievement("I'm Lovin' It");
            }
            
            CurrencyHandler.SaveCurrency(currency);
            UpdateCurrencyUI();
            StartCoroutine(ShowHeartEffect());
        //}
    }
    

    // Update the currency text UI
    private void UpdateCurrencyUI()
    {
        currencyText.text = CurrencyHandler.LoadCurrency().ToString();
    }

    private IEnumerator ShowHeartEffect()
    {
        GameObject heartInstance1 = Instantiate(heartPrefab, transform.position, Quaternion.identity);
        GameObject heartInstance2 = Instantiate(heartPrefab, transform.position, Quaternion.identity);
        GameObject heartInstance3 = Instantiate(heartPrefab, transform.position, Quaternion.identity);
        heartInstance1.transform.SetParent(transformMainCanvas);
        heartInstance2.transform.SetParent(transformMainCanvas); 
        heartInstance3.transform.SetParent(transformMainCanvas);


        float duration = 2f; 
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float wave = Mathf.Sin(elapsedTime);
            heartInstance1.transform.position += new Vector3(wave, 1f, 0); 
            heartInstance1.transform.localScale = heartInstance1.transform.localScale * 0.99f;

            heartInstance2.transform.position += new Vector3(wave*1.3f, 0.8f, 0);
            heartInstance2.transform.localScale = heartInstance2.transform.localScale * 0.95f;

            heartInstance3.transform.position += new Vector3(wave*0.57f, 0.95f, 0);
            heartInstance3.transform.localScale = heartInstance3.transform.localScale * 0.95f;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(heartInstance1); 
        Destroy(heartInstance2); 
        Destroy(heartInstance3); 
    }
    
}
