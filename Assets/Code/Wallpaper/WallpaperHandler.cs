using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the purpose of this class is to seamlessly change 
// and update all wallpaper previews and etc with what the 
// end user has selected to be as the default wallpaper
public class WallpaperHandler : MonoBehaviour
{
    private BackgroundObjectHandler backgroundObjectHandler;
    
    [SerializeField] private RawImage skyImage;
    [SerializeField] private RawImage floorImage;
    
    // trees that will be used to switch
    public List<Texture> spritesTreeNight = new List<Texture>();
    public List<Texture> spritesTreeDay = new List<Texture>();
    public List<Texture> spritesTreeNightJungle = new List<Texture>();
    public List<Texture> spritesTreeDayJungle = new List<Texture>();

    // mountains  that will be used to switch
    public List<Texture> spritesMountainNight = new List<Texture>();
    public List<Texture> spritesMountainDay = new List<Texture>();
    public List<Texture> spritesMountainNightJungle = new List<Texture>();
    public List<Texture> spritesMountainDayJungle = new List<Texture>();
    // sky images
    public Texture skyImageNight;
    public Texture skyImageDay;
    public Texture skyImageNightJungle;
    public Texture skyImageDayJungle;

    // floor images
    public Texture floorImageNight;
    public Texture floorImageDay;
    public Texture floorImageNightJungle;
    public Texture floorImageDayJungle;

    public void WallpaperUpdate()
    {
        backgroundObjectHandler = GetComponent<BackgroundObjectHandler>();
        string background = PlayerPrefs.GetString("background");
        Debug.Log("Wallpaper update: " + background);
        switch (background)
        {
            // fallthrough
            case "Night":
            default:
                // handle the sky
                skyImage.texture = skyImageNight;
                // handle the floor
                floorImage.texture = floorImageNight;
                // handle the trees
                backgroundObjectHandler.treeSprites = spritesTreeNight;
                // handle the mountains/background whatever
                backgroundObjectHandler.mountainSprites = spritesMountainNight;
                break;

            case "Day":
                skyImage.texture = skyImageDay;
                floorImage.texture = floorImageDay;
                backgroundObjectHandler.treeSprites = spritesTreeDay;
                backgroundObjectHandler.mountainSprites = spritesMountainDay;
                break;

            case "NightJungle":
                skyImage.texture = skyImageNightJungle;
                floorImage.texture = floorImageNightJungle;
                backgroundObjectHandler.treeSprites = spritesTreeNightJungle;
                backgroundObjectHandler.mountainSprites = spritesMountainNightJungle;
                break;

            case "DayJungle":
                skyImage.texture = skyImageDayJungle;
                floorImage.texture = floorImageDayJungle;
                backgroundObjectHandler.treeSprites = spritesTreeDayJungle;
                backgroundObjectHandler.mountainSprites = spritesMountainDayJungle ;
                break;


        }

    }
    
}