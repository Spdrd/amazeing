using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoBola : MonoBehaviour
{
    [SerializeField] float distanciaDetect;
    [SerializeField] LayerMask paredH;
    [SerializeField] LayerMask paredV;
    [SerializeField] float velocidad;

    [SerializeField] Vector3 posDestino;

    private bool enMovimiento = false;
    [SerializeField] bool enMovimientoUp = false;
    [SerializeField] bool enMovimientoLef = false;
    [SerializeField] bool enMovimientoDow = false;
    [SerializeField] bool enMovimientoRig = false;

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
        RaycastHit2D rayUp = new RaycastHit2D();
        RaycastHit2D rayIzq = new RaycastHit2D();
        RaycastHit2D rayDow = new RaycastHit2D();
        RaycastHit2D rayDer = new RaycastHit2D();

        float step = velocidad * Time.deltaTime;

        generarRayCast(ref rayUp, ref rayIzq, ref rayDow, ref rayDer);

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
        rayUp = Physics2D.Raycast(transform.position, Vector2.up, distanciaDetect, paredH);
        if (rayUp)
        {
            Debug.DrawRay(transform.position, Vector2.up * distanciaDetect, Color.blue);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.up * distanciaDetect, Color.red);
        }
        rayIzq = Physics2D.Raycast(transform.position, Vector2.left, distanciaDetect, paredV);
        if (rayIzq)
        {
            Debug.DrawRay(transform.position, Vector2.left * distanciaDetect, Color.blue);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.left * distanciaDetect, Color.red);
        }

        rayDow = Physics2D.Raycast(transform.position, Vector2.down, distanciaDetect, paredH);
        if (rayDow)
        {
            Debug.DrawRay(transform.position, Vector2.down * distanciaDetect, Color.blue);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.down * distanciaDetect, Color.red);
        }

        rayDer = Physics2D.Raycast(transform.position, Vector2.right, distanciaDetect, paredV);
        if (rayDer)
        {
            Debug.DrawRay(transform.position, Vector2.right * distanciaDetect, Color.blue);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.right * distanciaDetect, Color.red);
        }
    }
}
