using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;


    public List<ItemData> Data = new List<ItemData>();
    public ItemConfigs Configs;
    Player player;
    [SerializeField] InventoryPanel inventoryPanel;
    [SerializeField] InputActionProperty inventoryPanelAction;

    public float Weight => Data.Sum(e => e.Weight) * (1 + player.Effects.Sum(e => e.WeightModifer));

    private void Awake()
    {
        if (!Instance) Instance = this;
        player = GetComponent<Player>();
    }

    private void Update()
    {
        Debug.Log("Inventory Weight: " + Weight);
        HandleSoundness();
        if (Input.GetKeyDown(KeyCode.Q) || inventoryPanelAction.action.triggered)
            inventoryPanel.gameObject.SetActive(!inventoryPanel.gameObject.activeSelf);
    }

    public void AddItem(string name, Vector3 position, string questName = null)
    {
        var data = Configs.Configs.Find(e => e.Name == name).GetCopy();

        if (data.Mesh)
        {
            var mesh = Instantiate(data.Mesh, position, Quaternion.identity);
            mesh.transform.DOMove(Player.Instance.VFXPoint.position, 2f).OnComplete(() => Destroy(mesh.gameObject));
            mesh.transform.DOLocalRotate(Vector3.one * 360, 2f, RotateMode.FastBeyond360);
        }

        if (!string.IsNullOrEmpty(questName))
        {
            data.Quest = questName;
        }
        Data.Add(data);

        inventoryPanel.UpdateViusal();
    }

    public void RemoveItem(string name, Vector3 position)
    {
        var data = Data.Find(e => e.Name == name);

        if (data == null) return;

        if (data.Mesh)
        {
            var mesh = Instantiate(data.Mesh, Player.Instance.VFXPoint.position, Quaternion.identity);
            mesh.transform.DOMove(position, 2f).SetEase(Ease.InSine).OnComplete(() => Destroy(mesh.gameObject));
            mesh.transform.DOLocalRotate(Vector3.one * 360, 2f, RotateMode.FastBeyond360);
        }

        Data.Remove(data);

        inventoryPanel.UpdateViusal();
    }

    void HandleSoundness()
    {
        float damage = player.Effects.Sum(e => e.SoundnessDamage);
        for (int i = 0; i < Data.Count; i++)
        {
            var item = Data[i];
            item.Soundness -= damage * Time.deltaTime;
            Data[i] = item;
        }
    }

    [ContextMenu("TestAddItem")]
    void TestAddItem()
    {
        AddItem("Letter", transform.position);
    }

    [ContextMenu("TestRemoveItem")]
    void TestRemoveItem()
    {
        RemoveItem("Letter", transform.position);
    }
}
