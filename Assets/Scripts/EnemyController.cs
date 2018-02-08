using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    public int rangAtac;
    public int velocitat;
    public GameObject roca;

    private GameObject destination;
    private NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] pedres = GameObject.FindGameObjectsWithTag("Pedra");
        if (pedres.Length != 0) agent.destination = pedres[0].transform.position;

        foreach (GameObject pedra in pedres) {
            if ((pedra.transform.position-transform.position).magnitude < rangAtac)
            {
                if (!pedra.GetComponent<PedraMove>().getSelected())
                {
                    pedra.GetComponent<PedraMove>().Selected();
                    pedra.GetComponent<PedraMove>().Attack(GameObject.FindGameObjectWithTag("Player"));
                }
            }
        }
    }

    public void Defend()
    {
        if (Random.Range(0, 1) == 0)
        {
            Instantiate(roca, transform.position + (transform.forward*4) - (transform.up*3), Quaternion.identity);
        }
    }
}
