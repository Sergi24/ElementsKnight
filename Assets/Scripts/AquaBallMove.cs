using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaBallMove : MonoBehaviour {

    private bool pistolaAiguaCreada;
    private GameObject pistolaAiguaNova;
    private bool destroy, functionDestroyCalled;

    public GameObject pistolaAigua;

    // Use this for initialization
    void Start () {
        Invoke("Destrossar", 10f);
        pistolaAiguaCreada = false;
        destroy = false;
        functionDestroyCalled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < 2 && !destroy)
        {
            transform.position += transform.up * 0.05f;
        }
        if (pistolaAiguaNova == null)
        {
            if (destroy && !functionDestroyCalled)
            {
                Destroy(gameObject, 3f);
                functionDestroyCalled = true;
            }
            if (destroy) transform.position -= transform.up * 0.05f;
            pistolaAiguaCreada = false;
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
}
