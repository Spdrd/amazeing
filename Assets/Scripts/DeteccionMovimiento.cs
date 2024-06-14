using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeteccionMovimiento : MonoBehaviour
{
    [SerializeField] float tEspera = 0.04f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        validarMovimiento(ref ClaseEstatica.infoMovimientoH.control, ref ClaseEstatica.infoMovimientoH.tEsperado);
        validarMovimiento(ref ClaseEstatica.infoMovimientoV.control, ref ClaseEstatica.infoMovimientoV.tEsperado);
    }

    public void validarMovimiento(ref bool enMov, ref float tEsperado)
    {
        tEsperado += Time.deltaTime;
        if (tEsperado >= tEspera)
        {
            enMov = false;
            tEsperado = 0;
        }
    }
}
