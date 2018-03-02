using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : GeneralFunctions {

    private void OnParticleCollision(GameObject other)
    {
        // Debug.Log("AQUI" + other);
        if (other.tag == "Player" || other.tag == "Enemy")
        {

        }
        else if (!esTerra(other.tag))
        {
            other.GetComponent<IResistencia>().Destrossar(1);
        }
    }
}
