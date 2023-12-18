using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementLoader : MonoBehaviour
{
    [SerializeField] private GameObject backToTheRootsPanel;
    [SerializeField] private GameObject basePanel;
    [SerializeField] private GameObject bookshelfPanel;
    [SerializeField] private GameObject clickPanel;
    [SerializeField] private GameObject dayPanel;
    [SerializeField] private GameObject dayJunglePanel;
    [SerializeField] private GameObject goGreenPanel;
    [SerializeField] private GameObject hatPanel;
    [SerializeField] private GameObject tophatPanel;
    [SerializeField] private GameObject hatOnAHatOnAHatPanel;
    [SerializeField] private GameObject heartPanel;
    [SerializeField] private GameObject heartsPanel;
    [SerializeField] private GameObject heartssPanel;
    [SerializeField] private GameObject nightJunglePanel;
    [SerializeField] private GameObject thisOrThatPanel;
    [SerializeField] private GameObject wavePanel;
    private void Start()
    {
        if (PlayerPrefs.GetInt("Back To The Roots") == 1)
            backToTheRootsPanel.SetActive(false);

        if (PlayerPrefs.GetInt("All Your Baseballs Are Belong To Us") == 1)
            basePanel.SetActive(false);

        if (PlayerPrefs.GetInt("Background Keeper") == 1)
            bookshelfPanel.SetActive(false);

        if (PlayerPrefs.GetInt("Click!") == 1)
            clickPanel.SetActive(false);

        if (PlayerPrefs.GetInt("Morning!") == 1)
            dayPanel.SetActive(false);

        if (PlayerPrefs.GetInt("Wood Needed") == 1)
            dayJunglePanel.SetActive(false);

        if (PlayerPrefs.GetInt("You Did It.") == 1)
            goGreenPanel.SetActive(false);

        if (PlayerPrefs.GetInt("Hat.") == 1)
            hatPanel.SetActive(false);
        
        if (PlayerPrefs.GetInt("Bottom Top Hat") == 1)
            tophatPanel.SetActive(false);

        if (PlayerPrefs.GetInt("Hat On A Hat On A Hat") == 1)
            hatOnAHatOnAHatPanel.SetActive(false);

        if (PlayerPrefs.GetInt("Love Is In The Air!") == 1)
            heartPanel.SetActive(false);

        if (PlayerPrefs.GetInt("Baby Don't Hurt Me") == 1)
            heartsPanel.SetActive(false);

        if (PlayerPrefs.GetInt("I'm Lovin' It") == 1)
            heartssPanel.SetActive(false);

        if (PlayerPrefs.GetInt("It's Cold Out There") == 1)
            nightJunglePanel.SetActive(false);

        if (PlayerPrefs.GetInt("This Or That") == 1)
            thisOrThatPanel.SetActive(false);

        if (PlayerPrefs.GetInt("Hello World") == 1)
            wavePanel.SetActive(false);
    }

}
