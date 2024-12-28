using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public static InteractableManager Instance;
    List<Interactable> interactables = new List<Interactable>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddInteractable(Interactable interactable)
    {
        interactables.Add(interactable);
    }

    public void Refresh()
    {
        foreach (var i in interactables)
        {
            i.Refresh();
        }
    }
}
