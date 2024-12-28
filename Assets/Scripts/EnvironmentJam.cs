using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentJam : MonoBehaviour
{
    public Effect Effect;

    public void AddEffect()
    {
        Player.Instance.AddEffect(Effect.Clone());
    }
}
