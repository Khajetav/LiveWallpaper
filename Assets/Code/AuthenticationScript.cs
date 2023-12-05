using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;

public class AuthenticationScript : MonoBehaviour
{
    // tutorial followed:
    // AUTHENTICATION IN UNITY
    // https://www.youtube.com/watch?v=OHWo_xCdqHQ

    public TextMeshProUGUI textLog;

    async void Start()
    {
        await UnityServices.InitializeAsync();
    }

    // there is anonymous sign in
    // and google
    public async void SignIn()
    {
        await SignInAnonymous();
    }

    async Task SignInAnonymous()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            print("Sign in was a success");
            print("Player ID: " + AuthenticationService.Instance.PlayerId);
            textLog.text = "Sign in was a success\nPlayer ID: " + AuthenticationService.Instance.PlayerId;
        }
        catch (AuthenticationException e)
        {
            print("Sign in failed: " + e);
            textLog.text = "Sign in failed: " + e.Message;
        }

    }
}

