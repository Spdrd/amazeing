using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovMuros : MonoBehaviour
{
    [SerializeField] bool alta;
    [SerializeField] LayerMask capa;

    // Ray Positivo [Arriba / Derecha]
    private RaycastHit2D ray1;
    private bool choque1 = false;
    // Ray Negativo [Abajo / Izquierda]
    private RaycastHit2D ray2;
    private bool choque2 = false;

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
        if (!accederControlInv())
        {
            entradas();
        }
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
        }
    }

    private void registrarChoques()
    {
        Vector2 positivo2;
        Vector2 negativo2;
        Vector3 positivo3;
        Vector3 negativo3;
        if (alta)
        {
            positivo2 = Vector2.right;
            negativo2 = Vector2.left;
            positivo3 = Vector3.right;
            negativo3 = Vector3.left;
        }
        else
        {
            positivo2 = Vector2.up;
            negativo2 = Vector2.down;
            positivo3 = Vector3.up;
            negativo3 = Vector3.down;
        }
        generarRayCast(ref ray1, positivo2, positivo3, separacionRay, longitudRayCast);
        generarRayCast(ref ray2, negativo2, negativo3, separacionRay, longitudRayCast);

        if (ray1 && !choque2)
        {
            choque1 = true;
            posDestino += negativo3;
        }

        if (ray2 && !choque1)
        {
            choque2 = true;
            posDestino += positivo3;
        }
        if (!accederControl())
        {
            choque1 = false;
            choque2 = false;
        }
    }

    public bool accederControlInv()
    {
        if (alta)
        {
            return ClaseEstatica.controlMovV;
        }
        else
        {
            return ClaseEstatica.controlMovH;
        }
    }

    public ref bool accederControl()
    {
        if (!alta)
        {
            return ref ClaseEstatica.controlMovV;
        }
        else
        {
            return ref ClaseEstatica.controlMovH;
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

    private void mover()
    {
        float step = velocidadMovimiento * Time.deltaTime;
        if(transform.position != posDestino)
        {
            enMovimiento = true;
            accederControl() = true;
            transform.position = Vector3.MoveTowards(transform.position, posDestino, step);
        }
        else
        {
            enMovimiento = false;
        }
    }

}
