using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tutorial : MonoBehaviour
{
    [SerializeField] bool vertical;
    [SerializeField] LayerMask capa;

    [SerializeField] RaycastHit2D ray1;

    private float tiempoW = 0;
    private float tiempoA = 0;
    private float tiempoS = 0;
    private float tiempoD = 0;

    private bool enMovimiento = false;
    private bool enMovimientoUp = false;
    private bool enMovimientoLef = false;
    private bool enMovimientoDow = false;
    private bool enMovimientoRig = false;
    private bool rebotando1 = false;
    private bool rebotando2 = false;

    [SerializeField] TextMeshProUGUI mensajeW;
    [SerializeField] TextMeshProUGUI mensajeS;
    [SerializeField] TextMeshProUGUI mensajeA;
    [SerializeField] TextMeshProUGUI mensajeD;
    [SerializeField] TextMeshProUGUI mensajeMeta;
    [SerializeField] SpriteRenderer meta;

    [SerializeField] bool permisoLef = false;
    [SerializeField] bool permisoRig = false;
    [SerializeField] bool permisoUp = false;
    [SerializeField] bool permisoDow = false;

    private float tFadeAnimaciones = 3;

    private float velocidadMovimiento = 1.0f;

    private float longitudRayCast = 0.7f;

    [SerializeField] Vector3 posDestino;

    private int pasosTutorial = -1;
    private float tiempoPostInicio = 0;
    private bool unaVez = true;
    private bool cPrendio = false;

    private float contadorUltimoMensje = 0;

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
        Color color = mensajeA.color;
        color.a = 0;
        mensajeW.color = color;
        mensajeA.color = color;
        mensajeS.color = color;
        mensajeD.color = color;
        mensajeMeta.color = color;
        meta.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return) || cPrendio)
        {
            cPrendio = true;
            tiempoPostInicio += Time.deltaTime;
        }
        if (tiempoPostInicio > 5 && unaVez)
        {
            unaVez = false;
            pasosTutorial = 0;
        }
        mensajesTutorial();
        reestart();
        movimiento();
    }

    private void mensajesTutorial()
    {
        Color transparente = new Color();
        transparente.a = 0;
        switch(pasosTutorial){
            case 0:
                mensajeW.color = Color.Lerp(mensajeW.color, Color.white, Time.deltaTime / tFadeAnimaciones);
                permisoUp = true;
                if (Input.GetKey(KeyCode.W))
                {
                    pasosTutorial++;
                }
                break;
            case 1:
                mensajeW.color = Color.Lerp(mensajeW.color, transparente, Time.deltaTime / tFadeAnimaciones);
                mensajeS.color = Color.Lerp(mensajeS.color, Color.white, Time.deltaTime / tFadeAnimaciones);
                permisoDow = true;
                if (Input.GetKey(KeyCode.S))
                {
                    pasosTutorial++;
                }
                break;
            case 2:
                mensajeS.color = Color.Lerp(mensajeS.color, transparente, Time.deltaTime / tFadeAnimaciones);
                mensajeA.color = Color.Lerp(mensajeA.color, Color.white, Time.deltaTime / tFadeAnimaciones);
                permisoLef = true;
                if (Input.GetKey(KeyCode.A))
                {
                    pasosTutorial++;
                }
                break;
            case 3:
                mensajeA.color = Color.Lerp(mensajeA.color, transparente, Time.deltaTime / tFadeAnimaciones);
                mensajeD.color = Color.Lerp(mensajeD.color, Color.white, Time.deltaTime / tFadeAnimaciones);
                permisoRig = true;
                if (Input.GetKey(KeyCode.D))
                {
                    pasosTutorial++;
                }
                break;
            case 4:
                mensajeD.color = Color.Lerp(mensajeD.color, transparente, Time.deltaTime / tFadeAnimaciones);
                mensajeMeta.color = Color.Lerp(mensajeMeta.color, Color.white, Time.deltaTime / tFadeAnimaciones);
                meta.color = Color.Lerp(meta.color, Color.white, Time.deltaTime / tFadeAnimaciones);
                permisoRig = true;
                permisoDow = true;
                permisoLef = true;
                permisoUp = true;
                contadorUltimoMensje += Time.deltaTime;
                if(contadorUltimoMensje > 10)
                {
                    pasosTutorial = 5;
                }
                break;
            case 5:
                mensajeMeta.color = Color.Lerp(mensajeD.color, transparente, Time.deltaTime / tFadeAnimaciones);
                permisoRig = true;
                permisoDow = true;
                permisoLef = true;
                permisoUp = true;
                break;
        }
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

        if ((entrada == Control.izquierda) && !enMovimiento && vertical && permisoLef)
        {
            permisoLef = false;
            enMovimientoLef = true;
            enMovimiento = true;
            posDestino = transform.position + Vector3.left;
        }

        // Derecha

        if ((entrada == Control.derecha) && !enMovimiento && vertical && permisoRig)
        {
            permisoRig = false;
            enMovimientoRig = true;
            enMovimiento = true;
            posDestino = transform.position + Vector3.right;
        }

        // Arriba

        if ((entrada == Control.arriba) && !enMovimiento && !vertical && permisoUp)
        {
            permisoUp = false;
            enMovimientoUp = true;
            enMovimiento = true;
            posDestino = transform.position + Vector3.up;
        }

        // Abajo

        if ((entrada == Control.abajo) && !enMovimiento && !vertical && permisoDow)
        {
            permisoDow = false;
            enMovimientoDow = true;
            enMovimiento = true;
            posDestino = transform.position + Vector3.down;
        }
        if ((enMovimientoUp || enMovimientoRig))
        {
            generarRayCast(ref ray1, direccion, direccionV3, 0.068f, longitudRayCast);
        }

        if ((enMovimientoDow || enMovimientoLef))
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

        if (ray1 && vertical && enMovimientoLef && !rebotando2)
        {
            rebotando1 = true;
            print("Choque izquierda");
            enMovimientoLef = false;
            enMovimientoRig = true;
            posDestino += Vector3.right;
        }

        if (ray1 && !vertical && enMovimientoUp && !rebotando1)
        {
            rebotando2 = true;
            print("Choque arriba");
            enMovimientoUp = false;
            enMovimientoDow = true;
            posDestino += Vector3.down;
        }

        if (ray1 && !vertical && enMovimientoDow && !rebotando2)
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

