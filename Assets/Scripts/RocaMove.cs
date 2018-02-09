using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocaMove : MonoBehaviour {
    private float limitDalt, limitDreta;
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
        asource = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!destrossat) {
            if (transform.position.y < limitDalt && transform.position.x < limitDreta) transform.Translate(Vector3.up * 0.1f);
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
                Destroy(gameObject, 2f);
                asource.Play();
                destrossat = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!destrossat)
        {
            if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "TerraRoca" && collision.gameObject.tag != "ParetRoca")
            {
                if (collision.gameObject.tag == "Pedra" || collision.gameObject.tag == "GranPedra")
                {
                    if (collision.gameObject.GetComponent<PedraMove>().getAttacking())
                    {
                        if (collision.gameObject.tag == "Pedra") Destrossar(1);
                        else Destrossar(3);
                    }
                }
                else Destrossar(1);
            }
        }
    }
}
