using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeteccionMovimiento : MonoBehaviour
{
    private float tEsperadoH = 0.0f;
    private float tEsperadoV = 0.0f;
    [SerializeField] float tEspera = 0.04f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        validarMovimiento(ref ClaseEstatica.controlMovH, ref tEsperadoH);
        validarMovimiento(ref ClaseEstatica.controlMovV, ref tEsperadoV);
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
