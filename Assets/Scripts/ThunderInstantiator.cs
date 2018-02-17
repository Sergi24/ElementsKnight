using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderInstantiator : MonoBehaviour {

    public int probabilitat;
    public GameObject thunder;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("InstantiateThunder", 2f, 2f);
    }

    void InstantiateThunder()
    {
        if (Random.Range(0, probabilitat) == 0)
        {
            Vector3 position = new Vector3(transform.position.x + Random.Range(-2f, 2f), 0, transform.position.z + Random.Range(-2f, 2f));
            Instantiate(thunder, position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        }
    }
}
