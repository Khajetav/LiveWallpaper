using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonHandler : MonoBehaviour
{
    private Confirmation confirmationPanelScript;
    private FadeLogic fadeLogic;
    private void Awake()
    {
        fadeLogic = FindFirstObjectByType<FadeLogic>();
    }
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        //CurrencyHandler.SaveCurrency(100);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "mainScene")
            {
                return;
            }

            if (confirmationPanelScript == null)
            {
                confirmationPanelScript = FindFirstObjectByType<Confirmation>();
            }

            if (confirmationPanelScript != null && confirmationPanelScript.gameObject.activeInHierarchy)
            {
                confirmationPanelScript.CancelButtonClick();
            }
            else
            {
                fadeLogic.LoadSceneAfterFade("mainScene");
            }
        }
    }
}
