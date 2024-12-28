using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.ParticleSystem;


public class EnvironmentBuffer : MonoBehaviour
{
    [SerializeField] float particleDuration = 3;

    [SerializeField] UnityEvent OnEnter = new UnityEvent();
    [SerializeField] UnityEvent OnTrigger = new UnityEvent();
    [SerializeField] UnityEvent OnExit = new UnityEvent();
    
    public void Respawn()
    {
        Player.Instance.Respawn();
    }

    public void PlayParticleAtPlayer(ParticleSystem particle)
    {
        particle.transform.SetParent(null);
        particle.transform.position = Player.Instance.VFXPoint.position;
        particle.Play();
    }

    public void PlayParticleAtPlayerFront(ParticleSystem particle)
    {
        particle.transform.SetParent(null);
        particle.transform.position = Player.Instance.VFXFront.position;
        particle.Play();
    }

    public void PlayParticleAtPlayerFrontWithDuration(ParticleSystem particle)
    {
        StartCoroutine(IPlayParticleAtPlayerFrontWithDuration(particle));
    }

    IEnumerator IPlayParticleAtPlayerFrontWithDuration(ParticleSystem particle)
    {
        particle.transform.SetParent(Player.Instance.transform);
        particle.transform.position = Player.Instance.VFXFront.position;

        particle.Play();

        yield return new WaitForSeconds(particleDuration);

        particle.Stop();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnEnter.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            OnTrigger.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            OnExit.Invoke();
    }
}
