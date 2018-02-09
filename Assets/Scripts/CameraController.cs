using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private new Camera camera;

    public GameObject roca, granRoca, pistolaAigua;

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
	}

    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 objectHit = hit.point;

            funcionsTerra(hit, hit.point);

            funcionsAigua(hit, hit.point);

        }
    }

    void funcionsTerra(RaycastHit hit, Vector3 objectHit)
    {
        // Do something with the object that was hit by the raycast.
        if (hit.transform.gameObject.tag == "TerraRoca" && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(roca, new Vector3(objectHit.x, objectHit.y - 2, objectHit.z), Quaternion.identity);
        }
        else if (hit.transform.gameObject.tag == "TerraRoca" && Input.GetKeyDown(KeyCode.Mouse1))
        {
            Instantiate(granRoca, new Vector3(objectHit.x, objectHit.y - 2, objectHit.z), Quaternion.identity);
        }
        else if (hit.transform.gameObject.tag == "ParetRoca" && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(roca, new Vector3(objectHit.x - 2, objectHit.y, objectHit.z), Quaternion.Euler(new Vector3(0, 0, -90)));
        }
        else if (hit.transform.gameObject.tag == "ParetRoca" && Input.GetKeyDown(KeyCode.Mouse1))
        {
            Instantiate(granRoca, new Vector3(objectHit.x - 2, objectHit.y, objectHit.z), Quaternion.Euler(new Vector3(0, 0, -90)));
        }
        else if ((hit.transform.gameObject.tag == "Roca" || hit.transform.gameObject.tag == "GranRoca") && Input.GetKeyDown(KeyCode.Mouse2))
        {
            hit.transform.gameObject.GetComponent<RocaMove>().Destrossar(3);
        }
        else if ((hit.transform.gameObject.tag == "Pedra" || hit.transform.gameObject.tag == "GranPedra") && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!hit.transform.gameObject.GetComponent<PedraMove>().getSelected())
            {
                hit.transform.gameObject.GetComponent<PedraMove>().Selected();
            }
        }
        else if (hit.transform.gameObject.tag == "Enemy" && Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject[] pedres = GameObject.FindGameObjectsWithTag("Pedra");
            foreach (GameObject pedra in pedres)
            {
                if (!pedra.GetComponent<PedraMove>().getAttacking())
                {
                    pedra.GetComponent<PedraMove>().Attack(hit.transform.gameObject);
                }
            }
            pedres = GameObject.FindGameObjectsWithTag("GranPedra");
            foreach (GameObject pedra in pedres)
            {
                if (!pedra.GetComponent<PedraMove>().getAttacking())
                {
                    pedra.GetComponent<PedraMove>().Attack(hit.transform.gameObject);
                }
            }
        }
    }

    void funcionsAigua(RaycastHit hit, Vector3 objectHit)
    {
        if (hit.transform.gameObject.tag == "TerraAigua" && Input.GetKeyDown(KeyCode.Mouse0))
        {
                if (GameObject.FindGameObjectWithTag("PistolaAigua") == null) Instantiate(pistolaAigua, new Vector3(objectHit.x, objectHit.y - 1, objectHit.z), Quaternion.identity);
        }
    }

}
