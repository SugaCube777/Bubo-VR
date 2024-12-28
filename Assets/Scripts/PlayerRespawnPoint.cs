using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerRespawnPoint : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.AddRespawnPoint(this);

    }
}
