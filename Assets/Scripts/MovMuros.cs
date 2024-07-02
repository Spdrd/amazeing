using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovMuros : MonoBehaviour
{
    Guid id = Guid.NewGuid();

    [SerializeField] bool alta;
    [SerializeField] LayerMask capa;
    [SerializeField] LayerMask capaLimite;

    private InfoMuro infoMuro;

    private enum Control
    {
        arriba,
        izquierda,
        abajo,
        derecha,
        quieto
    }

    // Info choque
    private RaycastHit2D ray1;
    private float tContactoRay = 0.0f;


    private float tiempoW = 0;
    private float tiempoA = 0;
    private float tiempoS = 0;
    private float tiempoD = 0;

    private bool enMov = false;
    private bool enMovAnterior = false;
    private bool enMovReportado = true;
    private bool permisoMov = true;
    private bool reiniciando = false;



    private Vector2 dirV2; 
    private Vector3 dirV3;


    [SerializeField] Control control;

    // Start is called before the first frame update
    void Start()
    {
        infoMuro = new InfoMuro(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        verificarDiccionario();
        entradas();
        reinicio();
        procesarEntrada(control);
        registrarChoques();
        mover();
        detectarMov();
    }

    public void verificarDiccionario()
    {
        if (!ClaseEstatica.infoMuros.ContainsKey(id))
        {
            ClaseEstatica.infoMuros.Add(id, infoMuro);
        }
    }

    private void detectarMov()
    {
        if (enMov != enMovAnterior)
        {
            enMovReportado = false;
        }
        if (!enMovReportado)
        {
            if (enMov)
            {
                accederInfoMov().numMurosEnMov++;
            }
            else
            {
                accederInfoMov().numMurosEnMov--;
            }
            enMovAnterior = enMov;
            enMovReportado = true;
        }
    }


    private void reinicio()
    {
        if (Input.GetKey(KeyCode.R))
        {
            transform.position = ClaseEstatica.infoMuros[id].inicio;
            ClaseEstatica.infoMuros[id].posDestino = ClaseEstatica.infoMuros[id].inicio;

        }
    }

    private void registrarChoques()
    {
        generarRayCast(ref ray1, dirV2, dirV3, ClaseEstatica.separacionRayMuros, ClaseEstatica.longitudRayCastMuros, capa);
        if (ray1)
        {
            ClaseEstatica.infoMuros[id].posDestino = ClaseEstatica.infoMuros[id].prevPosDestino - dirV3;
        }
    }

    public ref InfoMovimiento accederInfoMovInv()
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

    public ref InfoMovimiento accederInfoMov()
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
        if (Input.GetKey(tecla) && (tiempoA == 0) && !enMov)
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
        permisoMov = false;
    }

    private void entradas()
    {
        Control con = Control.quieto;

        if (permisoMov)
        {
            // Izquierda
            entrada(ref con, KeyCode.A, Control.izquierda, ref tiempoA);

            // Derecha
            entrada(ref con, KeyCode.D, Control.derecha, ref tiempoD);

            // Arriba
            entrada(ref con, KeyCode.W, Control.arriba, ref tiempoW);

            // Abajo
            entrada(ref con, KeyCode.S, Control.abajo, ref tiempoS);
        }
        control = con;
    }

    private void procesarEntrada(Control entrada)
    {
        if (alta)
        {
            // Mov Horizontal

            if (entrada == Control.derecha)
            {
                dirV2 = Vector2.right;
                dirV3 = Vector3.right;
                ClaseEstatica.infoMuros[id].posDestino += dirV3;
                ClaseEstatica.infoMuros[id].prevPosDestino = ClaseEstatica.infoMuros[id].posDestino;
            }
            if (entrada == Control.izquierda)
            {
                dirV2 = Vector2.left;
                dirV3 = Vector3.left;
                ClaseEstatica.infoMuros[id].posDestino += dirV3;
                ClaseEstatica.infoMuros[id].prevPosDestino = ClaseEstatica.infoMuros[id].posDestino;
            }
        }
        else
        {
            // Mov Vertical

            if (entrada == Control.arriba)
            {
                dirV2 = Vector2.up;
                dirV3 = Vector3.up;
                ClaseEstatica.infoMuros[id].posDestino += dirV3;
                ClaseEstatica.infoMuros[id].prevPosDestino = ClaseEstatica.infoMuros[id].posDestino;
            }
            if (entrada == Control.abajo)
            {
                dirV2 = Vector2.down;
                dirV3 = Vector3.down;
                ClaseEstatica.infoMuros[id].posDestino += dirV3;
                ClaseEstatica.infoMuros[id].prevPosDestino = ClaseEstatica.infoMuros[id].posDestino;
            }
        }
        
    }

    private void generarRayCast(ref RaycastHit2D ray1, Vector2 direccion,
       Vector3 directionV3, float separacion, float longitud, LayerMask cap)
    {
        ray1 = Physics2D.Raycast(transform.position + (directionV3 * separacion), direccion, longitud,
            cap);
        if (ray1)
        {
            Debug.DrawRay(transform.position + (directionV3 * separacion), direccion * longitud, Color.blue);
        }
        else
        {
            Debug.DrawRay(transform.position + (directionV3 * separacion), direccion * longitud, Color.red);
        }
    }

    private void mover()
    {
        float step = ClaseEstatica.velocidadMuros * Time.deltaTime;
        if(transform.position != ClaseEstatica.infoMuros[id].posDestino)
        {
            enMov = true;
            transform.position = Vector3.MoveTowards(transform.position, ClaseEstatica.infoMuros[id].posDestino, step);
        }
        else
        {
            enMov = false;
            if (!(accederInfoMov().enMov || accederInfoMovInv().enMov))
            {
                permisoMov = true;
                reiniciando = false;
            }
        }
    }

}
