using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using UnityEngine;
using Unity.Services.Core;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;
using System.Linq.Expressions;
using Unity.Services.CloudSave;
using System;

public class LoginExample : MonoBehaviour
{


    // this is where the debug data will be shown
    [SerializeField]
    private TextMeshProUGUI textStatus;
    // you can write smth here and it'll get saved in the cloud
    [SerializeField]
    private TextMeshProUGUI textInput;
    // overwrites this field when you load data
    [SerializeField]
    private TextMeshProUGUI textData;

    // NOTE
    // must be ASYNC VOID START
    // NOT VOID START
    async void Start()
    {
        // must start UnityServices
        await UnityServices.InitializeAsync();
        // must login first, anon is easiest
        await SignInAnonymous();

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
    public async void SaveButtonClick()
    {
        try
        {
            string textToSave = "no input";
            if(textInput.text != null)
            {
                textToSave = textInput.text;
            }
            await CloudSaveWrapper.Save("exampleKey", textToSave);
            if(textStatus) textStatus.text = "Successfully saved: " + textToSave ;
        }
        catch (CloudSaveException e)
        {
            Debug.LogError($"Error saving data: {e.Message}");
            if(textStatus)
            {
                textStatus.text = e.Message;
            }
        }
    }

    public async void LoadButtonClick()
    {
        try
        {
            string example = await CloudSaveWrapper.Load<string>("exampleKey");
            if (textStatus) textStatus.text = "Successfully loaded: " + example;
            if (textData) textData.text = example;
        }
        catch (CloudSaveException e)
        {
            Debug.LogError($"Error saving data: {e.Message}");
            if (textStatus)
            {
                textStatus.text = e.Message;
            }
        }
    }

    public async void UpdateButtonClick()
    {
        try
        {
            string dataToUpdateWith = "Update test";
            if(textInput.text != null)
            {
                dataToUpdateWith = textInput.text;
            }
            await CloudSaveWrapper.UpdateData("exampleKey", dataToUpdateWith);
            if (textStatus) textStatus.text = "Successfully updated: " + dataToUpdateWith;
        }
        catch (CloudSaveException e)
        {
            Debug.LogError($"Error updating data: {e.Message}");
            if (textStatus)
            {
                textStatus.text = e.Message;
            }
        }
    }

    public async void DeleteButtonClick()
    {
        try
        {
            await CloudSaveWrapper.Delete("exampleKey");
            if (textStatus)
                textStatus.text = "Successfully deleted";
        }
        catch(Exception e)
        {
            textStatus.text = e.Message;
            Debug.LogError($"Error saving data: {e.Message}");
        }
    }
}
