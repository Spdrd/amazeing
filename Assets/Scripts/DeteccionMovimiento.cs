using Assets.Scripts;
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
        validarMovimiento(ref ClaseEstatica.infoMovimientoH);
        validarMovimiento(ref ClaseEstatica.infoMovimientoV);
    }

    public void validarMovimiento(ref InfoMovimiento infoMov)
    {
        infoMov.tEsperado += Time.deltaTime;
        if (infoMov.tEsperado >= tEspera)
        {
            infoMov.enMov = false;
            infoMov.tEsperado = 0;
        }
    }
}
