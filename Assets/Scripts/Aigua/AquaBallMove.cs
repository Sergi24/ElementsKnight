using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaBallMove : AVida, IResistencia {

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
        pistolaAiguaCreada = false;
        destroy = false;
        flying = false;
        functionDestroyCalled = false;
        destination = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < 2 && !destroy && !flying)
        {
            transform.position += transform.up * 0.05f;
        }
        else if (!flying && !destroy)
        {
            Vector3 puntoDeChoque;
            puntoDeChoque = new Vector3(destination.transform.position.x, 2f, destination.transform.position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(puntoDeChoque - transform.position), 100 * Time.deltaTime);

            //Movimiento en dirección del target 
            if ((puntoDeChoque - transform.position).magnitude > 4f)
                transform.position += transform.forward * moveSpeed * 0.3f * Time.deltaTime;
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
        if (flying && vida == 3 && !attached)
        {
            Vector3 puntoDeChoque;
            puntoDeChoque = new Vector3(destination.transform.position.x, destination.transform.position.y + 1.2f, destination.transform.position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(puntoDeChoque - transform.position), 100 * Time.deltaTime);

            //Movimiento en dirección del target 
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            if (((destination.transform.position + new Vector3(0, 1.2f, 0)) - transform.position).magnitude < 0.05f)
            {
                transform.SetParent(destination.transform);

                attached = true;

                destination.GetComponent<PlayerController>().FlyTime(flyingTime);
                Destroy(gameObject, flyingTime);
            }
        }
        if (pistolaAiguaNova != null)
        {
            transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
            if (transform.localScale.x < 0.3f)
            {
                Destroy(pistolaAiguaNova);
                destroy = true;
            }
        } else if (transform.localScale.x < 0.55f) destroy = true;
        if (destroy) gameObject.GetComponent<SphereCollider>().enabled = false;
	}

    public void Destrossar(int dany)
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f) * dany;
    }

    public bool GetPistolaAiguaCreada()
    {
        return pistolaAiguaCreada;
    }

    public bool GetFlying()
    {
        return flying;
    }

    public void FlyBegin(GameObject destination)
    {
        if (!pistolaAiguaCreada && transform.localScale.x > 0.95f)
        {
            flying = true;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            ParticleSystem.MainModule main = ps.main;
            main.startLifetime = 1f;
            this.destination = destination;
        }
    }

    public void Attack(GameObject destination)
    {
        if (!pistolaAiguaCreada && !destroy)
        {
            pistolaAiguaNova = Instantiate(pistolaAigua, transform.position, Quaternion.identity);
            pistolaAiguaNova.GetComponent<PistolaAiguaMove>().Attack(destination);
            pistolaAiguaNova.transform.SetParent(transform);

            pistolaAiguaCreada = true;
        }
    }

    private void OnMouseDown()
    {
        Attack(GameObject.FindGameObjectWithTag("Enemy"));
    }

    private void OnTriggerEnter(Collider collider)
    {
   //     if (!esTerra(collider.gameObject.tag) && collider.gameObject.tag != "Player" && collider.gameObject.tag != "PistolaAigua") Destrossar(1);
    }
}
