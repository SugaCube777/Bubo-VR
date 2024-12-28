using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("Data")]
    public List<Quest> Quests = new List<Quest>();
    public List<Quest> CompletedQuests = new List<Quest>();
    Quest currentQuest;

    [Header("Navigation")]
    [SerializeField] List<Character> currentReceivers = new List<Character>();
    [SerializeField] GameObject questArrowPrefab;
    List<GameObject> questArrows = new List<GameObject>(); 
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectQuestByName(string questName)
    {
        var quest = Quests.Find(e => e.Name == questName);
        SelectQuest(quest);
    }

    public void SelectQuest(Quest quest)
    {
        currentQuest = quest;
        currentReceivers.Clear();

        if (quest != null)
            foreach (var receiver in quest.Receivers)
                currentReceivers.Add(CharacterManager.Instance.GetCharacter(receiver));

        foreach (var arrow in questArrows)
            Destroy(arrow.gameObject);

        if (quest != null)
            foreach (var receiver in currentReceivers)
            {
                var arrow = Instantiate(questArrowPrefab).GetComponent<QuestArrow>();
                arrow.SetCharacter(receiver);
            }
    }

    public void AddQuest(Quest quest)
    {
        Quests.Add(quest);
        if (currentQuest == null)
            SelectQuest(quest);
        InteractableManager.Instance.Refresh();
    }

    public void CompelteQuest(string name)
    {
        var quest = Quests.Find(e => e.Name == name);
        if (quest != null)
        {
            Quests.Remove(quest);
            CompletedQuests.Add(quest);
        }
        if (currentQuest == quest)
            SelectQuest(null);
        InteractableManager.Instance.Refresh();
    }
}

[System.Serializable]
public class Quest
{
    public string Name;
    [TextArea] public string Description;

    public string Sender;
    public List<string> Receivers;
}
