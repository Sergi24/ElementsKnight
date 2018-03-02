using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerraController : MonoBehaviour
{
    public GameObject lava;
    public int tempsPosesio;
    public Material terraRocaMaterial, negre;

    private GameObject elementActual, knightActual;
    private bool terraAssignat;
    private IEnumerator coroutineTempsAcabat;

    private void Start()
    {
        coroutineTempsAcabat = null;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (knightActual == null || knightActual.Equals(collision.gameObject))
        {
            if (collision.gameObject.tag == "FirePlayer")
            {
                if (knightActual == null)
                {
                    elementActual = Instantiate(lava, transform.position, Quaternion.identity);
                    ParticleSystem.ShapeModule shape = elementActual.GetComponent<ParticleSystem>().shape;
                    shape.scale = new Vector3((gameObject.transform.localScale.x * 10) - 5f, 0.01f, 0.01f);
                }

                elementAssignat(collision, "TerraFoc");
            }
            else if (collision.gameObject.tag == "GroundPlayer")
            {
                Renderer actualRender = gameObject.GetComponent<Renderer>();
                actualRender.material = terraRocaMaterial;

                elementAssignat(collision, "TerraRoca");
            }
        }
    }

    private void elementAssignat(Collision collision, string terra)
    {
        knightActual = collision.gameObject;
        gameObject.tag = terra;

        if (coroutineTempsAcabat != null)
        {
            StopCoroutine(coroutineTempsAcabat);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
   //     Debug.Log(knightActual + "   " + collision.gameObject);
        if (knightActual != null && knightActual.Equals(collision.gameObject))
        {
            coroutineTempsAcabat = TempsAcabat();
            StartCoroutine(coroutineTempsAcabat);
        }
    }

    IEnumerator TempsAcabat()
    {
        yield return new WaitForSeconds(tempsPosesio);
        if (knightActual.tag == "FirePlayer")
        {
            ParticleSystem.EmissionModule emission = elementActual.GetComponent<ParticleSystem>().emission;
            emission.rateOverTime = 0;
            Destroy(elementActual, 4f);
        }
        else if (knightActual.tag == "GroundPlayer")
        {
            Renderer actualRender = gameObject.GetComponent<Renderer>();
            actualRender.material = negre;
        }
        gameObject.tag = "Terra";
        knightActual = null;
    }
}
