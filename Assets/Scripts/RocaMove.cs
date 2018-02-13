using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocaMove : MonoBehaviour {
    private float limitDalt, limitDreta, limitEsquerra;
    private bool destrossat;
    private AudioSource asource;

    public float duracio;
    public int resistencia;

	// Use this for initialization
	void Start () {
        Invoke("DestrossarFiTemps", duracio);
        destrossat = false;
        limitDalt = transform.position.y + 4;
        limitDreta = transform.position.x + 4;
        limitEsquerra = transform.position.x - 4;
        asource = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!destrossat) {
            if (transform.position.y < limitDalt && transform.position.x < limitDreta && transform.position.x > limitEsquerra) transform.Translate(Vector3.up * 0.1f);
        } else transform.Translate(Vector3.down * 0.2f);
    }

    private void DestrossarFiTemps()
    {
        Destrossar(3);
    }

    public void Destrossar(int dany)
    {
        if (!destrossat)
        {
            resistencia -= dany;
            if (resistencia <= 0)
            {
                GameObject.Find("Player").GetComponent<Rigidbody>().WakeUp();
                Destroy(gameObject, 2f);
                anularColliders();
                asource.Play();
                destrossat = true;
            }
        }
    }

    private void anularColliders()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        if (gameObject.GetComponent<BoxCollider>() != null)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (!destrossat)
        {
            if (tag != "Player" && tag != "TerraRoca" && tag != "ParetRoca")
            {
                if (tag == "Pedra" || tag == "GranPedra")
                {
                    if (collision.gameObject.GetComponent<PedraMove>().getAttacking())
                    {
                        if (tag == "Pedra") Destrossar(1);
                        else Destrossar(3);
                    }
                }
                else if (tag == "Enemy")
                {
                    Destrossar(3);
                }
                else
                {
                    Destrossar(1);
                }
            }
        }
    }
}
