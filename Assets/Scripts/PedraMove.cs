using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedraMove : MonoBehaviour {
    private GameObject destination;
    private bool attacking;
    private bool selected;
    private Color initialColor;
    private Rigidbody rb;

    public int moveSpeed, rotationSpeed;
    public Color selectedColor;

    // Use this for initialization
    void Start () {
        attacking = false;
        selected = false;
        initialColor = gameObject.GetComponentInChildren<Renderer>().material.color;
        rb = GetComponent<Rigidbody>();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (attacking)
        {
            Destroy(gameObject);
        }
    }
}
