using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUtility : MonoBehaviour
{
    [SerializeField] string quest;

    [ContextMenu("TestAddItem")]
    void TestAddItem()
    {
        AddItem("Letter");
    }

    [ContextMenu("TestRemoveItem")]
    void TestRemoveItem()
    {
        RemoveItem("Letter");
    }

    public void AddGold(int amount)
    {
        Player.Instance.Gold = Mathf.Max(0, Player.Instance.Gold + amount);
    }

    public void AddItem(string name)
    {
        Player.Instance.Inventory.AddItem(name, transform.position);
        InteractableManager.Instance.Refresh();
    }

    public void AddQuestItem(string name)
    {
        Player.Instance.Inventory.AddItem(name, transform.position, quest);
        InteractableManager.Instance.Refresh();
    }

    public void RemoveItem(string name)
    {
        Player.Instance.Inventory.RemoveItem(name, transform.position);
        InteractableManager.Instance.Refresh();
    }
}
