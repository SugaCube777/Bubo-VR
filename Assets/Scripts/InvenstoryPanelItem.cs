using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvenstoryPanelItem : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text weightText;
    [SerializeField] Image soundness;
    
    ItemData itemData;

    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowDetail);
    }

    public void SetItemData(ItemData data)
    {
        itemData = data;
        icon.sprite = data.Icon;
        weightText.text = data.Weight.ToString();
        soundness.fillAmount = data.Soundness;
    }

    void ShowDetail()
    {
        GetComponentInParent<InventoryPanel>().ShowDetail(itemData);
    }
}
