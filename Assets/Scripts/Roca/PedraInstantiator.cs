using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedraInstantiator : MonoBehaviour {

    public int probabilitat;
    public GameObject pedra;
    public GameObject granPedra;

	// Use this for initialization
	void Start () {
        InvokeRepeating("InstantiatePedra", 2f, 2f);
	}
	
	void InstantiatePedra()
    {
        if (Random.Range(0, probabilitat) == 0)
        {
            Vector3 position = new Vector3(transform.position.x + Random.Range(-2f, 2f), transform.position.y, transform.position.z + Random.Range(-2f, 2f));
            
            if (Random.Range(0, 4) == 0) { 
                Instantiate(granPedra, position, Quaternion.identity);
            }
            else Instantiate(pedra, position, Quaternion.identity);
        }
    }
}
