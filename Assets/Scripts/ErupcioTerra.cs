﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErupcioTerra : GeneralFunctions {

    private void OnParticleCollision(GameObject other)
    {
       // Debug.Log("AQUI" + other);
       if (other.tag == "Player" || other.tag == "Enemy")
        {

        }
        else if (!esTerra(other.tag))
        {
            Debug.Log(other);
            other.GetComponent<IResistencia>().Destrossar(1);
        }
    }
}
