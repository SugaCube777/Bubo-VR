using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestArrow : MonoBehaviour
{
    Character character;
    [SerializeField] float closeDistance = 2;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject closeArrow;

    public void SetCharacter(Character character)
    {
        this.character = character;
    }

    // Update is called once per frame
    void Update()
    {
        if (character)
        {
            Vector3 direction = character.transform.position - transform.position;

            bool isClose = direction.sqrMagnitude <= closeDistance;

            arrow.SetActive(!isClose);
            closeArrow.SetActive(isClose);

            if (isClose)
                closeArrow.transform.position = character.transform.position;
            else
            {
                transform.position = QuestManager.Instance.transform.position;
                transform.forward = direction;
            }
        }

    }
}
