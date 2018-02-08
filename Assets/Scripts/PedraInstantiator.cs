using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedraInstantiator : MonoBehaviour {

    public int probabilitat;
    public GameObject pedra;
    public GameObject granPedra;

	// Use this for initialization
	void Start () {
        InvokeRepeating("InstantiatePedra", 1f, 1f);
	}
	
	void InstantiatePedra()
    {
        if (Random.Range(0, probabilitat) == 0)
        {
            if (Random.Range(0, 4) == 0) { 
                Instantiate(granPedra, transform.position, Quaternion.identity);
            }
            else Instantiate(pedra, transform.position, Quaternion.identity);
        }
    }
}
