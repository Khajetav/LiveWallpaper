using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
using UnityEngine.UI;
using Unity.Services.Core;
using System;
using System.Threading.Tasks;
using Unity.Services.CloudSave.Models;
using UnityEditor.PackageManager;

public class CloudSaveScript : MonoBehaviour
{
    /*
    // tutorial followed:
    // CLOUD SAVE In Unity
    // https://www.youtube.com/watch?v=STuIobcdKzk
    // https://docs.unity.com/ugs/en-us/manual/cloud-save/manual
    // another tutorial
    // https://www.youtube.com/watch?v=WRKsmnmNpb4
    public GameObject text;
    public GameObject inputField;

    // dictionary is KEY:VALUE
    
    public async void Start()
    {
        await UnityServices.InitializeAsync();
    }
    
    public async void SaveData()
    {
        var client = CloudSaveService.Instance.Data;
        var data = new Dictionary<string, object>() { { "my_key", "Some data goes here" } };

        //var data = new Dictionary<string, object> { { "firstData", inputField.ToString() } };
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
    public async void LoadData()
    {
        var client = CloudSaveService.Instance.Data;
        var query = await client.LoadAsync(new HashSet<string> { "my_key" });
        var myData = query["my_key"];
        try
        {
            await CloudSaveService.Instance.Data.Player.LoadAsync("myData");
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving data: " + e.Message);
        }
    }

    public async void DeleteKey()
    {

        await CloudSaveService.Instance.Data.ForceDeleteAsync("firstData");
    }

    public async void RetriveAllKeys()
    {

        List<string> allKeys = await CloudSaveService.Instance.Data.RetrieveAllKeysAsync();

        for (int i = 0; i < allKeys.Count; i++)
        {
            print(allKeys[i]);
        }
    }
    */

}
