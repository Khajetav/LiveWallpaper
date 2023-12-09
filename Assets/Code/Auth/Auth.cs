using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using System.Threading.Tasks;

public class Auth : MonoBehaviour
{
    async void Start()
    {
        await UnityServices.InitializeAsync();
        await SignInAnonymous();
    }

    async Task SignInAnonymous()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in was a success");
            Debug.Log("Player ID: " + AuthenticationService.Instance.PlayerId);
        }
        catch (AuthenticationException e)
        {
            Debug.Log("Sign in failed: " + e);
        }
    }
    
    private void OnApplicationQuit()
    {
        CurrencyHandler.SaveCurrencyCloud();
    }
}
