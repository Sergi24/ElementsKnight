using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerraMovedisaController : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") other.gameObject.GetComponent<PlayerController>().Arralentir();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") other.gameObject.GetComponent<PlayerController>().Desarralentir();
    }
}
