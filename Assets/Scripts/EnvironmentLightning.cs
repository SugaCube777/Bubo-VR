using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentLightning : MonoBehaviour
{
    public Effect Effect;
    [SerializeField] float timer = -1;
    [SerializeField] GameObject VFX;

    public void AddEffect()
    {
        StartCoroutine(IAddEffect());
    }

    IEnumerator IAddEffect()
    {
        if (timer == -1)
            ResetTimer();

        timer -= Time.fixedDeltaTime;

        if (timer <= 0)
        {
            VFX.transform.SetParent(Player.Instance.transform);
            VFX.transform.position = Player.Instance.VFXPoint.position + Vector3.up * 4;

            VFX.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            VFX.gameObject.SetActive(false);

            Player.Instance.AddEffect(Effect.Clone());
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        timer = Random.Range(2f, 20f);
    }
}
