using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocaMove : MonoBehaviour {
    private float limitDalt, limitDreta;
    private bool destrossat;
	// Use this for initialization
	void Start () {
        Invoke("Destrossar", 10f);
        destrossat = false;
        limitDalt = transform.position.y + 4;
        limitDreta = transform.position.x + 4;
    }
	
	// Update is called once per frame
	void Update () {
        if (!destrossat) {
            if (transform.position.y < limitDalt && transform.position.x < limitDreta) transform.Translate(Vector3.up * 0.1f);
        } else transform.Translate(Vector3.down * 0.05f);
    }

    public void Destrossar()
    {
        if (!destrossat)
        {
            Destroy(gameObject, 2f);
            destrossat = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "TerraRoca" && collision.gameObject.tag != "ParetRoca")
        {
            Destrossar();
        }
    }
}
