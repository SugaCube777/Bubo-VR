using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUtility : MonoBehaviour
{
    public Quest Quest;

    public void AddQuest()
    {
        QuestManager.Instance.AddQuest(Quest);
    }

    public void CompleteQuest(string name)
    {
        QuestManager.Instance.CompelteQuest(name);
    }
}
