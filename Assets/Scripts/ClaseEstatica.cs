using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClaseEstatica
{
    // Variables globales de selección de nivel

    public static string nivelSeleccionado = "Assets/Niveles/1x1Test.txt";

    // Variables globales movimiento
    public static InfoMovimiento infoMovimientoH = new InfoMovimiento();
    public static InfoMovimiento infoMovimientoV = new InfoMovimiento();

    // Configuraciones Globales

    public static float separacionRay;
    public static float longitudRayCast;


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
