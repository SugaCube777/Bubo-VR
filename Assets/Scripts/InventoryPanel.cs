using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Tutorials.Core.Editor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform grid;
    [SerializeField] GameObject detailPanel;
    [SerializeField] TMP_Text detailName;
    [SerializeField] TMP_Text detailSender;
    [SerializeField] TMP_Text detailReceiver;
    [SerializeField] TMP_Text detailDesc;
    [SerializeField] TMP_Text detailQuest;
    [SerializeField] Button detailTrack;
    [SerializeField] TMP_Text weightText;
    [SerializeField] TMP_Text CoinText;
    [SerializeField] Image weightBar;
    // Start is called before the first frame update
    void Start()
    {
        detailPanel.SetActive(false);
    }

    private void OnEnable()
    {
        UpdateViusal();
    }

    public void UpdateViusal()
    {
        if (!Inventory.Instance || !Player.Instance)
        {
            gameObject.SetActive(false);
            return;
        }

        var items = GetComponentsInChildren<InvenstoryPanelItem>();
        for (int i = 0; i < items.Length; i++)
            Destroy(items[i].gameObject);

        var list = Inventory.Instance.Data;
        foreach (var element in list)
        {
            var item = Instantiate(itemPrefab, grid).GetComponent<InvenstoryPanelItem>();
            item.SetItemData(element);
        }

        float weight = Inventory.Instance.Data.Sum(e => e.Weight);
        float weightMax = Player.Instance.WeightCapacity;
        weightBar.fillAmount = weight / weightMax;
        weightText.text = weight + " / " + weightMax;

        CoinAmountUI();
    }

    public void ShowDetail(ItemData data)
    {
        detailPanel.gameObject.SetActive(true);
        detailName.text = data.Name;
        detailDesc.text = data.Desc;

        bool isQuestItem = !string.IsNullOrEmpty(data.Quest);
        detailSender.gameObject.SetActive(isQuestItem);
        detailReceiver.gameObject.SetActive(isQuestItem);
        detailQuest.gameObject.SetActive(isQuestItem);
        detailTrack.gameObject.SetActive(isQuestItem);
        if (isQuestItem)
        {
            var quest = QuestManager.Instance.Quests.Find(e => e.Name == data.Quest);
            detailSender.text = quest.Sender;
            detailQuest.text = data.Quest;
            detailReceiver.text = quest.Receivers[0];
            detailTrack.onClick.RemoveAllListeners();
            detailTrack.onClick.AddListener(() => QuestManager.Instance.SelectQuestByName(data.Quest));
        }
    }

    public void CoinAmountUI() 
    {
        CoinText.SetText(Player.Instance.Gold.ToString());
    }
}
