using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedraMove : AVida, IResistencia {
    private GameObject destination;
    private bool attacking;
    private bool selected;
    private Color initialColor;
    private Rigidbody rb;
    private float disminucioRoca;
    private AudioSource asource;

    public int moveSpeed, rotationSpeed;
    public Color selectedColor;
    public GameObject pedraExplosion;

    // Use this for initialization
    void Start () {
        attacking = false;
        selected = false;
        initialColor = gameObject.GetComponentInChildren<Renderer>().material.color;
        asource = gameObject.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        disminucioRoca = transform.localScale.x / 4;
    }
	
	// Update is called once per frame
	void Update () {

        if (attacking)
        {
            Vector3 puntoDeChoque;
            puntoDeChoque = new Vector3(destination.transform.position.x, destination.transform.position.y + 1, destination.transform.position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(puntoDeChoque - transform.position), rotationSpeed * Time.deltaTime);

            //Movimiento en dirección del target 
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            rb.WakeUp();
        }
	}

    public bool getSelected()
    {
        return selected;
    }

    public bool getAttacking()
    {
        return attacking;
    }

    public void Select()
    {
        selected = true;
        gameObject.GetComponentInChildren<Renderer>().material.color = selectedColor;
    }

    public void Attack(GameObject gameObject)
    {
        if (selected)
        {
            asource.Play();

            this.gameObject.GetComponentInChildren<Renderer>().material.color = initialColor;
            destination = gameObject;
            if (gameObject.tag == "Enemy")
            {
                gameObject.GetComponent<EnemyController>().Defend(gameObject);
            }
            rb.useGravity = false;
            attacking = true;
        }
    }

    public void Destrossar(int dany)
    {
        vida -= dany;
        Instantiate(pedraExplosion, transform.position, Quaternion.identity);
        if (vida <= 0)
        {
            Destroy(gameObject);
        }else
        {
            transform.localScale -= new Vector3(disminucioRoca, disminucioRoca, disminucioRoca);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (attacking)
        {
            string tag = collision.gameObject.tag;
            if (esRoca(tag))
            {
                if (tag == "Roca") Destrossar(1);
                else Destrossar(3);
            }
            else if (tag != "Player" && tag != "Enemy" && !esTerra(tag) && !esPedra(tag)) Destrossar(1);
            else if (tag == "Player" && destination.tag == "Player") Destrossar(1);
            else if (tag == "Enemy" && destination.tag == "Enemy") Destrossar(1);
      //      Destrossar(1);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (attacking)
        {
            if (tag == "Player" && destination.tag == "Player") Destroy(gameObject);
            else if (tag == "Enemy" && destination.tag == "Enemy") Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Thunder") Destrossar(1);
        else if (collider.gameObject.tag == "AquaBall")
        {
            collider.gameObject.GetComponent<IResistencia>().Destrossar(vida);
            Destrossar(vida);
        }
    }
}
