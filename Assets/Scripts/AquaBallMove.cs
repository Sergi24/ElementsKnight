using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaBallMove : MonoBehaviour {

    private bool pistolaAiguaCreada;
    private GameObject pistolaAiguaNova;
    private bool destroy, flying, functionDestroyCalled, attached;
    private GameObject destination;
    private ParticleSystem ps;

    public GameObject pistolaAigua;
    public int moveSpeed, flyingTime;

    // Use this for initialization
    void Start () {
        ps = gameObject.GetComponent<ParticleSystem>();
        Invoke("Destrossar", 10f);
        pistolaAiguaCreada = false;
        destroy = false;
        flying = false;
        functionDestroyCalled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < 2 && !destroy && !flying)
        {
            transform.position += transform.up * 0.05f;
        }
        if (pistolaAiguaNova == null && !attached && !flying)
        {
            if (destroy && !functionDestroyCalled)
            {
                Destroy(gameObject, 3f);
                functionDestroyCalled = true;
            }
            if (destroy) transform.position -= Vector3.up * 0.05f;
            pistolaAiguaCreada = false;
        }
        if (flying && !destroy && !attached)
        {
            Vector3 puntoDeChoque;
            puntoDeChoque = new Vector3(destination.transform.position.x, destination.transform.position.y + 1, destination.transform.position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(puntoDeChoque - transform.position), 100 * Time.deltaTime);

            //Movimiento en dirección del target 
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            if (((destination.transform.position + Vector3.up) - transform.position).magnitude < 0.05f)
            {
                transform.SetParent(destination.transform);
                ParticleSystem.MainModule main = ps.main;
             //   main.simulationSpace = ParticleSystemSimulationSpace.World;
                main.startLifetime = 1f;
                ParticleSystem.EmissionModule emission = ps.emission;
                //   emission.rateOverTime = 30f;
                attached = true;
                destination.GetComponent<PlayerController>().FlyTime(flyingTime, gameObject);
                Destroy(gameObject, flyingTime);
            }
        }
	}

    private void Destrossar()
    {
        destroy = true;
    }

    public bool GetPistolaAiguaCreada()
    {
        return pistolaAiguaCreada;
    }

    public void FlyBegin(GameObject destination)
    {
        if (!pistolaAiguaCreada)
        {
            flying = true;
            this.destination = destination;
        }
    }

    private void OnMouseDown()
    {
        if (!pistolaAiguaCreada && !destroy)
        {
            pistolaAiguaNova = Instantiate(pistolaAigua, transform.position, Quaternion.identity);
            pistolaAiguaNova.GetComponent<PistolaAiguaMove>().Attack(GameObject.FindGameObjectWithTag("Enemy"));
            pistolaAiguaNova.transform.SetParent(transform);

            pistolaAiguaCreada = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (flying && collision.gameObject.Equals(destination))
        {

        }
    }
}
