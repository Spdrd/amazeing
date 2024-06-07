using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovMuros : MonoBehaviour
{
    [SerializeField] bool vertical;
    [SerializeField] LayerMask capa;

    // Ray Positivo [Arriba / Derecha]
    private RaycastHit2D ray1;
    // Ray Negativo [Abajo / Izquierda]
    private RaycastHit2D ray2;

    private float tiempoW = 0;
    private float tiempoA = 0;
    private float tiempoS = 0;
    private float tiempoD = 0;

    private bool enMovimiento = false;
    [SerializeField] bool enMovimientoUp = false;
    [SerializeField] bool enMovimientoLef = false;
    [SerializeField] bool enMovimientoDow = false;
    [SerializeField] bool enMovimientoRig = false;
    [SerializeField] bool rebotando1 = false;
    [SerializeField] bool rebotando2 = false;

    private float velocidadMovimiento = 1.0f;

    private float longitudRayCast = 0.7f;

    [SerializeField] Vector3 posDestino;

    private Vector3 inicio;
    private enum Control
    {
        arriba,
        izquierda,
        abajo,
        derecha,
        quieto
    }

    [SerializeField] Control control;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        entradas();
    }
    private void entrada(ref Control control, KeyCode tecla, Control orden, 
        ref float tiempoTecla)
    {
        if (Input.GetKey(tecla) && (tiempoA == 0))
        {
            control = orden;
        }

        if (Input.GetKey(tecla))
        {
            tiempoTecla += Time.deltaTime;
        }
        else
        {
            tiempoTecla = 0;
        }
    }

    private void entradas()
    {
        Control con = Control.quieto;

        // Izquierda
        entrada(ref con, KeyCode.A, Control.izquierda, ref tiempoA);

        // Derecha
        entrada(ref con, KeyCode.D, Control.derecha, ref tiempoD);

        // Arriba
        entrada(ref con, KeyCode.W, Control.arriba, ref tiempoW);

        // Abajo
        entrada(ref con, KeyCode.S, Control.abajo, ref tiempoS);

        control = con;
    }

    private void generarRayCast(ref RaycastHit2D ray1, Vector2 direccion,
       Vector3 directionV3, float separacion, float longitud)
    {
        ray1 = Physics2D.Raycast(transform.position + (directionV3 * separacion), direccion, longitud - separacion,
            capa);
        if (ray1)
        {
            Debug.DrawRay(transform.position, direccion * longitud, Color.blue);
        }
        else
        {
            Debug.DrawRay(transform.position, direccion * longitud, Color.red);
        }
    }

    private void movimiento(Control entrada, Control comparar)
    {
        if ((entrada == Control.izquierda) && !enMovimiento && vertical)
        {
            enMovimientoLef = true;
            enMovimiento = true;
            posDestino = transform.position + Vector3.left;
        }
    }

}
