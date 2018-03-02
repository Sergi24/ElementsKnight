using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralFunctions : MonoBehaviour {

	protected bool esTerra(string tag)
    {
        if (tag == "TerraRoca" || tag == "ParetRoca" || tag == "Terra" || tag == "TerraAigua" || tag == "TerraFoc") return true;
        else return false;
    }

    protected bool esPedra(string tag)
    {
        if (tag == "Pedra" || tag == "GranPedra") return true;
        else return false;
    }

    protected bool esRoca(string tag)
    {
        if (tag == "Roca" || tag == "GranRoca") return true;
        else return false;
    }
}
