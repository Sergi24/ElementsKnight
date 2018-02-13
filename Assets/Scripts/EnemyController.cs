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
    private void buscarPedra(GameObject[] pedres)
    {
        foreach (GameObject pedra in pedres)
        {
            if (!pedra.GetComponent<PedraMove>().getSelected())
            {
                agent.destination = pedra.transform.position;
            }
            if ((pedra.transform.position - transform.position).magnitude < rangAtac)
            {
                if (!pedra.GetComponent<PedraMove>().getSelected())
                {
                    pedra.GetComponent<PedraMove>().Select();
                    pedra.GetComponent<PedraMove>().Attack(GameObject.FindGameObjectWithTag("Player"));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] pedres = GameObject.FindGameObjectsWithTag("GranPedra");
        buscarPedra(pedres);

        pedres = GameObject.FindGameObjectsWithTag("Pedra");
        buscarPedra(pedres);
    }

    public void Defend(GameObject atacant)
    {
        Vector3 posAtacant = atacant.transform.position;
        if (Random.Range(0, 3) == 0)
        {
            Vector3 direccio = posAtacant - transform.position;
            //   Instantiate(roca, transform.position + (transform.forward*4) - (transform.up*3), Quaternion.identity);
            Instantiate(roca, transform.position + (direccio * 0.2f) - (transform.up * 3), Quaternion.identity);
        }
    }
}
