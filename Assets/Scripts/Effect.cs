using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect
{
    public string ID = "Effect";
    public float Duration = 0; // s
    public bool IsUnique = false;

    // Fly
    public float FalpForceModifer = 0; // %
    public bool BlockControl = false;
    public Vector3 VelocityModifier = Vector3.zero;
    public bool HasTurbulence = false;

    // Inventory
    public float WeightModifer = 0; // %
    public float SoundnessDamage = 0; // /s

    // UI
    public bool BlockNavigationUI = false;

    public Effect Clone()
    {
        Effect clone = new Effect();
        clone.ID = ID;
        clone.Duration = Duration;
        clone.IsUnique = IsUnique;
        clone.FalpForceModifer = FalpForceModifer;
        clone.BlockControl = BlockControl;
        clone.VelocityModifier = VelocityModifier;
        clone.HasTurbulence = HasTurbulence;
        clone.WeightModifer = WeightModifer;
        clone.SoundnessDamage = SoundnessDamage;
        clone.BlockNavigationUI = BlockNavigationUI;
        return clone;
    }
}
