using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the purpose of this class is to seamlessly change 
// and update all wallpaper previews and etc with what the 
// end user has selected to be as the default wallpaper
public class AccessoryHandler : MonoBehaviour
{
    private void Start()
    {
        AccessoryUpdate();
    }
    [SerializeField] private Image accessoryImage;
 
    public Sprite accessoryImageNone;
    public Sprite accessoryImageFancyHat;
    public Sprite accessoryImageTophat;
    public Sprite accessoryImageBase;


    public void AccessoryUpdate()
    {
        string accessory = PlayerPrefs.GetString("accessory");
        Debug.Log("accessory update: " + accessory);
        switch (accessory)
        {
            // fallthrough
            case "Bald":
            default:
                accessoryImage.sprite = accessoryImageNone;
                break;

            case "Fancy hat":
                accessoryImage.sprite = accessoryImageFancyHat;
                break;

            case "Tophat":
                accessoryImage.sprite = accessoryImageTophat;
                break;

            case "Base":
                accessoryImage.sprite = accessoryImageBase;
                break;
        }

    }
    
}
