using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscutController : AVida, IResistencia
{
    private ParticleSystem ps;

    public Gradient gradient;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        ParticleSystem.MinMaxGradient mmg = ps.main.startColor;
        //     mmg.mode = ParticleSystemGradientMode.Gradient;
        mmg.gradient = gradient;
    }

    public void Destrossar(int dany)
    {
        vida -= dany;
        ps.Emit(10);
        ParticleSystem.MinMaxGradient mmg = ps.main.startColor;
   //     mmg.mode = ParticleSystemGradientMode.Gradient;
        mmg.gradient = gradient;
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!esTerra(collider.gameObject.tag))
        {
            //Destruccio ajena
            if (collider.gameObject.tag == "GranPedra") collider.GetComponent<IResistencia>().Destrossar(3);
            else collider.GetComponent<IResistencia>().Destrossar(1);

            //Destruccio propia
            if (collider.gameObject.tag == "GranPedra") Destrossar(3);
            else Destrossar(1);
        }
    }
}
