using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : GeneralFunctions
{
    private new Camera camera;

    public GameObject roca, granRoca, pistolaAigua, fontAigua, thunder, tormentaElectrica, aquaBall, erupcioRoca;
    public Texture terraRocaTexture;

    // Use this for initialization
    void Start()
    {
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

            funcionsTerra(hit.transform, hit.point);

            funcionsAigua(hit.transform, hit.point);

            funcionsElectricitat(hit.transform, hit.point);
        }
    }

    void funcionsTerra(Transform hit, Vector3 objectHit)
    {
        // Do something with the object that was hit by the raycast.
        if (hit.tag == "TerraRoca")
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.E))
            {
                GameObject erupcioRocaNova = Instantiate(erupcioRoca, new Vector3(hit.transform.position.x, hit.transform.position.y + 1.78f, hit.transform.position.z), Quaternion.identity);
                ParticleSystem.ShapeModule shape = erupcioRocaNova.GetComponent<ParticleSystem>().shape;
                shape.scale = new Vector3(hit.gameObject.transform.localScale.x*10, 0.01f, hit.gameObject.transform.localScale.z * 10);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Instantiate(roca, new Vector3(objectHit.x, objectHit.y - 2, objectHit.z), Quaternion.identity);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Instantiate(granRoca, new Vector3(objectHit.x, objectHit.y - 2, objectHit.z), Quaternion.identity);
            }
        }
        else if (hit.tag == "ParetRoca")
        {
            int orientacioParet;
            if (hit.gameObject.transform.rotation.eulerAngles.z == 270) orientacioParet = -2;
            else orientacioParet = 2;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Instantiate(roca, new Vector3(objectHit.x + orientacioParet, objectHit.y, objectHit.z), Quaternion.Euler(new Vector3(0, 0, hit.gameObject.transform.rotation.eulerAngles.z)));
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Instantiate(granRoca, new Vector3(objectHit.x + orientacioParet, objectHit.y, objectHit.z), Quaternion.Euler(new Vector3(0, 0, hit.gameObject.transform.rotation.eulerAngles.z)));
            }
        }
        else if ((hit.tag == "Roca" || hit.tag == "GranRoca") && Input.GetKeyDown(KeyCode.Mouse2))
        {
            hit.GetComponent<RocaMove>().Destrossar(3);
        }
        else if ((hit.tag == "Pedra" || hit.tag == "GranPedra") && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!hit.GetComponent<PedraMove>().getSelected())
            {
                hit.GetComponent<PedraMove>().Select();
            }
        }
        else if (hit.tag == "Enemy" && Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject[] pedres = GameObject.FindGameObjectsWithTag("Pedra");
            foreach (GameObject pedra in pedres)
            {
                if (!pedra.GetComponent<PedraMove>().getAttacking())
                {
                    pedra.GetComponent<PedraMove>().Attack(hit.gameObject);
                }
            }
            pedres = GameObject.FindGameObjectsWithTag("GranPedra");
            foreach (GameObject pedra in pedres)
            {
                if (!pedra.GetComponent<PedraMove>().getAttacking())
                {
                    pedra.GetComponent<PedraMove>().Attack(hit.gameObject);
                }
            }
        } 
        else if (hit.tag == "Terra" && Input.GetKeyDown(KeyCode.Mouse2))
        {
            hit.gameObject.GetComponent<Renderer>().material.SetTexture("TerraRoca", terraRocaTexture);
        }
    }

    void funcionsAigua(Transform hit, Vector3 objectHit)
    {
        if (hit.tag == "TerraAigua" && Input.GetKeyDown(KeyCode.Mouse2))
        {
           Instantiate(aquaBall, new Vector3(objectHit.x, objectHit.y - 2, hit.transform.position.z), Quaternion.identity);


            //aquaBallNova.GetComponent<PistolaAiguaMove>().Attack(GameObject.FindGameObjectWithTag("Enemy"));
        //     GameObject fontAiguaNova = Instantiate(fontAigua, new Vector3(objectHit.x, objectHit.y, hit.transform.position.z), Quaternion.identity);
        //      pistolaAiguaNova.GetComponent<PistolaAiguaMove>().SetFontAigua(fontAiguaNova);
        }
        else if (hit.tag == "AquaBall" && Input.GetKeyDown(KeyCode.Mouse1))
        {
            hit.gameObject.GetComponent<AquaBallMove>().FlyBegin(GameObject.Find("Player"));
        }
        else if ((esPedra(hit.tag)) && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (hit.gameObject.GetComponent<PedraMove>().getAttacking())
            {
                GameObject[] aquaBalls = GameObject.FindGameObjectsWithTag("AquaBall");
                GameObject aquaBallEscollida = null;
                float distanciaMinima = 10000f;
                foreach (GameObject aquaBall in aquaBalls)
                {
                    float distancia = (aquaBall.transform.position - hit.transform.position).magnitude;
                    if (distancia < distanciaMinima && !aquaBall.GetComponent<AquaBallMove>().GetPistolaAiguaCreada() && !aquaBall.GetComponent<AquaBallMove>().GetFlying())
                    {
                        distanciaMinima = distancia;
                        aquaBallEscollida = aquaBall;
                    }
                }
                if (aquaBallEscollida != null)
                {
                    aquaBallEscollida.GetComponent<AquaBallMove>().Attack(hit.transform.gameObject);

                 //   GameObject fontAiguaNova = Instantiate(fontAigua, new Vector3(objectHit.x, aquaBalls[0].transform.position.y, aquaBalls[0].transform.position.z), Quaternion.identity);
                   // pistolaAiguaNova.GetComponent<PistolaAiguaMove>().SetFontAigua(fontAiguaNova);
                }
            }
        }
    }

    void funcionsElectricitat(Transform hit, Vector3 objectHit)
    {
        //  if (Input.GetKeyDown(KeyCode.Mouse1)) Instantiate(tormentaElectrica, new Vector3(objectHit.x, 0, objectHit.z), Quaternion.identity);

        //     else
        //     {
       // Instantiate(thunder, new Vector3(objectHit.x, 0, objectHit.z), Quaternion.Euler(new Vector3(-90, 0, 0)));
        //     }
    }
}
