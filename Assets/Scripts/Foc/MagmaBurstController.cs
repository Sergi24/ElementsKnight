using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaBurstController : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "FirePlayer" && !other.gameObject.GetComponent<PlayerController>().IsCreatingFireballs())
        {
            other.gameObject.transform.Find("HandsFire").gameObject.SetActive(true);
            ParticleSystem.EmissionModule emissionFireball = other.gameObject.transform.Find("Fireball").gameObject.GetComponent<ParticleSystem>().emission;
            emissionFireball.rateOverTime = 0.8f;
            other.gameObject.GetComponent<PlayerController>().FireballRepetitivaCreada();

            gameObject.GetComponent<SphereCollider>().enabled = false;
            Invoke("DisableMagmaBurstParticles", 0.5f);
        }
    }

    public void DisableMagmaBurstParticles()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        ParticleSystem[] particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particleSystem in particleSystems)
        {
            ParticleSystem.EmissionModule emissionMagmaBurst = particleSystem.emission;
            emissionMagmaBurst.rateOverDistance = 0f;
            emissionMagmaBurst.rateOverTime = 0f;
        }

    }
}
