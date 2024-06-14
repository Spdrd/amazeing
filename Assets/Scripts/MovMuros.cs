using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovMuros : MonoBehaviour
{
    [SerializeField] bool alta;
    [SerializeField] LayerMask capa;
    [SerializeField] LayerMask capaLimite;

    // Info choque
    private RaycastHit2D ray1;
    private bool choque1 = false;
    private Vector2 direccionV2 = Vector2.zero;
    private Vector3 direccionV3 = Vector3.zero;
    private float tContactoRay = 0.0f;

    private float separacionRay = 0.068f;

    private float tiempoW = 0;
    private float tiempoA = 0;
    private float tiempoS = 0;
    private float tiempoD = 0;

    private bool enMovimiento = false;

    private float velocidadMovimiento = 1.0f;

    [SerializeField] float longitudRayCast = 0.7f;

    private Vector3 posDestino;

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
        posDestino = transform.position;
        inicio = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        entradas();
        reinicio();
        procesarEntrada(control);
        registrarChoques();
        mover();
    }


    private void reinicio()
    {
        if (Input.GetKey(KeyCode.R))
        {
            posDestino = inicio;
            transform.position = inicio;
        }
    }

    private void registrarChoques()
    {
        if(!accederInfoMov().control || !enMovimiento){

            if (alta)
            {
                if (transform.position.x < posDestino.x)
                {
                    direccionV2 = Vector2.right;
                    direccionV3 = Vector3.right;
                }
                if (transform.position.x > posDestino.x)
                {
                    direccionV2 = Vector2.left;
                    direccionV3 = Vector3.left;
                }
            }
            else
            {
                if (transform.position.y < posDestino.y)
                {
                    direccionV2 = Vector2.up;
                    direccionV3 = Vector3.up;
                }
                if (transform.position.y > posDestino.y)
                {
                    direccionV2 = Vector2.down;
                    direccionV3 = Vector3.down;
                }
            }
            if(posDestino == transform.position)
            {
                direccionV2 = Vector2.zero;
                direccionV3 = Vector3.zero;
            }
        }
        generarRayCast(ref ray1, direccionV2, direccionV3, separacionRay, longitudRayCast, capa);

        if (ray1 && tContactoRay < 1)
        {
            tContactoRay += 1;
            posDestino -= direccionV3;
        }
        else if(!ray1)
        {
            tContactoRay = 0.0f;
        }
    }

    public ref infoMovimiento accederInfoMovInv()
    {
        if (alta)
        {
            return ref ClaseEstatica.infoMovimientoV;
        }
        else
        {
            return ref ClaseEstatica.infoMovimientoH;
        }
    }

    public ref infoMovimiento accederInfoMov()
    {
        if (!alta)
        {
            return ref ClaseEstatica.infoMovimientoV;
        }
        else
        {
            return ref ClaseEstatica.infoMovimientoH;
        }
    }

    private void entrada(ref Control control, KeyCode tecla, Control orden, 
        ref float tiempoTecla)
    {
        if (Input.GetKey(tecla) && (tiempoA == 0) && !enMovimiento)
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

    private void procesarEntrada(Control entrada)
    {
        // Mov Vertical

        if(entrada == Control.arriba && !alta)
        {
            posDestino += Vector3.up;
        }
        if (entrada == Control.abajo && !alta)
        {
            posDestino += Vector3.down;
        }

        // Mov Horizontal

        if (entrada == Control.derecha && alta)
        {
            posDestino += Vector3.right;
        }
        if (entrada == Control.izquierda && alta)
        {
            posDestino += Vector3.left;
        }
    }

    private void generarRayCast(ref RaycastHit2D ray1, Vector2 direccion,
       Vector3 directionV3, float separacion, float longitud, LayerMask cap)
    {
        ray1 = Physics2D.Raycast(transform.position + (directionV3 * separacion), direccion, longitud - separacion,
            cap);
        if (ray1)
        {
            Debug.DrawRay(transform.position, direccion * longitud, Color.blue);
        }
        else
        {
            Debug.DrawRay(transform.position, direccion * longitud, Color.red);
        }
    }

    private void mover()
    {
        float step = velocidadMovimiento * Time.deltaTime;
        if(transform.position != posDestino)
        {
            enMovimiento = true;
            accederInfoMov().control = true;
            accederInfoMov().tEsperado = 0.0f;
            transform.position = Vector3.MoveTowards(transform.position, posDestino, step);
        }
        else
        {
            enMovimiento = false;
        }
    }

}
