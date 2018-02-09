using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralFunctions : MonoBehaviour {

	protected bool esTerra(GameObject gameObject)
    {
        if (gameObject.tag == "TerraRoca" || gameObject.tag == "ParetRoca" || gameObject.tag == "Terra" || gameObject.tag == "TerraAigua") return true;
        else return false;
    }

    protected bool esPedra(GameObject gameObject)
    {
        if (gameObject.tag == "Pedra" || gameObject.tag == "GranPedra") return true;
        else return false;
    }

    protected bool esRoca(GameObject gameObject)
    {
        if (gameObject.tag == "Roca" || gameObject.tag == "GranRoca") return true;
        else return false;
    }
}
