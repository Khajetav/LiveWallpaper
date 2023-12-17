using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AccessoryObject
{
    public string Name;
    public int Cost;
    public Sprite Image;
    public bool IsUnlocked;

    public AccessoryObject(string name, int cost, Sprite image, bool isUnlocked = false)
    {
        Name = name;
        Cost = cost;
        Image = image;
        IsUnlocked = isUnlocked;
    }
}
