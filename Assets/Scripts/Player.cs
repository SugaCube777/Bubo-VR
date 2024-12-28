using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public List<Effect> Effects = new List<Effect>();
    [SerializeField] List<PlayerRespawnPoint> respawnPoints = new List<PlayerRespawnPoint>();

    [Header("Resources")]
    public int Gold = 0;
    // TODO: EQUIP

    [Header("Attributes")]
    public float Hunger = 0;
    public float HungerMax = 100;
    public float Speed = 1;
    public float WeightCapacity = 100;

    [Header("Data")]
    public bool IsFlightMode = false;

    [Header("Components")]
    public Inventory Inventory;
    public Transform VFXFront;
    public Transform VFXPoint;
    public ParticleSystem Respawnfeather;

    private void Awake()
    {
        if (!Instance) Instance = this;
        Inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        HandleEffects();
    }

    public void AddRespawnPoint(PlayerRespawnPoint point)
    {
        respawnPoints.Add(point);
    }

    [ContextMenu("Test Respawn")]
    public void Respawn()
    {
        respawnPoints.Sort((x, y) => (x.transform.position - transform.position).sqrMagnitude.CompareTo((y.transform.position - transform.position).sqrMagnitude));
        StartCoroutine(IRespawn());
        Respawnfeather.Play();
    }

    IEnumerator IRespawn()
    {
        UIBlackscreen.Instance.FadeIn(4);
        yield return new WaitForSeconds(4);

        transform.position = respawnPoints[0].transform.position;

        UIBlackscreen.Instance.FadeOut(3);
        yield return new WaitForSeconds(3);
    }

    public void AddEffect(Effect effect)
    {
        if (effect.IsUnique)
        {
            Effects.RemoveAll(e => e.ID == effect.ID);
            
        }
        Effects.Add(effect);
    }

    [ContextMenu("TestAddEffect")]
    void TestAddEffect()
    {
        Inventory.AddItem("Letter", transform.position);

        Effect effect = new Effect();
        effect.ID = "Rain";
        effect.Duration = 4;
        effect.WeightModifer = 0.5f;
        effect.SoundnessDamage = 0.1f;
        effect.IsUnique = true;

        //AddEffect(effect);
    }

    public void HandleEffects()
    {
        foreach (var effect in Effects)
        {
            effect.Duration -= Time.deltaTime;
        }

        Effects.RemoveAll(e => e.Duration <= 0);
    }
}
