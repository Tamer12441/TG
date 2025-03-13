using UnityEngine;

public enum ItemType {Default, Box, Key, Coin}
public class ItemScriptableObject : ScriptableObject
{

    public string itemName;
    public int maximumAmount;
    public GameObject itemPrefab;
    public Sprite icon;
    public ItemType itemType;

    public bool isConsumeable;
    public string inHandName;
}
