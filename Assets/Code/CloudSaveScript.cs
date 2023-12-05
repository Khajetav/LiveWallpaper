using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
using UnityEngine.UI;
using Unity.Services.Core;
using System;

public class CloudSaveScript : MonoBehaviour
{
    // tutorial followed:
    // CLOUD SAVE In Unity
    // https://www.youtube.com/watch?v=STuIobcdKzk

    
    public GameObject text;
    public GameObject inputField;

    // dictionary is KEY:VALUE
    
    public async void Start()
    {
        await UnityServices.InitializeAsync();
    }
    
    public async void SaveData()
    {
        var data = new Dictionary<string, object> { { "firstData", inputField.ToString() } };
        //await CloudSaveService.Instance.Data.ForceSaveAsync(data);
        // this method is now deprecated, gotta use
        try
        {
            await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving data: " + e.Message);
        }
    }

    
}
