using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BackgroundObject
{
    public string Name;
    public int Cost;
    public Sprite Image;
    public bool IsUnlocked;

    public BackgroundObject(string name, int cost, Sprite image, bool isUnlocked = false)
    {
        Name = name;
        Cost = cost;
        Image = image;
        IsUnlocked = isUnlocked;
    }
}
