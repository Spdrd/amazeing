using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClaseEstatica
{
    // Variables globales de selección de nivel

    public static string nivelSeleccionado;

    // Variables globales movimiento
    public static InfoMovimiento infoMovimientoH = new InfoMovimiento();
    public static InfoMovimiento infoMovimientoV = new InfoMovimiento();

    // Configuraciones / Info Globales Muros
    public static float separacionRayMuros = 0.06f;
    public static float longitudRayCastMuros = 0.7f;
    public static float velocidadMuros = 1.0f;
    public static Dictionary<Guid, InfoMuro> infoMuros = new Dictionary<Guid, InfoMuro>();

    // Configuraciones / Info Bola
    public static float velocidadBola = 1.4f;
    public static float distDetectBola = 0.15f;
    public static Vector3 posBola;

    // Configuraciones / Info Meta
    public static Vector3 posMeta;
    public static bool nivelCompletado = false;



    // Funciones Generales

    public static bool esPar(int eval)
    {
        if((eval % 2) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
