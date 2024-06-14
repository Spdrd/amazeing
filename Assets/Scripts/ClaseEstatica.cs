using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClaseEstatica
{
    // Variables globales de selección de nivel

    public static string nivelSeleccionado = "Assets/Niveles/1x1Test.txt";

    // Variables globales movimiento
    public static infoMovimiento infoMovimientoH = new infoMovimiento();
    public static infoMovimiento infoMovimientoV = new infoMovimiento();


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
