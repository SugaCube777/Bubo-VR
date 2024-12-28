using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfigs", menuName = "ScriptableObjects/ItemConfigs", order = 1)]
public class ItemConfigs : ScriptableObject
{
    public List<ItemData> Configs = new List<ItemData>();
}

[System.Serializable]
public class ItemData
{
    public string Name;
    public ItemType Type;
    public string Sender;
    public string Receiver;
    [TextArea] public string Desc;
    public float Weight;
    public Sprite Icon;
    public GameObject Mesh;

    [Header("Package")]
    public float Soundness;
    public float Time;
    public string Quest;

    [Header("Food")]
    public float Hunger;

    public ItemData GetCopy()
    {
        ItemData itemData = new ItemData();
        itemData.Name = Name;
        itemData.Type = Type;
        itemData.Sender = Sender;
        itemData.Receiver = Receiver;
        itemData.Desc = Desc;
        itemData.Weight = Weight;
        itemData.Icon = Icon;
        itemData.Mesh = Mesh;
        itemData.Soundness = Soundness;
        itemData.Time = Time;
        itemData.Quest = Quest;
        itemData.Hunger = Hunger;
        return itemData;
    }
}

public enum ItemType
{
    None,
    Package,
    Food
}

