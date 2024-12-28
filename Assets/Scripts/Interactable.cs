using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction;

public class Interactable : MonoBehaviour
{
    public string ItemCondition = string.Empty;
    public string QuestCondition = string.Empty;
    public UnityEvent OnInteract = new UnityEvent();

    private void Start()
    {
        InteractableManager.Instance.AddInteractable(this);
        Refresh();
    }

    [ContextMenu("Interact")]
    public void Interact()
    {
        OnInteract.Invoke();
    }

    public void Refresh()
    {
        if (!string.IsNullOrEmpty(QuestCondition))
            gameObject.SetActive(QuestManager.Instance.Quests.Find(e => e.Name == QuestCondition) != null);

        if (!string.IsNullOrEmpty(ItemCondition))
        {
            var item = Inventory.Instance.Data.Find(e => e.Name == ItemCondition);
            gameObject.SetActive(item != null);
        }
            
    }
}
