using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoMuros : MonoBehaviour
{
    [SerializeField] bool vertical;
    [SerializeField] LayerMask capa;
    
    [SerializeField] RaycastHit2D ray1;

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

    // Start is called before the first frame update
    void Start()
    {
        inicio = transform.position;
        posDestino = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        reestart();
        movimiento();
    }

    private void reestart()
    {
        if (Input.GetKey(KeyCode.R))
        {
            posDestino = inicio;
            transform.position = inicio;
        }
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

    private void movimiento()
    {
        ray1 = new RaycastHit2D();
        Vector2 direccion;
        Vector3 direccionV3;
        if (vertical)
        {
            direccion = Vector2.right;
            direccionV3 = Vector3.right;
        }
        else
        {
            direccion = Vector2.up;
            direccionV3 = Vector3.up;
        }

        Control entrada = entradas();
        float step = velocidadMovimiento * Time.deltaTime;

        if (posDestino == transform.position)
        {
            enMovimiento = false;
            enMovimientoUp = false;
            enMovimientoLef = false;
            enMovimientoDow = false;
            enMovimientoRig = false;
            rebotando1 = false;
            rebotando2 = false;
        }

        // Izquierda

        if ((entrada == Control.izquierda) && !enMovimiento && vertical)
        {
            enMovimientoLef = true;
            enMovimiento = true;
            posDestino = transform.position + Vector3.left;
        }

        // Derecha

        if ((entrada == Control.derecha) && !enMovimiento && vertical)
        {
            enMovimientoRig = true;
            enMovimiento = true;
            posDestino = transform.position + Vector3.right;
        }

        // Arriba

        if ((entrada == Control.arriba) && !enMovimiento && !vertical)
        {
            enMovimientoUp = true;
            enMovimiento = true;
            posDestino = transform.position + Vector3.up;
        }

        // Abajo

        if ((entrada == Control.abajo) && !enMovimiento && !vertical)
        {
            enMovimientoDow = true;
            enMovimiento = true;
            posDestino = transform.position + Vector3.down;
        }
        if ((enMovimientoUp || enMovimientoRig))
        {
            generarRayCast(ref ray1, direccion, direccionV3, 0.068f, longitudRayCast);
        }

        if((enMovimientoDow || enMovimientoLef))
        {
            generarRayCast(ref ray1, -direccion, -direccionV3, 0.068f, longitudRayCast);
        }

        // Choques

        if (ray1 && vertical && enMovimientoRig && !rebotando1)
        {
            rebotando2 = true;
            print("Choque derecha");
            enMovimientoRig = false;
            enMovimientoLef = true;
            posDestino += Vector3.left;
        }

        if(ray1 && vertical && enMovimientoLef && !rebotando2)
        {
            rebotando1 = true;
            print("Choque izquierda");
            enMovimientoLef = false;
            enMovimientoRig = true;
            posDestino += Vector3.right;
        }

        if(ray1 && !vertical && enMovimientoUp && !rebotando1)
        {
            rebotando2 = true;
            print("Choque arriba");
            enMovimientoUp = false;
            enMovimientoDow = true;
            posDestino += Vector3.down;
        }

        if(ray1 && !vertical && enMovimientoDow && !rebotando2)
        {
            rebotando1 = true;
            print("Choque abajo");
            enMovimientoDow = false;
            enMovimientoUp = true;
            posDestino += Vector3.up;
        }

        transform.position = Vector3.MoveTowards(transform.position, posDestino, step);
    }

    private Control entradas()
    {
        Control control = Control.quieto;

        // Izquierda

        if (Input.GetKey(KeyCode.A) && (tiempoA == 0))
        {
            control = Control.izquierda;
        }

        if (Input.GetKey(KeyCode.A))
        {
            tiempoA += Time.deltaTime;
        }
        else
        {
            tiempoA = 0;
        }

        // Derecha

        if (Input.GetKey(KeyCode.D) && (tiempoD == 0))
        {
            control = Control.derecha;
        }

        if (Input.GetKey(KeyCode.D))
        {
            tiempoD += Time.deltaTime;
        }
        else
        {
            tiempoD = 0;
        }

        // Arriba

        if (Input.GetKey(KeyCode.W) && (tiempoW == 0))
        {
            control = Control.arriba;
        }

        if (Input.GetKey(KeyCode.W))
        {
            tiempoW += Time.deltaTime;
        }
        else
        {
            tiempoW = 0;
        }

        // Abajo

        if (Input.GetKey(KeyCode.S) && (tiempoS == 0))
        {
            control = Control.abajo;
        }

        if (Input.GetKey(KeyCode.S))
        {
            tiempoS += Time.deltaTime;
        }
        else
        {
            tiempoS = 0;
        }

        return control;
    }
}
