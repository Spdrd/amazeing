using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovBola : MonoBehaviour
{
    [SerializeField] LayerMask paredH;
    [SerializeField] LayerMask paredV;

    [SerializeField] Vector3 posDestino;

    private bool enMovimiento = false;
    [SerializeField] bool enMovimientoUp = false;
    [SerializeField] bool enMovimientoLef = false;
    [SerializeField] bool enMovimientoDow = false;
    [SerializeField] bool enMovimientoRig = false;

    private bool reiniciando = false;

    private Vector3 inicio;

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
        ClaseEstatica.posBola = transform.position;
        RaycastHit2D rayUp = new RaycastHit2D();
        RaycastHit2D rayIzq = new RaycastHit2D();
        RaycastHit2D rayDow = new RaycastHit2D();
        RaycastHit2D rayDer = new RaycastHit2D();

        float step = ClaseEstatica.velocidadBola * Time.deltaTime;

        if(!reiniciando)
        { 
            generarRayCast(ref rayUp, ref rayIzq, ref rayDow, ref rayDer); 
        }

        if (rayUp && !enMovimientoDow)
        {
            enMovimientoDow = true;
            posDestino += Vector3.down;
        }

        if (rayIzq && !enMovimientoRig)
        {
            enMovimientoRig = true;
            posDestino += Vector3.right;
        }

        if (rayDow && !enMovimientoUp)
        {
            enMovimientoUp = true;
            posDestino += Vector3.up;
        }

        if (rayDer && !enMovimientoLef)
        {
            enMovimientoLef = true;
            posDestino += Vector3.left;
        }

        if(transform.position ==  posDestino)
        {
            enMovimiento = false;
            enMovimientoUp = false;
            enMovimientoLef = false;
            enMovimientoDow = false;
            enMovimientoRig = false;
            if(!(ClaseEstatica.infoMovimientoH.enMov || ClaseEstatica.infoMovimientoV.enMov))
            {
                reiniciando = false;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, posDestino, step);
    }
    private void reestart()
    {
        if (Input.GetKey(KeyCode.R))
        {
            posDestino = inicio;
            transform.position = inicio;
        }
    }

    private void generarRayCast(ref RaycastHit2D rayUp, ref RaycastHit2D rayIzq, ref RaycastHit2D rayDow, 
        ref RaycastHit2D rayDer)
    {
        rayUp = Physics2D.Raycast(transform.position, Vector2.up, ClaseEstatica.distDetectBola, paredH);
        if (rayUp)
        {
            Debug.DrawRay(transform.position, Vector2.up * ClaseEstatica.distDetectBola, Color.blue);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.up * ClaseEstatica.distDetectBola, Color.red);
        }
        rayIzq = Physics2D.Raycast(transform.position, Vector2.left, ClaseEstatica.distDetectBola, paredV);
        if (rayIzq)
        {
            Debug.DrawRay(transform.position, Vector2.left * ClaseEstatica.distDetectBola, Color.blue);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.left * ClaseEstatica.distDetectBola, Color.red);
        }

        rayDow = Physics2D.Raycast(transform.position, Vector2.down, ClaseEstatica.distDetectBola, paredH);
        if (rayDow)
        {
            Debug.DrawRay(transform.position, Vector2.down * ClaseEstatica.distDetectBola, Color.blue);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.down * ClaseEstatica.distDetectBola, Color.red);
        }

        rayDer = Physics2D.Raycast(transform.position, Vector2.right, ClaseEstatica.distDetectBola, paredV);
        if (rayDer)
        {
            Debug.DrawRay(transform.position, Vector2.right * ClaseEstatica.distDetectBola, Color.blue);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.right * ClaseEstatica.distDetectBola, Color.red);
        }
    }
}
