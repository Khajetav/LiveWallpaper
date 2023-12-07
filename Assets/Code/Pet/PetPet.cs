using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
using UnityEngine.UI;
using Unity.Services.Core;
using System.Threading.Tasks;
using Unity.VisualScripting;
using TMPro;
using Unity.Services.Authentication;

public class PetInteractionCloudSave : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private Transform transformMainCanvas;
    private int heartsCurrency = 0;


    // the heart that will pop out of the pet
    public GameObject heartPrefab;
    private float lastTapTime = 0f;
    private float tapCooldown = 2f; 
    public async void Start()
    {
        // must start UnityServices
        await UnityServices.InitializeAsync();
        // must login first, anon is easiest
        await SignInAnonymous();
        LoadHeartsCurrency();
        if (PlayerPrefs.HasKey("HeartsCurrency"))
        {
            heartsCurrency = PlayerPrefs.GetInt("HeartsCurrency");
        }
        currencyText.text = heartsCurrency.ToString();
    }

    async Task SignInAnonymous()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in was a success");
            Debug.Log("Player ID: " + AuthenticationService.Instance.PlayerId);
            //textLog.text = "Sign in was a success\nPlayer ID: " + AuthenticationService.Instance.PlayerId;
        }
        catch (AuthenticationException e)
        {
            Debug.Log("Sign in failed: " + e);
            //textLog.text = "Sign in failed: " + e.Message;
        }
    }
    // TAP THE PET 
    public void OnPet()
    {
        Debug.Log("Click registered");
        // default cooldown is 60f
        if (Time.time - lastTapTime >= tapCooldown)
        {
            lastTapTime = Time.time;
            IncreaseHeartsCurrency();
            StartCoroutine(ShowHeartEffect());
        }
    }
    
    // COMMUNICATE WITH CLOUD SAVE
    public async void IncreaseHeartsCurrency()
    {
        heartsCurrency = await CloudSaveWrapper.Load<int>("heartsKey");
        heartsCurrency++;
        PlayerPrefs.SetInt("HeartsCurrency", heartsCurrency);
        PlayerPrefs.Save();
        await CloudSaveWrapper.Save<int>("heartsKey", heartsCurrency);
        UpdateCurrencyUI();
    }
    // Call this function to load the currency count from the cloud
    public async void LoadHeartsCurrency()
    {
        heartsCurrency = await CloudSaveWrapper.Load<int>("heartsKey");
        UpdateCurrencyUI();
    }

    // Update the currency text UI
    private void UpdateCurrencyUI()
    {
        currencyText.text = $"Hearts: {heartsCurrency}";
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
            heartInstance1.transform.localScale = heartInstance1.transform.localScale * 0.998f;

            heartInstance2.transform.position += new Vector3(wave*1.3f, 0.8f, 0);
            heartInstance2.transform.localScale = heartInstance2.transform.localScale * 0.997f;

            heartInstance3.transform.position += new Vector3(wave*0.57f, 0.95f, 0);
            heartInstance3.transform.localScale = heartInstance3.transform.localScale * 0.997f;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(heartInstance1); 
        Destroy(heartInstance2); 
        Destroy(heartInstance3); 
    }
    
}
