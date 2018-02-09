using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolaAiguaMove : GeneralFunctions {

    public int rotationSpeed;

    private float indexRotacio;

  //  private bool attacking;
    private GameObject destination;

	// Use this for initialization
	void Start () {
        destination = GameObject.FindGameObjectWithTag("Enemy");
        indexRotacio = 0;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, -(indexRotacio), 0);
        transform.Rotate(-90, 0, 0);

        Vector3 puntoDeChoque;
        puntoDeChoque = new Vector3(destination.transform.position.x, destination.transform.position.y + 1, destination.transform.position.z);
        transform.rotation = Quaternion.Slerp(transform.rotation,
        Quaternion.LookRotation(puntoDeChoque - transform.position), rotationSpeed * Time.deltaTime);

        transform.localScale += new Vector3(0.005f, 0.01f, 0.005f);

        transform.Rotate(90, 0, 0);

        indexRotacio += 5f;
        transform.Rotate(0, indexRotacio, 0);
    }

    public void Attack(GameObject gameObject)
    {
        destination = gameObject;
        if (gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<EnemyController>().Defend();
        }
   //     attacking = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!esTerra(collision.gameObject) && collision.gameObject.tag != "PistolaAigua")
        {
          Destroy(gameObject);
        }
    }
}
