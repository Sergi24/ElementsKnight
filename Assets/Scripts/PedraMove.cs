using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedraMove : GeneralFunctions {
    private GameObject destination;
    private bool attacking;
    private bool selected;
    private Color initialColor;
    private Rigidbody rb;
    private float disminucioRoca;

    public int moveSpeed, rotationSpeed;
    public Color selectedColor;
    public int resistencia;

    // Use this for initialization
    void Start () {
        attacking = false;
        selected = false;
        initialColor = gameObject.GetComponentInChildren<Renderer>().material.color;
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
	}

    public bool getSelected()
    {
        return selected;
    }

    public bool getAttacking()
    {
        return attacking;
    }

    public void Selected()
    {
        selected = true;
        gameObject.GetComponentInChildren<Renderer>().material.color = selectedColor;
    }

    public void Attack(GameObject gameObject)
    {
        if (selected)
        {
            this.gameObject.GetComponentInChildren<Renderer>().material.color = initialColor;
            destination = gameObject;
            if (gameObject.tag == "Enemy")
            {
                gameObject.GetComponent<EnemyController>().Defend();
            }
            rb.useGravity = false;
            attacking = true;
        }
    }

    public void Destrossar(int dany)
    {
        resistencia -= dany;
        if (resistencia <= 0)
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
            if (esRoca(collision.gameObject))
            {
                if (collision.gameObject.tag == "Roca") Destrossar(1);
                else Destrossar(3);
            }
            else if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Enemy" && !esTerra(collision.gameObject) && !esPedra(collision.gameObject)) Destrossar(1);
            else if (collision.gameObject.tag == "Player" && destination.tag == "Player") Destrossar(1);
            else if (collision.gameObject.tag == "Enemy" && destination.tag == "Enemy") Destrossar(1);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (attacking)
        {
            if (collision.gameObject.tag == "Player" && destination.tag == "Player") Destroy(gameObject);
            else if (collision.gameObject.tag == "Enemy" && destination.tag == "Enemy") Destroy(gameObject);
        }
    }
}
