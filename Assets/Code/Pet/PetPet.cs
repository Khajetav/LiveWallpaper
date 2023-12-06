using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
using UnityEngine.UI;
using Unity.Services.Core;
using System.Threading.Tasks;

public class PetInteractionCloudSave : MonoBehaviour
{
    /*
    [SerializeField] private Text currencyText; // Assign this in the inspector
    private int heartsCurrency = 0;
    private const string currencyKey = "heartsCurrency"; // The key for the currency data in cloud save

    public GameObject heartPrefab; // Assign a heart prefab with a sprite in the inspector
    private float lastTapTime = 0f;
    private float tapCooldown = 60f; // Cooldown in seconds
    private bool canPet = true;
    public async void Start()
    {
        await UnityServices.InitializeAsync();
        LoadHeartsCurrency();
    }


    // TAP THE PET 
    private void OnMouseDown()
    {
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
        heartsCurrency++;
        var data = new Dictionary<string, object> { { currencyKey, heartsCurrency } };
        try
        {
            await CloudSaveService.Instance.Data.Player.SaveAsync(data);
            UpdateCurrencyUI();
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving data: " + e.Message);
        }
        
    }
    // Call this function to load the currency count from the cloud
    public async void LoadHeartsCurrency()
    {
        try
        {
            // Load the data asynchronously without a generic type argument
            var loadDataTask = CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { currencyKey });
            await loadDataTask;

            // If the task is completed successfully, process the results
            if (loadDataTask.Status == TaskStatus.RanToCompletion)
            {
                var loadData = loadDataTask.Result; // This is a Dictionary<string, object>

                // Check if the loadData contains the key and then extract the value if it exists
                if (loadData.TryGetValue(currencyKey, out object loadedCurrencyObject) && loadedCurrencyObject is int loadedCurrency)
                {
                    heartsCurrency = loadedCurrency;
                    UpdateCurrencyUI();
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error loading data: " + e.Message);
        }
    }

    // Update the currency text UI
    private void UpdateCurrencyUI()
    {
        currencyText.text = $"Hearts: {heartsCurrency}";
    }



    private IEnumerator ShowHeartEffect()
    {
        GameObject heartInstance = Instantiate(heartPrefab, transform.position, Quaternion.identity);
        heartInstance.transform.SetParent(transform); // Optional: Set as child of the pet object
        float duration = 2f; // Duration for the heart to float and disappear

        // Animate heart floating up
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            heartInstance.transform.position += new Vector3(0, Time.deltaTime, 0); // Float up
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(heartInstance); // Destroy the heart object after the animation
    }
    */
}
