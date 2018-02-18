using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolaAiguaMove : AVida, IResistencia
{

    public int rotationSpeed;

    private float indexRotacio;
    private bool teObjectiu;
    private GameObject fontAigua;
    private int contador;

  //  private bool attacking;
    private GameObject destination;

	// Use this for initialization
	void Start () {
        indexRotacio = 0;
        teObjectiu = false;
        contador = 0;
        Destroy(gameObject, 4f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, -(indexRotacio), 0);
        transform.Rotate(-90, 0, 0);

        if (destination != null)
        {
            teObjectiu = true;
            Vector3 puntoDeChoque;
            puntoDeChoque = new Vector3(destination.transform.position.x, destination.transform.position.y, destination.transform.position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(puntoDeChoque - transform.position), rotationSpeed * Time.deltaTime);

            transform.localScale += new Vector3(0.005f, 0.01f, 0.005f);

            transform.Rotate(90, 0, 0);
        }
        else if (teObjectiu) Destrossar(1);
        else if (contador > 10) Destrossar(1);
        else contador++;

        indexRotacio += 8f;
        transform.Rotate(0, indexRotacio, 0);
    }

    public void Attack(GameObject destination)
    {
        this.destination = destination;
        if (destination.tag == "Enemy")
        {
            destination.GetComponent<EnemyController>().Defend(gameObject);
        }
    }

    public void Destrossar(int dany)
    {
   //     gameObject.GetComponent<Renderer>().enabled = false;
     //   gameObject.GetComponent<CapsuleCollider>().enabled = false;
    /*    EllipsoidParticleEmitter[] steams = fontAigua.GetComponentsInChildren<EllipsoidParticleEmitter>();
        foreach (EllipsoidParticleEmitter steam in steams)
        {
            steam.emit = false;
        }*/
    //    Destroy(fontAigua, 2f);
        Destroy(gameObject);
    }

    public void SetFontAigua(GameObject fontAigua)
    {
        this.fontAigua = fontAigua;
    }

    private void OnTriggerEnter(Collider collider)
    {
        string tag = collider.gameObject.tag;
        if (!esTerra(tag) && tag != "PistolaAigua" && tag != "Player" && tag != "AquaBall")
        {
            if (tag == "Pedra" || tag == "GranPedra" || esRoca(tag)) collider.gameObject.GetComponent<IResistencia>().Destrossar(1);
            Destrossar(1);
        }
    }
}
