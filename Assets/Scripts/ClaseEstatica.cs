using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClaseEstatica
{
    // Variables globales de selección de nivel

    public static string nivelSeleccionado = "Assets/Niveles/nivelPruebas.txt";

    // Variables globales movimiento
    public static InfoMovimiento infoMovimientoH = new InfoMovimiento();
    public static InfoMovimiento infoMovimientoV = new InfoMovimiento();

    // Configuraciones Globales Muros
    public static float separacionRayMuros = 0.06f;
    public static float longitudRayCastMuros = 0.7f;
    public static float velocidadMuros = 1.0f;

    // Configuraciones Bola
    public static float velocidadBola = 1.4f;
    public static float distDetectBola = 0.15f;



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
