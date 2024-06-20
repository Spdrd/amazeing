using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeteccionMovimiento : MonoBehaviour
{
    private float tEspera = 0.0f;
    [SerializeField] float tSinc = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        validarMovimiento(ref ClaseEstatica.infoMovimientoH);
        validarMovimiento(ref ClaseEstatica.infoMovimientoV);
    }

    public void validarMovimiento(ref InfoMovimiento infoMov)
    {
        if (infoMov.numMurosEnMov == 0)
        {
            tEspera += Time.deltaTime;
        }
        else
        {
            infoMov.enMov = true;
            tEspera = 0;
        }

        if(tEspera > tSinc)
        {
            infoMov.enMov = false;
        }
    }
}
